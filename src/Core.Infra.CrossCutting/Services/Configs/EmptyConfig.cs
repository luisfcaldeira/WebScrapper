using Core.Infra.CrossCutting.Interfaces.Services.Configs.Enums;

namespace Core.Infra.CrossCutting.Services.Configs
{
    public class EmptyConfig : BaseConfiguration
    {
        public override string Description { get; }

        public override LogicType LogicType
        {
            get
            {
                return LogicType.Disjunctive;
            }
        }

        public EmptyConfig(string description)
        {
            Description = description;
        }
    }
}
