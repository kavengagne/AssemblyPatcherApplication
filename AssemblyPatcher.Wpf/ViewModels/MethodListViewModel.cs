using AssemblyPatcher.Wpf.ViewModels.Base;
using Caliburn.Micro;
using PropertyChanged;


namespace AssemblyPatcher.Wpf.ViewModels
{
    [ImplementPropertyChanged]
    public class MethodListViewModel : MethodTreeViewItemViewModelBase
    {
        public string Protection { get; set; }
        
        [DependsOn(nameof(Methods))]
        public int MethodsCount => Methods.Count;
        public IObservableCollection<MethodViewModel> Methods { get; set; }

        public MethodListViewModel(string protection)
        {
            Protection = protection;
            Methods = new BindableCollection<MethodViewModel>();
            IsExpanded = true;
            IsParent = true;
        }
    }
}