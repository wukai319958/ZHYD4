using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Microsoft.AspNetCore.Mvc;
using SFBR.Log.Api.Controllers;
using SFBR.Log.Api.Queries;

namespace SFBR.Log.Api.Infrastructure
{
    public class ApplicationModule : Autofac.Module
    {
        public string QueriesConnectionString { get; }
        public ApplicationModule(string qconstr)
        {
            QueriesConnectionString = qconstr;

        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new AlarmQueries(new SqlConnection(QueriesConnectionString))).As<IAlarmQueries>().InstancePerDependency();
            builder.Register(c => new ActionQueries(new SqlConnection(QueriesConnectionString))).As<IActionQueries>().InstancePerDependency();
        }

    }
}
