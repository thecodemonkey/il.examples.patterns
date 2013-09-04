using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IL.Examples.Patterns.WPFRichClient.Common
{
    public class ViewModelBase<T> : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(GetPropertyName(expression)));
        }

        private static string GetPropertyName<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            UnaryExpression unaryExpression = expression.Body as UnaryExpression;

            MemberExpression member;

            if (unaryExpression != null)
                member = (MemberExpression)unaryExpression.Operand;
            else
                member = (MemberExpression)expression.Body;

            return member.Member.Name;
        }


        #region Validation

        private Dictionary<String, List<String>> errors = new Dictionary<string, List<string>>();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public System.Collections.IEnumerable GetErrors(string propertyName)
        {
            if (String.IsNullOrEmpty(propertyName) ||
                            !errors.ContainsKey(propertyName)) return null;
            return errors[propertyName];
        }

        public bool HasErrors
        {
            get { return errors.Count > 0; }
        }
        #endregion
    }

    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
