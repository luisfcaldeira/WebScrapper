using Core.Infra.CrossCutting.Interfaces.Services.Configs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infra.CrossCutting.Services.Configs.Collections
{
    public class ConfigurationCollection : Collection<IConfiguration>
    {
        public IList<IConfiguration> GetConfiguration(string name)
        {
            return Items.Where(item => item.Name.Equals(name)).ToList();
        }
    }
}
