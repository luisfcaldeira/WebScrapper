using Core.Infra.CrossCutting.Interfaces.Services.Configs.Enums;

namespace Core.Infra.CrossCutting.Interfaces.Services.Configs
{
    public interface IConfiguration
    {
        LogicType LogicType { get; }
        string Name { get; }
        string Description { get; }
    }
}
