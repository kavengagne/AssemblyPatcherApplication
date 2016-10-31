using System;
using AssemblyPatcher.MethodPatcher.Logging.Patches;
using Caliburn.Micro;


namespace AssemblyPatcher.Wpf.DesignMocks
{
    public class PatchViewModelMock : PropertyChangedBase
    {
        public bool IsChecked { get; } = true;
        public string PatchName { get; } = "LogMethodNamePatch";
        public Type Type { get; } = typeof(LogMethodNamePatch);


        public PatchViewModelMock()
        {
        }
    }
}
