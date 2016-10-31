using System.Collections.Generic;
using AssemblyPatcher.MethodPatcher.Logging.Patches;
using AssemblyPatcher.Wpf.Helpers;
using AssemblyPatcher.Wpf.ViewModels;
using Caliburn.Micro;
using PropertyChanged;


namespace AssemblyPatcher.Wpf.DesignMocks
{
    [ImplementPropertyChanged]
    public class ShellViewModelMock : PropertyChangedBase
    {
        public string MainWindowTitle => @"Assembly Patcher - C:\tests\Dummy Assembly.dll";

        private ClassViewModel _selectedClass;
        
        [DependsOn(nameof(Patches))]
        public int PatchesCount => Patches.Count;
        public IObservableCollection<PatchViewModel> Patches { get; set; }

        [DependsOn(nameof(Classes))]
        public int ClassesCount => Classes.Count;
        public IObservableCollection<ClassViewModel> Classes { get; set; }

        public ClassViewModel SelectedClass
        {
            get { return _selectedClass; }
            set
            {
                if (value != null)
                {
                    var methods = MethodListsHelper.GetMethodLists(ClassHelper.GetMethods(value.ClassType));
                    value.MethodLists = new BindableCollection<MethodListViewModel>(methods);
                }
                _selectedClass = value;
            }
        }

        public ShellViewModelMock()
        {
            Patches = new BindableCollection<PatchViewModel>(new List<PatchViewModel>
            {
                new PatchViewModel(typeof(LogMethodNamePatch)),
                new PatchViewModel(typeof(LogMethodParametersPatch)),
                new PatchViewModel(typeof(LogMethodResultPatch))
            });

            Classes = new BindableCollection<ClassViewModel>(new List<ClassViewModel>
            {
                new ClassViewModel(typeof(ShellViewModel)),
                new ClassViewModel(typeof(LogMethodParametersPatch)),
                new ClassViewModel(typeof(LogMethodResultPatch)),
                new ClassViewModel(typeof(LogMethodResultPatch)),
                new ClassViewModel(typeof(LogMethodResultPatch)),
                new ClassViewModel(typeof(LogMethodResultPatch)),
                new ClassViewModel(typeof(LogMethodResultPatch)),
                new ClassViewModel(typeof(LogMethodResultPatch))
            });

            MethodListsHelper.GetMethodLists(ClassHelper.GetMethods(typeof(ShellViewModel)));
        }
    }
}
