namespace Hyperar.HPA.Infrastructure.Strategies.XmlFileDataPersister
{
    using System;
    using Application.Hattrick.Interfaces;
    using Application.Interfaces;
    using Domain.Interfaces;
    using Hattrick = Application.Hattrick.StaffAvatars;

    public class StaffAvatars : XmlFileDataPersisterBase, IXmlFileDataPersisterStrategy
    {
        private readonly IDatabaseContext databaseContext;

        private readonly IRepository<Domain.Senior.StaffMemberAvatarLayer> staffMemberAvatarLayerRepository;

        private readonly IHattrickRepository<Domain.Senior.StaffMember> staffMemberRepository;

        public StaffAvatars(
            IDatabaseContext databaseContext,
            IHattrickRepository<Domain.Senior.StaffMember> staffMemberRepository,
            IRepository<Domain.Senior.StaffMemberAvatarLayer> staffMemberAvatarLayerRepository)
        {
            this.databaseContext = databaseContext;
            this.staffMemberRepository = staffMemberRepository;
            this.staffMemberAvatarLayerRepository = staffMemberAvatarLayerRepository;
        }

        public override async Task PersistDataAsync(IXmlFile file)
        {
            try
            {
                if (file is Hattrick.HattrickData xmlEntity)
                {
                    await this.ProcessAvatarsAsync(xmlEntity);
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

        private static void ProcessStaffMemberAvatar(Hattrick.Avatar avatar, Domain.Senior.StaffMember staffMember)
        {
            uint layerIndex = 1;

            staffMember.AvatarLayers.Add(new Domain.Senior.StaffMemberAvatarLayer
            {
                Index = layerIndex,
                XCoordinate = 0,
                YCoordinate = 0,
                ImageUrl = NormalizeUrl(avatar.BackgroundImage),
            });

            foreach (Hattrick.Layer curLayer in avatar.Layers)
            {
                layerIndex++;

                staffMember.AvatarLayers.Add(new Domain.Senior.StaffMemberAvatarLayer
                {
                    Index = layerIndex,
                    XCoordinate = curLayer.X,
                    YCoordinate = curLayer.Y,
                    ImageUrl = NormalizeUrl(curLayer.Image)
                });
            }
        }

        private async Task ProcessAvatarsAsync(Hattrick.HattrickData xmlEntity)
        {
            foreach (Hattrick.Staff curStaffMember in xmlEntity.StaffMembers)
            {
                Domain.Senior.StaffMember? staffmember = await this.staffMemberRepository.GetByHattrickIdAsync(curStaffMember.StaffId);

                ArgumentNullException.ThrowIfNull(staffmember, nameof(staffmember));

                bool mustDeleteAvatar = false;

                List<string> xmlAvatarLayers = new List<string>(curStaffMember.Avatar.Layers.Select(x => NormalizeUrl(x.Image)).ToArray())
                {
                    NormalizeUrl(curStaffMember.Avatar.BackgroundImage),
                };

                mustDeleteAvatar = staffmember.AvatarLayers.Select(x => x.ImageUrl)
                                                      .Except(xmlAvatarLayers)
                                                      .Any();

                if (mustDeleteAvatar)
                {
                    List<int> layerIdsToDelete = staffmember.AvatarLayers.Select(x => x.Id).ToList();

                    await this.staffMemberAvatarLayerRepository.DeleteRangeAsync(layerIdsToDelete);

                    staffmember.AvatarBytes = Array.Empty<byte>();

                    await this.databaseContext.SaveAsync();
                }

                if (staffmember.AvatarLayers.Count == 0)
                {
                    ProcessStaffMemberAvatar(curStaffMember.Avatar, staffmember);

                    staffmember.AvatarBytes = await BuildAvatarFromLayers(staffmember.AvatarLayers);
                }
            }

            await this.databaseContext.SaveAsync();
        }
    }
}