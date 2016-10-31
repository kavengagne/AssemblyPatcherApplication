using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using AssemblyPatcher.Wpf.ViewModels;


namespace AssemblyPatcher.Wpf.Views
{
    public partial class ShellView
    {
        public ShellView()
        {
            InitializeComponent();
        }

        private void Classes_OnDragOver(object sender, DragEventArgs e)
        {
            bool dropEnabled = false;
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var validFileNames = GetValidFileNames(e.Data);
                dropEnabled = validFileNames.Any();
            }
            e.Effects = dropEnabled ? DragDropEffects.Copy : DragDropEffects.None;
            e.Handled = true;
        }

        private void Classes_OnDrop(object sender, DragEventArgs e)
        {
            var validFileNames = GetValidFileNames(e.Data);
            // TODO: KG - Should support multiple assemblies loaded at the same time (TreeView for Classes)
            var file = validFileNames.FirstOrDefault();
            if (file != null)
            {
                var viewModel = DataContext as ShellViewModel;
                viewModel?.LoadAssemblyClasses(file);
            }
        }

        private static IEnumerable<string> GetValidFileNames(IDataObject data)
        {
            var filenames = data.GetData(DataFormats.FileDrop) as string[] ?? new string[0];
            var validFiles = filenames.Where(file =>
            {
                var ext = Path.GetExtension(file);
                return ".dll".Equals(ext, StringComparison.InvariantCultureIgnoreCase) ||
                       ".exe".Equals(ext, StringComparison.InvariantCultureIgnoreCase);
            });
            return validFiles;
        }
    }
}
