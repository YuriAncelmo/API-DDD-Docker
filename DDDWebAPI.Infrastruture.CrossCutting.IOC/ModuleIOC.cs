using Autofac;
namespace DDDWebAPI.Infrastruture.CrossCutting.IOC
{
    public class ModuleIOC:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            #region Carrega o IOC 
            ConfigurationIOC.Load(builder);
            #endregion
        }
    }
}
