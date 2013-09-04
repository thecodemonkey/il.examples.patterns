using IL.Examples.Patterns.WPFRichClient.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IL.Examples.Patterns.WPFRichClient.ViewModels
{
    public class MainViewModel : ViewModelBase<MainViewModel>
    {
        public MainViewModel() 
        {
            this.IsLoginVisible = false;
            this.IsUsersVisible = false;
        }

        private bool _isLoginVisible;
        public bool IsLoginVisible 
        { 
            get
            {
                return this._isLoginVisible;
            }
            set 
            {
                this._isLoginVisible = true;
                this.OnPropertyChanged(p => p.IsLoginVisible);
            }
        }

        private bool _isUsersVisible;
        public bool IsUsersVisible 
        {
            get 
            {
                return this._isUsersVisible;
            }
            set 
            {
                this._isUsersVisible = value;
                this.OnPropertyChanged(p => p.IsUsersVisible);
            } 
        }
    }
}
