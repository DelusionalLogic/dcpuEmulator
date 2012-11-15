using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using PluginInterface;

namespace dcpuEmulator
{
    public class PluginHandler
    {
        readonly Computer computer;

        public PluginHandler(Computer computer)
        {
            this.computer = computer;
        }

        public List<T> loadPluginsInFolder<T>(string path)
        {
            var pluginList = new List<T>();
            foreach (var fileName in Directory.GetFiles(path))
            {
                var file = new FileInfo(fileName);

                if(file.Extension == ".dll")
                {
                    var plugin = loadPlugin<T>(fileName);
                    if (plugin != null)
                        pluginList.Add(plugin);
                }
            }
            AdvConsole.Log(string.Format("Loaded {0} plugins of type {1}", pluginList.Count, typeof(T).Name));
            return pluginList;
        }

        public T loadPlugin<T>(string path)
        {
            Assembly assembly = Assembly.LoadFrom(path);
            Type pluginType = typeof(T);

            foreach (Type type in assembly.GetTypes())
            {
                if(type.IsPublic && !type.IsAbstract)
                {
                    //Gets a type object of the interface we need the plugins to match
					Type typeInterface = type.GetInterface(string.Format("{0}", pluginType), true);
					//Make sure the interface we want to use actually exists
                    if (typeInterface != null)
                    {
                        var plugin = (T) Activator.CreateInstance(assembly.GetType(type.ToString()));

                        ((IPlugin) plugin).Host = computer;
                        ((IPlugin) plugin).initialize();
                        return plugin;
                    }
                }
            }
            return default(T);
        }
    }
}
