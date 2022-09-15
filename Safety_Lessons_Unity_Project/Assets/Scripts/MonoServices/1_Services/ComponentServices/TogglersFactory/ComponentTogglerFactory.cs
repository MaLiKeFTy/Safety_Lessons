using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MonoServices.Components
{
    public static class ComponentTogglerFactory
    {
        public static List<ComponentToggler> DisableAllCompsCommand()
        {
            List<ComponentToggler> disablerList = new List<ComponentToggler>();

            disablerList.Clear();

            var assembly = Assembly.GetAssembly(typeof(ComponentToggler));

            var allDisablerTypes = assembly.GetTypes().Where(t => typeof(ComponentToggler).IsAssignableFrom(t) && t.IsAbstract == false && t.Name != nameof(ComponentToggler));

            foreach (var disablerType in allDisablerTypes)
            {
                ComponentToggler disabler = Activator.CreateInstance(disablerType) as ComponentToggler;
                disabler.TogglerType = disabler.GetType().Name;
                disablerList.Add(disabler);
            }

            return disablerList;
        }
    }
}