using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Device.Api.Application.Commands.Brand
{
    public class RemoveBrandCommand : IRequest<bool>
    {
        /// <summary>
        /// 品牌Id
        /// </summary>
        public string Id { get; set; }
    }
}
