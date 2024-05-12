namespace Hyperar.HPA.Infrastructure.Strategies.FileDownloadTaskStepProcess.Persister
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Interfaces;
    using Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Models = Shared.Models.Hattrick;

    public class StaffList : PersisterBase, IFileDownloadTaskStepProcessStrategy
    {
        private readonly IDatabaseContext databaseContext;

        private readonly IHattrickRepository<Domain.Senior.HallOfFamePlayer> hallOfFameRepository;

        private readonly IHattrickRepository<Domain.Senior.StaffMember> staffMemberRepository;

        private readonly IHattrickRepository<Domain.Senior.Team> teamRepository;

        public StaffList(
            IDatabaseContext databaseContext,
            IHattrickRepository<Domain.Senior.HallOfFamePlayer> hallOfFameRepository,
            IHattrickRepository<Domain.Senior.StaffMember> staffMemberRepository,
            IHattrickRepository<Domain.Senior.Team> teamRepository)
        {
            this.databaseContext = databaseContext;
            this.hallOfFameRepository = hallOfFameRepository;
            this.staffMemberRepository = staffMemberRepository;
            this.teamRepository = teamRepository;
        }

        public override async Task PersistFileAsync(IXmlFileDownloadTask fileDownloadTask, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(fileDownloadTask.XmlFile, nameof(fileDownloadTask.XmlFile));
            ArgumentNullException.ThrowIfNull(fileDownloadTask.ContextId, nameof(fileDownloadTask.ContextId));

            if (fileDownloadTask.XmlFile is Models.StaffList.HattrickData file)
            {
                var team = await this.teamRepository.GetByHattrickIdAsync(fileDownloadTask.ContextId.Value);

                ArgumentNullException.ThrowIfNull(team, nameof(team));

                var staffIds = file.StaffList.StaffMembers.Select(x => x.StaffId).ToList();

                var staffIdsToDelete = await this.staffMemberRepository.Query(x => x.TeamHattrickId == fileDownloadTask.ContextId.Value
                                                                                && !staffIds.Contains(x.HattrickId))
                                                                       .Select(x => x.HattrickId)
                                                                       .ToArrayAsync(cancellationToken);

                if (staffIdsToDelete.Length > 0)
                {
                    await this.DeleteStaffAsync(staffIdsToDelete);
                }

                foreach (var xmlStaff in file.StaffList.StaffMembers)
                {
                    await this.ProcessStaff(xmlStaff, team);
                }
            }
            else
            {
                throw new ArgumentException(
                    string.Format(
                        Globalization.Translations.UnexpectedFileType,
                        typeof(Models.StaffList.HattrickData).FullName,
                        fileDownloadTask.XmlFile.GetType().FullName));
            }

            await this.databaseContext.SaveAsync();
        }

        private async Task DeleteStaffAsync(long[] staffIdsToDelete)
        {
            await this.staffMemberRepository.DeleteRangeAsync(staffIdsToDelete);
        }

        private async Task ProcessStaff(Models.StaffList.Staff xmlStaff, Domain.Senior.Team team)
        {
            var staffMember = await this.staffMemberRepository.GetByHattrickIdAsync(xmlStaff.StaffId);

            Domain.Senior.HallOfFamePlayer? hallOfFamePlayer = null;

            if (xmlStaff.HofPlayerId > 0)
            {
                hallOfFamePlayer = await this.hallOfFameRepository.GetByHattrickIdAsync(xmlStaff.HofPlayerId);

                ArgumentNullException.ThrowIfNull(hallOfFamePlayer, nameof(hallOfFamePlayer));
            }

            if (staffMember == null)
            {
                staffMember = Domain.Senior.StaffMember.Create(
                    xmlStaff,
                    team,
                    hallOfFamePlayer);

                await this.staffMemberRepository.InsertAsync(staffMember);
            }
            else if (staffMember.HasChanged(xmlStaff))
            {
                staffMember.Update(hallOfFamePlayer);

                this.staffMemberRepository.Update(staffMember);
            }
        }
    }
}