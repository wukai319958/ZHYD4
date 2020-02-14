using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Log.Api.Infrastructure.AutoMapperConfig
{
    public class AlarmLogProfile : Profile
    {
        public AlarmLogProfile()
        {
            CreateMap<Model.AlarmLog, SFBR.Log.Api.ViewModel.AlarmMdoel>();
            CreateMap<Model.AlarmLog, SFBR.Log.Api.ViewModel.ActionLogModel>();
        }
    }
}
