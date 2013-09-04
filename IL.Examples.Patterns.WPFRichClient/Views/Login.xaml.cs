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
using IL.Examples.Patterns.WPFRichClient.ViewModels;
using IL.Examples.Patterns.WPFRichClient.Common;

namespace IL.Examples.Patterns.WPFRichClient.Views
{
    public partial class Login : UserControl
    {
        public Login()
        {
            InitializeComponent();
            this.DataContext = ViewModelLocator.LoginVM;
        }
    }
}
