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
    public class AddOrderVM : INotifyPropertyChanged
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

        private RepairServiceVM singleton;

        public event Action RequestClose;
        protected virtual void OnRequestClose()
        {
            RequestClose?.Invoke();
        }


        public AddOrderVM(RepairServiceVM vm)
        {
            singleton = vm;

            Order = new Order
            {
                OrderTakeDate = DateTime.Today
            };

            StatusList = new();
            foreach (OrderStatus os in vm.dbManipulator.GetStatusList())
                StatusList.Add(os);
            SelectedStatus = StatusList[0];
        }

        public void CloseWindow()
        {
            OnRequestClose();
        }


        private Command addOrderCommand;
        public Command AddOrderCommand
        {
            get => addOrderCommand ??= new Command(obj =>
            {
                // Рандом заменить на нормальный счётчик
                Random rnd = new Random();

                Order.OrderCode = $"{Order.OrderDevice[0]}{Order.OrderTakeDate:ddMMyyyy}{rnd.Next(0, 10)}";

                singleton.dbManipulator.AddOrderRecord(Order);
                singleton.FillOrdersList();

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
