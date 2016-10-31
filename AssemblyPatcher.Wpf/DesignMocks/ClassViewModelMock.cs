using Caliburn.Micro;


namespace AssemblyPatcher.Wpf.DesignMocks
{
    public class ClassViewModelMock : PropertyChangedBase
    {
        public string ClassName { get; } = "Calculator";


        public ClassViewModelMock()
        {
        }
    }
}
