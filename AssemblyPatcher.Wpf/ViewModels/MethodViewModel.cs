using System.Reflection;
using AssemblyPatcher.Wpf.ViewModels.Base;
using PropertyChanged;


namespace AssemblyPatcher.Wpf.ViewModels
{
    [ImplementPropertyChanged]
    public class MethodViewModel : MethodTreeViewItemViewModelBase
    {
        public MethodInfo MethodInfo { get; set; }
        public string MethodName { get; set; }
        public string Signature { get; set; }


        public MethodViewModel(MethodInfo methodInfo)
        {
            MethodInfo = methodInfo;
            MethodName = methodInfo.Name;
            Signature = GetMethodSignature(methodInfo);
        }

        private string GetMethodSignature(MethodInfo methodInfo)
        {
            var baseSignature = methodInfo.ToString();
            var staticStr = methodInfo.IsStatic ? "static " : "";
            return $"{staticStr}{baseSignature}";
        }
    }
}