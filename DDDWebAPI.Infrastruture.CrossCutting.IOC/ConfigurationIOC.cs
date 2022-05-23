﻿
using Autofac;
using DDDWebAPI.Application.Interfaces;
using DDDWebAPI.Application.Services;
using DDDWebAPI.Domain.Core.Interfaces.Repositorys;
using DDDWebAPI.Domain.Core.Interfaces.Services;
using DDDWebAPI.Domain.Services.Services;
using DDDWebAPI.Infrastruture.CrossCutting.Adapter.Interfaces;
using DDDWebAPI.Infrastruture.CrossCutting.Adapter.Map;
using DDDWebAPI.Infrastruture.Repository.Repositorys;

namespace DDDWebAPI.Infrastruture.CrossCutting.IOC
{
    public class ConfigurationIOC
    {
        public static void Load(ContainerBuilder builder)
        {
            #region Registra IOC

            #region IOC Application
            builder.RegisterType<ApplicationServiceFeira>().As<IApplicationServiceFeira>();
            #endregion

            #region IOC Services
            builder.RegisterType<ServiceFeira>().As<IServiceFeira>();
            #endregion

            #region IOC Repositorys SQL
            builder.RegisterType<RepositoryFeira>().As<IRepositoryFeira>();
            #endregion

            #region IOC Mapper
            builder.RegisterType<MapperFeira>().As<IMapperFeira>();
            #endregion

            #endregion

        }
    }
}
