using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFBR.Device.Domain.AggregatesModel.RegionAggregate;

namespace SFBR.Device.Api.Application.Commands.Region
{
    public class SetRegionNameCommandHandle : IRequestHandler<SetRegionNameCommand, bool>
    {
        private readonly IRegionRepository _regionRepository;

        public SetRegionNameCommandHandle(IRegionRepository regionRepository)
        {
            _regionRepository = regionRepository ?? throw new ArgumentNullException(nameof(regionRepository));

        }
        public async Task<bool> Handle(SetRegionNameCommand request, CancellationToken cancellationToken)
        {
            var region = await _regionRepository.GetAsync(request.Id);
            if (region == null) return false;
            region.SetName(request.RegionName);
            _regionRepository.Patch(region);
            return await _regionRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
