using Core.Infra.CrossCutting.Interfaces.Services.Configs;
using Core.Infra.CrossCutting.Interfaces.Services.Configs.Enums;

namespace Core.Infra.CrossCutting.Services.Configs
{
    public abstract class BaseConfiguration : IConfiguration
    {
        public string Name
        {
            get
            {
                return GetType().Name.Replace("Configuration", "");
            }
        }

        public abstract string Description { get; }
        public abstract LogicType LogicType { get; }
    }
}
