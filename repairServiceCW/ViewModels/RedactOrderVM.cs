using AoIS_course_work;
using repairServiceCW.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repairServiceCW.ViewModels
{
    public class RedactOrderVM : INotifyPropertyChanged
    {
        private Order order;
        public Order Order
        {
            get { return order; }
            set
            {
                if (order != value)
                {
                    order = value;
                    OnPropertyChanged(nameof(Order));
                }
            }
        }
        public ObservableCollection<OrderStatus> StatusList { get; set; }

        private OrderStatus selectedStatus;
        public OrderStatus SelectedStatus
        {
            get { return selectedStatus; }
            set
            {
                if (selectedStatus != value)
                {
                    selectedStatus = value;
                    if (selectedStatus != null)
                        Order.CodeStatus = selectedStatus.StatusCode;
                    OnPropertyChanged(nameof(SelectedStatus));
                }
            }
        }

        public string FormatDate
        {
            get
            {
                return Order.OrderTakeDate.ToString("dd MMM yyyy'г,' ddd", new CultureInfo("ru-RU"));
            }
        }
            

        private RepairServiceVM singleton;

        public event Action RequestClose;
        protected virtual void OnRequestClose()
        {
            RequestClose?.Invoke();
        }

        public void CloseWindow()
        {
            OnRequestClose();
        }


        public RedactOrderVM(RepairServiceVM vm, Order redactedOrder)
        {
            singleton = vm;
            Order = new Order
            {
                OrderId = redactedOrder.OrderId,
                OrderCode = redactedOrder.OrderCode,
                OrderDescription = redactedOrder.OrderDescription,
                OrderDevice = redactedOrder.OrderDevice,
                OrderDeviceModel = redactedOrder.OrderDeviceModel,
                OrderTakeDate = redactedOrder.OrderTakeDate,
                CodeStatus = redactedOrder.CodeStatus
            };

            StatusList = new();
            foreach (OrderStatus os in vm.dbManipulator.GetStatusList())
            {
                StatusList.Add(os);
                if (os.StatusCode == Order.CodeStatus)
                    SelectedStatus = os;
            }
        }


        private Command redactOrderCommand;
        public Command RedactOrderCommand
        {
            get => redactOrderCommand ??= new Command(obj =>
            {
                Order order = obj as Order;

                if (order != null)
                {
                    singleton.dbManipulator.RedactOrderRecord(order);
                    singleton.FillOrdersList();
                }

                CloseWindow();
            });
        }

        private Command deleteOrderCommand;
        public Command DeleteOrderCommand
        {
            get => deleteOrderCommand ??= new Command(obj =>
            {
                Order order = obj as Order;

                if (order != null)
                {
                    singleton.dbManipulator.DeleteOrderRecord(order);
                    singleton.FillOrdersList();
                }

                CloseWindow();
            });
        }

        private Command cancelCommand;
        public Command CancelCommand
        {
            get => cancelCommand ??= new Command(obj =>
            {
                CloseWindow();
            });
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
