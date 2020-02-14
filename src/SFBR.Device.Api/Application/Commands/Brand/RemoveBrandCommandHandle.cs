using MediatR;
using SFBR.Device.Domain.AggregatesModel.BrandAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SFBR.Device.Api.Application.Commands.Brand
{
    public class RemoveBrandCommandHandle : IRequestHandler<RemoveBrandCommand, bool>
    {
        private readonly IBrandRepository _brandRepository;
        public RemoveBrandCommandHandle(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository ?? throw new ArgumentNullException(nameof(brandRepository));
        }

        public async Task<bool> Handle(RemoveBrandCommand request, CancellationToken cancellationToken)
        {
            Domain.AggregatesModel.BrandAggregate.Brand brand = await _brandRepository.GetAsync(request.Id);
            _brandRepository.Delete(brand);
            var result = await _brandRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return result;
        }
    }
}
