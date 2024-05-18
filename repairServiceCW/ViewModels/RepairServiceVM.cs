using AoIS_course_work;
using repairServiceCW.Models;
using repairServiceCW.Views;
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

        public ObservableCollection<Order> OrdersList { get; set; }

        public RepairServiceVM singleton;
        public DBManipulator dbManipulator;

        #endregion

        #region Commands

        // For Elements list

        #endregion

        public RepairServiceVM()
        {
            singleton = this;
            dbManipulator = new();
            
            OrdersList = new();

            FillOrdersList();
        }

        #region Commands

        private Command addOrderWindowCommand;
        public Command AddOrderWindowCommand
        {
            get => addOrderWindowCommand ??= new Command(obj =>
            {
                var addWindow = new OrderAddWindow(singleton);
                addWindow.ShowDialog();
            });
        }

        private Command deleteOrderWindowCommand;
        public Command DeleteOrderWindowCommand
        {
            get => deleteOrderWindowCommand ??= new Command(obj =>
            {
                Order order = obj as Order;
                if (order != null)
                {
                    var deleteWindow = new OrderDeleteWindow(singleton, order);
                    deleteWindow.ShowDialog();
                }
            });
        }

        private Command redactOrderWindowCommand;
        public Command RedactOrderWindowCommand
        {
            get => redactOrderWindowCommand ??= new Command(obj =>
            {
                Order order = obj as Order;
                if (order != null)
                {
                    var redactWindow = new OrderRedactWindow(singleton, order);
                    redactWindow.ShowDialog();
                }
            });
        }

        private Command orderElementsWindowCommand;
        public Command OrderElementsWindowCommand
        {
            get => orderElementsWindowCommand ??= new Command(obj =>
            {
                Order order = obj as Order;
                if (order != null)
                {
                    var elementsWindow = new ElementsWindow(singleton, order);
                    elementsWindow.ShowDialog();
                }
            });
        }

        #endregion


        public void FillOrdersList()
        {
            OrdersList.Clear();

            var _orders = dbManipulator.GetOrdersList();
            if (_orders.Count() > 0)
            {
                foreach (Order o in _orders)
                {
                    OrdersList.Add(o);
                }
            }
        }

        public void FillElementsList()
        { 
            // Заполнить элементы для заказа
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
