using System;
using System.Linq;
using AssemblyPatcher.Wpf.Helpers;
using Caliburn.Micro;
using PropertyChanged;


namespace AssemblyPatcher.Wpf.ViewModels
{
    [ImplementPropertyChanged]
    public class ClassViewModel : PropertyChangedBase
    {
        private IObservableCollection<MethodListViewModel> _methodLists;
        public string ClassName { get; set; }
        public Type ClassType { get; set; }
        public bool IsSelected { get; set; }

        [DependsOn(nameof(MethodLists))]
        public int MethodsCount => MethodLists.Aggregate(0, (acc, list) => acc + list.MethodsCount);

        public IObservableCollection<MethodListViewModel> MethodLists
        {
            get { return _methodLists; }
            set
            {
                _methodLists = value;
                MethodLists.RemoveRange(MethodLists.Where(m => m.MethodsCount == 0).ToList());
            }
        }


        public ClassViewModel(Type type)
        {
            ClassName = type.Name;
            ClassType = type;
            MethodLists = new BindableCollection<MethodListViewModel>(MethodListsHelper.GetMethodLists());
        }
    }
}