using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Device.Api.Application.Commands.Region
{
    public class RemoveRegionCommand : IRequest<bool>
    {
        public RemoveRegionCommand(string id)
        {
            this.Id=id?? throw new ArgumentNullException(nameof(id)); 

        }
        public string Id { get; set; }
    }

}
