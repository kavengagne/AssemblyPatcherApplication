using Caliburn.Micro;


namespace AssemblyPatcher.Wpf.ViewModels.Base
{
    public class MethodTreeViewItemViewModelBase : PropertyChangedBase
    {
        public bool IsExpanded { get; set; }
        public bool IsParent { get; set; }
    }
}