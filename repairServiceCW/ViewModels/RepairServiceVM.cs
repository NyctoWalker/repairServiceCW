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

        private Order selectedOrder;
        public Order SelectedOrder
        {
            get { return selectedOrder; }
            set
            {
                if (selectedOrder != value)
                {
                    selectedOrder = value;
                    OnPropertyChanged(nameof(SelectedOrder));
                }
            }
        }

        public ObservableCollection<Order> OrdersList { get; set; }

        private OrderElement selectedElement;
        public OrderElement SelectedElement
        {
            get { return selectedElement; }
            set
            {
                if (selectedElement != value)
                {
                    selectedElement = value;
                    OnPropertyChanged(nameof(SelectedElement));
                }
            }
        }

        public ObservableCollection<OrderElement> ElementsList { get; set; }

        public RepairServiceVM singleton;
        public DBManipulator dbManipulator;

        #endregion

        #region Commands

        // Close Elements
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
            
            OrdersList = new();
            ElementsList = new();

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
                    System.Windows.MessageBox.Show($"Elements: {order.OrderCode} \n{order.OrderElements.Count()} элементов");
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

                SelectedOrder = (SelectedOrder == null) ? OrdersList[0] : SelectedOrder;
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
