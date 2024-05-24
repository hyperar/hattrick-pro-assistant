namespace Hyperar.HPA.Infrastructure.Strategies.FileDownloadTaskStepProcess.Persister
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Interfaces;
    using Domain.Interfaces;
    using Models = Shared.Models.Hattrick;

    public class StaffAvatars : PersisterBase, IFileDownloadTaskStepProcessStrategy
    {
        private readonly IDatabaseContext databaseContext;

        private readonly IHattrickRepository<Domain.Senior.StaffMember> staffMemberRepository;

        public StaffAvatars(
            IDatabaseContext databaseContext,
            IHattrickRepository<Domain.Senior.StaffMember> staffMemberRepository)
        {
            this.databaseContext = databaseContext;
            this.staffMemberRepository = staffMemberRepository;
        }

        public override async Task PersistFileAsync(IXmlFileDownloadTask fileDownloadTask, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(fileDownloadTask.XmlFile, nameof(fileDownloadTask.XmlFile));

            if (fileDownloadTask.XmlFile is Models.StaffAvatars.HattrickData file)
            {
                foreach (var xmlStaff in file.StaffMembers)
                {
                    await this.ProcessStaffAvatarAsync(xmlStaff);
                }
            }
            else
            {
                throw new ArgumentException(
                    string.Format(
                        Globalization.Translations.UnexpectedFileType,
                        typeof(Models.StaffAvatars.HattrickData).FullName,
                        fileDownloadTask.XmlFile.GetType().FullName));
            }

            await this.databaseContext.SaveAsync();
        }

        private async Task ProcessStaffAvatarAsync(Models.StaffAvatars.Staff xmlStaff)
        {
            var staffMember = await this.staffMemberRepository.GetByHattrickIdAsync(xmlStaff.StaffId);

            ArgumentNullException.ThrowIfNull(staffMember, nameof(staffMember));

            byte[] imageBytes = await BuildAvatarFromLayersAsync(xmlStaff.Avatar);

            staffMember.AvatarBytes = imageBytes;

            this.staffMemberRepository.Update(staffMember);
        }
    }
}