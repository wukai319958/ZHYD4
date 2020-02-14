using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Device.Api.Application.Workers
{
    public interface IWorker<T>
    {
        void Execute(T value, IServiceProvider services);
    }
}
