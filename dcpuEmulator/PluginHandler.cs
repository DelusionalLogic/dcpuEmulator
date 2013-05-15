using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using PluginInterface;

namespace dcpuEmulator
{
    /// <summary>
    /// Controls the plugins
    /// </summary>
    public class PluginHandler
    {
        readonly Computer computer;

        public PluginHandler(Computer computer)
        {
            this.computer = computer;
        }

        /// <summary>
        /// Load all the plugins in a folder
        /// </summary>
        /// <typeparam name="T">Interface of plugin</typeparam>
        /// <param name="path">The path of the plugins</param>
        /// <returns>A list of the interfaces in the folder</returns>
        public List<T> loadPluginsInFolder<T>(string path)
        {
            var pluginList = new List<T>();
            //loop through all the files in the folder
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

        /// <summary>
        /// Load a plugin
        /// </summary>
        /// <typeparam name="T">Interface of the plugin</typeparam>
        /// <param name="path">The path to the plugin</param>
        /// <returns>A loaded interface</returns>
        public T loadPlugin<T>(string path)
        {
            //Load the plugin
            Assembly assembly = Assembly.LoadFrom(path);
            //Get the type of the interface
            Type pluginType = typeof(T);
            
            //Loop through all the types in the plugin
            foreach (Type type in assembly.GetTypes())
            {
                if(type.IsPublic && !type.IsAbstract)
                {
                    //Gets a type object of the interface we need the plugins to match
					Type typeInterface = type.GetInterface(string.Format("{0}", pluginType), true);
					//Make sure the interface we want to use actually exists
                    if (typeInterface != null)
                    {
                        //Create an instace of this class
                        var plugin = (T) Activator.CreateInstance(assembly.GetType(type.ToString()));

                        //Tell the plugin about the emulator host
                        ((IPlugin) plugin).Host = computer;
                        return plugin;
                    }
                }
            }
            //Return null
            return default(T);
        }
    }
}
