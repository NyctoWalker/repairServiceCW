using AoIS_course_work;
using repairServiceCW.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repairServiceCW.ViewModels
{
    public class RepairServiceVM : INotifyPropertyChanged
    {
        #region Properties

        private ObservableCollection<Order> OrdersList;
        private Order SelectedOrder;

        private ObservableCollection<OrderElement> ElementsList;
        private OrderElement SelectedElement;

        public RepairServiceVM singleton;
        public DBManipulator dbManipulator;

        #endregion

        #region Commands

        private Command addOrderWindowCommand;
        private Command redactOrderWindowCommand;
        private Command deleteOrderWindowCommand;
        private Command orderElementsWindowCommand;

        // Cancel add, redact, delete for Orders and Elements, close Elements
        // For Orders and Elements add, redact, delete
        // Make word document

        /*
        private Command addElementWindowCommand;
        private Command redactElementWindowCommand;
        private Command deleteElementWindowCommand;


        */

        #endregion

        public RepairServiceVM()
        {
            singleton = this;
            dbManipulator = new();

        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
