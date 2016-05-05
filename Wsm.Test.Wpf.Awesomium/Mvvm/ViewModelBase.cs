using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wsm.Test.Wpf.Awesomium.Mvvm
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Sets the specified ipnv.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ipnv">The ipnv.</param>
        /// <param name="value">The value.</param>
        /// <param name="ipn">The ipn.</param>
        protected void Set<T>(ref T ipnv, T value, string ipn)
        {
            if (Equals(ipnv, value))
                return;

            ipnv = value;
            OnPropertyChanged(ipn);
        }

        private void OnPropertyChanged(string pn)
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged(this, new PropertyChangedEventArgs(pn));
        }
    }
}
