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

        private readonly IRepository<Domain.StaffMemberAvatarLayer> staffMemberAvatarLayerRepository;

        private readonly IHattrickRepository<Domain.StaffMember> staffMemberRepository;

        public StaffAvatars(
            IDatabaseContext databaseContext,
            IHattrickRepository<Domain.StaffMember> staffMemberRepository,
            IRepository<Domain.StaffMemberAvatarLayer> staffMemberAvatarLayerRepository)
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

        private static void ProcessStaffMemberAvatar(Hattrick.Avatar avatar, Domain.StaffMember staffMember)
        {
            uint layerIndex = 1;

            staffMember.AvatarLayers.Add(new Domain.StaffMemberAvatarLayer
            {
                Index = layerIndex,
                XCoordinate = 0,
                YCoordinate = 0,
                ImageUrl = NormalizeUrl(avatar.BackgroundImage),
            });

            foreach (var curLayer in avatar.Layers)
            {
                layerIndex++;

                staffMember.AvatarLayers.Add(new Domain.StaffMemberAvatarLayer
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
            foreach (var curStaffMember in xmlEntity.StaffMembers)
            {
                var staffmember = await this.staffMemberRepository.GetByHattrickIdAsync(curStaffMember.StaffId);

                ArgumentNullException.ThrowIfNull(staffmember, nameof(staffmember));

                bool mustDeleteAvatar = false;

                var xmlAvatarLayers = new List<string>(curStaffMember.Avatar.Layers.Select(x => NormalizeUrl(x.Image)).ToArray())
                {
                    NormalizeUrl(curStaffMember.Avatar.BackgroundImage),
                };

                mustDeleteAvatar = staffmember.AvatarLayers.Select(x => x.ImageUrl)
                                                      .Except(xmlAvatarLayers)
                                                      .Any();

                if (mustDeleteAvatar)
                {
                    var layerIdsToDelete = staffmember.AvatarLayers.Select(x => x.Id).ToList();

                    await this.staffMemberAvatarLayerRepository.DeleteRangeAsync(layerIdsToDelete);

                    staffmember.Avatar = Array.Empty<byte>();

                    await this.databaseContext.SaveAsync();
                }

                if (staffmember.AvatarLayers.Count == 0)
                {
                    ProcessStaffMemberAvatar(curStaffMember.Avatar, staffmember);

                    staffmember.Avatar = await BuildAvatarFromLayers(staffmember.AvatarLayers);
                }
            }

            await this.databaseContext.SaveAsync();
        }
    }
}