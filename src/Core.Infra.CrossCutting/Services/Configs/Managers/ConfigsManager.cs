using Core.Infra.CrossCutting.Interfaces.Services.Configs;
using Core.Infra.CrossCutting.Interfaces.Services.Configs.Managers;
using Core.Infra.CrossCutting.Services.Configs.Collections;

namespace Core.Infra.CrossCutting.Services.Configs.Managers
{
    public class ConfigsManager : IConfigsManager
    {
        private readonly ConfigurationCollection _configurations;

        public ConfigsManager()
        {
            _configurations = new ConfigurationCollection();
        }

        public void Add(IConfiguration configuration)
        {
            _configurations.Add(configuration); 
        }

        public IConfiguration Get(string name)
        {
            var results = _configurations.Where(c => c.Name.Equals(name));

            if(results.Any())
            {
                return results.First();
            }

            return null;
        }
    }
}
