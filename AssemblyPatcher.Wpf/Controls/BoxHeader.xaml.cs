using System.Windows;
using System.Windows.Controls;


namespace AssemblyPatcher.Wpf.Controls
{
    public partial class BoxHeader : UserControl
    {
        public BoxHeader()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(BoxHeader), new PropertyMetadata(default(string)));
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        
        public static readonly DependencyProperty CountProperty = DependencyProperty.Register("Count", typeof(int), typeof(BoxHeader), new PropertyMetadata(default(int)));
        public int Count
        {
            get { return (int)GetValue(CountProperty); }
            set { SetValue(CountProperty, value); }
        }
    }
}
