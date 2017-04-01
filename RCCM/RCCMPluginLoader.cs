/* 
   Copyright 2013 Christoph Gattnar

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

	   http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RCCM
{
    /// <summary>
    /// Plugin loading framework from https://code.msdn.microsoft.com/windowsdesktop/Creating-a-simple-plugin-b6174b62
    /// </summary>
    public class RCCMPluginLoader
    {
        public static ICollection<IRCCMPlugin> LoadPlugins(string path)
        {
            string[] dllFileNames = null;
            if (Directory.Exists(path + "\\"))
            {
                dllFileNames = Directory.GetFiles(path, "*.dll");

                ICollection<Assembly> assemblies = new List<Assembly>(dllFileNames.Length);
                foreach (string dllFile in dllFileNames)
                {
                    Console.WriteLine(dllFile);
                    AssemblyName an = AssemblyName.GetAssemblyName(dllFile);
                    Assembly assembly = Assembly.Load(an);
                    assemblies.Add(assembly);
                }

                Type pluginType = typeof(IRCCMPlugin);
                ICollection<Type> pluginTypes = new List<Type>();
                foreach (Assembly assembly in assemblies)
                {
                    if (assembly != null)
                    {
                        Type[] types = assembly.GetTypes();

                        foreach (Type type in types)
                        {
                            if (type.IsInterface || type.IsAbstract)
                            {
                                continue;
                            }
                            else
                            {
                                if (type.GetInterface(pluginType.FullName) != null)
                                {
                                    pluginTypes.Add(type);
                                }
                            }
                        }
                    }
                }

                ICollection<IRCCMPlugin> plugins = new List<IRCCMPlugin>(pluginTypes.Count);
                foreach (Type type in pluginTypes)
                {
                    IRCCMPlugin plugin = (IRCCMPlugin)Activator.CreateInstance(type);
                    plugins.Add(plugin);
                    Console.WriteLine(plugin.Name);
                }

                return plugins;
            }

            return null;
        }
    }
}
