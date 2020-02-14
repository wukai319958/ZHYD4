using AutoMapper;
using MediatR;
using SFBR.Device.Domain.AggregatesModel.BrandAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
namespace SFBR.Device.Api.Application.Commands.Brand
{
    public class CreateBrandCommandHandle : IRequestHandler<CreateBrandCommand, Queries.BrandViewModel>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CreateBrandCommandHandle(IBrandRepository brandRepository, IMediator mediator, IMapper mapper)
        {
            _brandRepository = brandRepository ?? throw new ArgumentNullException(nameof(brandRepository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        public async Task<Queries.BrandViewModel> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            Domain.AggregatesModel.BrandAggregate.Brand brand = new Domain.AggregatesModel.BrandAggregate.Brand(brandName: request.BrandName, groupKey: request.GroupKey, tentantId: request.TentantId, description: request.Description);
            _brandRepository.Add(brand);
            var result = await _brandRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            if (!result) return null;
            return _mapper.Map<Queries.BrandViewModel>(brand);
        }
    }
}

