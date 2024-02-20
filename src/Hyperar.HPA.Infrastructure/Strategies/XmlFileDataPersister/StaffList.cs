namespace Hyperar.HPA.Infrastructure.Strategies.XmlFileDataPersister
{
    using System;
    using Application.Hattrick.Interfaces;
    using Application.Interfaces;
    using Domain.Interfaces;
    using Hattrick = Application.Hattrick.StaffList;

    public class StaffList : XmlFileDataPersisterBase, IXmlFileDataPersisterStrategy
    {
        private readonly IDatabaseContext databaseContext;

        private readonly IHattrickRepository<Domain.Senior.HallOfFamePlayer> hallOfFamePlayerRepository;

        private readonly IHattrickRepository<Domain.Senior.StaffMember> staffMemberRepository;

        public StaffList(
            IDatabaseContext databaseContext,
            IHattrickRepository<Domain.Senior.HallOfFamePlayer> hallOfFamePlayerRepository,
            IHattrickRepository<Domain.Senior.StaffMember> staffRepository)
        {
            this.databaseContext = databaseContext;
            this.hallOfFamePlayerRepository = hallOfFamePlayerRepository;
            this.staffMemberRepository = staffRepository;
        }

        public override async Task PersistDataAsync(IXmlFile file)
        {
            try
            {
                if (file is Hattrick.HattrickData xmlEntity)
                {
                    await this.ProcessStaffList(xmlEntity);
                }
                else
                {
                    throw new ArgumentException(file.GetType().FullName, nameof(file));
                }
            }
            catch
            {
                this.databaseContext.Cancel();

                throw;
            }
        }

        private async Task ProcessStaffList(Hattrick.HattrickData xmlFile)
        {
            foreach (var curStaffMember in xmlFile.StaffList.StaffMembers)
            {
                await this.ProcessStaffMemberAsync(curStaffMember);
            }

            await this.databaseContext.SaveAsync();
        }

        private async Task ProcessStaffMemberAsync(Hattrick.Staff xmlStaff)
        {
            var staffMember = await this.staffMemberRepository.GetByHattrickIdAsync(xmlStaff.StaffId);

            if (staffMember == null)
            {
                staffMember = new Domain.Senior.StaffMember
                {
                    HattrickId = xmlStaff.StaffId,
                    Name = xmlStaff.Name,
                    HiredOn = xmlStaff.HiredDate,
                    Type = xmlStaff.StaffType,
                    Level = xmlStaff.StaffLevel,
                    Salary = xmlStaff.Cost,
                };

                if (xmlStaff.HofPlayerId != 0)
                {
                    var hallOfFamePlayer = await this.hallOfFamePlayerRepository.GetByHattrickIdAsync(xmlStaff.HofPlayerId);

                    ArgumentNullException.ThrowIfNull(hallOfFamePlayer, nameof(hallOfFamePlayer));

                    staffMember.HallOfFamePlayer = hallOfFamePlayer;
                }

                await this.staffMemberRepository.InsertAsync(staffMember);
            }
            else
            {
                staffMember.HattrickId = xmlStaff.StaffId;
                staffMember.Name = xmlStaff.Name;
                staffMember.HiredOn = xmlStaff.HiredDate;
                staffMember.Type = xmlStaff.StaffType;
                staffMember.Level = xmlStaff.StaffLevel;
                staffMember.Salary = xmlStaff.Cost;

                if (xmlStaff.HofPlayerId != staffMember.HallOfFamePlayerId)
                {
                    if (xmlStaff.HofPlayerId != 0)
                    {
                        var hallOfFamePlayer = await this.hallOfFamePlayerRepository.GetByHattrickIdAsync(xmlStaff.HofPlayerId);

                        ArgumentNullException.ThrowIfNull(hallOfFamePlayer, nameof(hallOfFamePlayer));

                        staffMember.HallOfFamePlayer = hallOfFamePlayer;
                    }
                    else
                    {
                        staffMember.HallOfFamePlayer = null;
                        staffMember.HallOfFamePlayerId = null;
                    }
                }

                this.staffMemberRepository.Update(staffMember);
            }
        }
    }
}