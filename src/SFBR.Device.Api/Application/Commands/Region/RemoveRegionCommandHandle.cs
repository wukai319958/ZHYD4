using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SFBR.Device.Domain.AggregatesModel.RegionAggregate;

namespace SFBR.Device.Api.Application.Commands.Region
{
    public class RemoveRegionCommandHandle : IRequestHandler<RemoveRegionCommand, bool>
    {
        private readonly IRegionRepository _regionRepository;
        public RemoveRegionCommandHandle(IRegionRepository regionRepository)
        {
            _regionRepository = regionRepository ?? throw new ArgumentNullException(nameof(regionRepository));

        }
        public async Task<bool> Handle(RemoveRegionCommand request, CancellationToken cancellationToken)
        {
            Domain.AggregatesModel.RegionAggregate.Region region = await _regionRepository.GetAsync(request.Id);
            _regionRepository.Delete(region);
            var result = await _regionRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return result;
        }
    }
}
