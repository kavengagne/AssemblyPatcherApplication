using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Caliburn.Micro;
using PropertyChanged;


namespace AssemblyPatcher.Wpf.ViewModels
{
    [ImplementPropertyChanged]
    public class PatchViewModel : PropertyChangedBase
    {
        public Type Type { get; set; }
        public string PatchName { get; set; }
        public string Description { get; set; }
        public bool IsChecked { get; set; }

        
        public PatchViewModel(Type type)
        {
            Type = type;
            PatchName = type.Name;
            Description = GetPatchDescription(type);
        }


        private static string GetPatchDescription(MemberInfo member)
        {
            var descriptionAttribute = member.CustomAttributes.FirstOrDefault(IsDescriptionAttribute);
            return (string)descriptionAttribute?.ConstructorArguments[0].Value;
        }

        private static bool IsDescriptionAttribute(CustomAttributeData attribute)
        {
            return attribute.AttributeType.FullName == typeof(DescriptionAttribute).FullName &&
                   attribute.ConstructorArguments.Count >= 1;
        }
    }
}
