using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SmartPlaylist.Extensions
{
    public static class AssemblyExtensions
    {
        public static IEnumerable<Type> FindDerivedTypes<TBase>(this Assembly assembly)
        {
            var baseType = typeof(TBase);
            return assembly.GetTypes()
                .Where(t => t != baseType && !t.IsAbstract && baseType.IsAssignableFrom(t))
                .OrderBy(x => x.Name);
        }

        public static IEnumerable<TBase> FindAndCreateDerivedTypes<TBase>(this Assembly assembly)
        {
            return assembly
                .FindDerivedTypes<TBase>()
                .Select(Activator.CreateInstance)
                .OfType<TBase>()
                .ToArray();
        }
    }
}