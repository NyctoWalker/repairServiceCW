using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repairServiceCW.ViewModels
{
    public class RepairServiceVM : INotifyPropertyChanged
    {
        // ObservableCollection OrdersList
        // Order SelectedOrder
        
        public RepairServiceVM singleton;

        public RepairServiceVM()
        {
            singleton = this;


        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
