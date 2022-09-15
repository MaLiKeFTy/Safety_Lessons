using System;
using System.Collections.Generic;
using System.Reflection;

namespace MonoServices.Core
{
    public static class MonoServiceFinder
    {
        static Assembly[] assemblies;
        static List<Type> types = new List<Type>();

        public static List<Type> Types => GetTypes();

        static List<Type> GetTypes()
        {
            if (assemblies == null)
            {
                List<Assembly> tempAssemlies = new List<Assembly>();

                assemblies = AppDomain.CurrentDomain.GetAssemblies();

                foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    if (assembly.FullName.Contains("MonoService") || assembly.FullName.Contains("Assembly-CSharp"))
                        tempAssemlies.Add(assembly);
                }

                assemblies = tempAssemlies.ToArray();
            }


            if (types.Count == 0)
            {
                for (int i = 0; i < assemblies.Length; i++)
                {
                    var foundTypes = new List<Type>();

                    foreach (var type in assemblies[i].GetTypes())
                    {
                        if (type.IsSubclassOf(typeof(MonoService)) && !type.IsGenericTypeDefinition)
                            foundTypes.Add(type);
                    }

                    types.AddRange(foundTypes);
                }

            }

            return types;
        }

    }
}

