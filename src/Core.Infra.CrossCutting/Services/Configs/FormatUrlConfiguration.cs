using Core.Infra.CrossCutting.Interfaces.Services.Configs;
using Core.Infra.CrossCutting.Interfaces.Services.Configs.Enums;

namespace Core.Infra.CrossCutting.Services.Configs
{
    public class FormatUrlConfiguration : BaseConfiguration, IConfiguration
    {

        public FormatUrlConfiguration(string description)
        {
            Description = description;
        }

        public override string Description { get; }

        public override LogicType LogicType
        {
            get
            {
                return LogicType.Conjunctive;
            }
        }
    }
}
