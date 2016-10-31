using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AssemblyPatcher.Wpf.ViewModels;


namespace AssemblyPatcher.Wpf.Helpers
{
    public static class ClassHelper
    {
        public static IList<ClassViewModel> GetAllClasses(string fileName)
        {
            var assembly = Assembly.LoadFile(fileName);
            var classes = assembly.ExportedTypes
                                  .Where(IsEligibleClass)
                                  .Select(t => new ClassViewModel(t))
                                  .OrderBy(t => t.ClassName)
                                  .ToList();
            return classes;
        }

        public static IList<MethodViewModel> GetMethods(Type type)
        {
            var methods = type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance)
                              .Where(IsEligibleMethod)
                              .Select(m => new MethodViewModel(m))
                              .OrderBy(m => m.MethodName)
                              .ToList();
            return methods;
        }


        private static bool IsEligibleClass(Type type)
        {
            return type.IsClass &&
                   !type.IsEnum && !type.IsPrimitive &&
                   !type.IsCOMObject && !type.IsImport && !type.IsPointer;
        }

        private static bool IsEligibleMethod(MethodInfo method)
        {
            return !method.IsAbstract && !method.IsSpecialName;
        }
    }
}