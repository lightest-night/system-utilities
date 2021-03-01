using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LightestNight.Utilities.Extensions
{
    public static class ExtendsAssembly
    {
        /// <summary>
        /// Gets all instances of the given Interface in the Assembly
        /// </summary>
        /// <param name="assembly">The assembly to search for Interface instances in</param>
        /// <typeparam name="T">The type of the Interface to look for instances of</typeparam>
        /// <returns>Collection of types that implement the given Interface</returns>
        public static IEnumerable<Type> GetInstancesOfInterface<T>(this Assembly assembly)
            => GetInstancesOfInterface(assembly, typeof(T));

        /// <summary>
        /// Gets all instances of the given Interface in the Assembly
        /// </summary>
        /// <param name="assembly">The assembly to search for Interfaces instances in</param>
        /// <param name="interfaceType">The type of the Interface to look for instances of</param>
        /// <returns>Collection of types that implement the given Interface</returns>
        public static IEnumerable<Type> GetInstancesOfInterface(this Assembly assembly, Type interfaceType)
            => assembly.GetTypes()
                .Where(t => interfaceType.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);
    }
}