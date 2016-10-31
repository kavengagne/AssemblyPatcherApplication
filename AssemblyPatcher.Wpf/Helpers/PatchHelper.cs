using System;
using System.Collections.Generic;
using System.Linq;
using AssemblyPatcher.Core.Base;
using AssemblyPatcher.MethodPatcher.Logging.Patches;
using AssemblyPatcher.Wpf.ViewModels;


namespace AssemblyPatcher.Wpf.Helpers
{
    public static class PatchHelper
    {
        public static IList<PatchViewModel> GetAllPatches()
        {
            var patches = typeof(LogMethodNamePatch).Module.GetTypes()
                                                    .Where(t => !t.IsAbstract & HasIPatchInterface(t))
                                                    .Select(t => new PatchViewModel(t))
                                                    .OrderBy(t => t.PatchName)
                                                    .ToList();
            return patches;
        }

        private static bool HasIPatchInterface(Type type)
        {
            return type.GetInterfaces().Any(t => t == typeof(IPatch));
        }
    }
}