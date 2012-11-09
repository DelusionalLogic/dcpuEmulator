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
        readonly PluginService pluginHandler = new PluginService();

        public List<T> loadPluginsInFolder<T>(string path)
        {
            AdvConsole.Log(string.Format("Loading plugins in {0}", path));
            var pluginList = new List<T>();
            foreach (var fileName in Directory.GetFiles(path))
            {
                var file = new FileInfo(fileName);

                if(file.Extension == ".dll")
                    pluginList.Add(loadPlugin<T>(fileName));
            }
            AdvConsole.Log(string.Format("Loaded {0} plugins of type {1}", pluginList.Count, typeof(T)));
            return pluginList;
        }

        public T loadPlugin<T>(string path)
        {
            Assembly assembly = Assembly.LoadFrom(path);

            foreach (Type type in assembly.GetTypes())
            {
                if(type.IsPublic && !type.IsAbstract)
                {
                    //Gets a type object of the interface we need the plugins to match
					Type typeInterface = type.GetInterface("PluginInterface.IPlugin", true);
						
					//Make sure the interface we want to use actually exists
                    if (typeInterface != null)
                    {
                        var plugin = (T) Activator.CreateInstance(assembly.GetType(type.ToString()));

                        ((IPlugin) plugin).Host = pluginHandler;
                        ((IPlugin) plugin).initialize();

                        return plugin;
                    }
                }
            }
            return default(T);
        }
    }
}
