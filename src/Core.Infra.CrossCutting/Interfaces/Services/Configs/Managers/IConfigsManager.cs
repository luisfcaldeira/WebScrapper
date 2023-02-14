namespace Core.Infra.CrossCutting.Interfaces.Services.Configs.Managers
{
    public interface IConfigsManager
    {
        IConfiguration Get(string name);
        void Add(IConfiguration configuration);
    }
}
