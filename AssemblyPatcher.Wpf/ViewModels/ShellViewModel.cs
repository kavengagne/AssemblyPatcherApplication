using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using AssemblyPatcher.Wpf.Helpers;
using Caliburn.Micro;
using Microsoft.Win32;
using PropertyChanged;


namespace AssemblyPatcher.Wpf.ViewModels
{
    // TODO: KG - Dont forget to allow passing patch parameters
    [ImplementPropertyChanged]
    public class ShellViewModel : PropertyChangedBase
    {
        private ClassViewModel _selectedClass;
        public string MainWindowTitle { get; set; }

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

        
        public ShellViewModel()
        {
            MainWindowTitle = GetWindowTitle("No assembly loaded");

            _selectedClass = new ClassViewModel(typeof(Enum));

            InitializePatchesList(PatchHelper.GetAllPatches());
            InitializeClassesList();
        }

        public void OpenAssembly()
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.Title = "Select a .Net assembly or a .Net executable";
            fileDialog.Filter = ".Net Assemblies (.dll, .exe)|*.dll;*.exe";
            if (fileDialog.ShowDialog(Application.Current.MainWindow).GetValueOrDefault(false))
            {
                LoadAssemblyClasses(fileDialog.FileName);
            }
        }

        public void SaveAssembly()
        {
            
        }

        public void LoadAssemblyClasses(string fileName)
        {
            MainWindowTitle = GetWindowTitle(fileName);
            InitializeClassesList(ClassHelper.GetAllClasses(fileName));
        }

        private void InitializePatchesList(IList<PatchViewModel> patches = null)
        {
            Patches = new BindableCollection<PatchViewModel>(patches ?? new List<PatchViewModel>());
            Patches.CollectionChanged -= PatchesOnCollectionChanged;
            Patches.CollectionChanged += PatchesOnCollectionChanged;
        }

        private void PatchesOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            NotifyOfPropertyChange(() => PatchesCount);
        }

        private void InitializeClassesList(IList<ClassViewModel> classes = null)
        {
            Classes = new BindableCollection<ClassViewModel>(classes ?? new List<ClassViewModel>());
            Classes.CollectionChanged -= ClassesOnCollectionChanged;
            Classes.CollectionChanged += ClassesOnCollectionChanged;
        }

        private void ClassesOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            NotifyOfPropertyChange(() => ClassesCount);
        }

        private static string GetWindowTitle(string title)
        {
            return $"Assembly Patcher - {title}";
        }
    }
}
