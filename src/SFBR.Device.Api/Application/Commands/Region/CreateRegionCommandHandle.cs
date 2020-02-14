using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SFBR.Device.Api.Application.Queries;
using SFBR.Device.Domain.AggregatesModel.RegionAggregate;

namespace SFBR.Device.Api.Application.Commands.Region
{
    public class CreateRegionCommandHandle : IRequestHandler<CreateRegionCommand, Queries.RegionModel>
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;

        public CreateRegionCommandHandle(IRegionRepository regionRepository, IMapper mapper)
        {
            _regionRepository = regionRepository ?? throw new ArgumentNullException(nameof(regionRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        }

        public async Task<RegionModel> Handle(CreateRegionCommand request, CancellationToken cancellationToken)
        {
            Domain.AggregatesModel.RegionAggregate.Region region = new Domain.AggregatesModel.RegionAggregate.Region(request.RegionCode, request.RegionName, request.ParentId, request.TenTantId, request.Description);
            _regionRepository.Add(region);
            var result = await _regionRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            if (!result) return null;
            return _mapper.Map<RegionModel>(region);
        }
    }
}
