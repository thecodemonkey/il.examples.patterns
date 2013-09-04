using IL.Examples.Patterns.WPFRichClient.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IL.Examples.Patterns.WPFRichClient.Views
{
    public partial class Editor : UserControl
    {
        public Editor()
        {
            InitializeComponent();
            this.DataContext = ViewModelLocator.EditorVM;
        }
    }
}
