﻿using AoIS_course_work;
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
    public class OrderElementsVM : INotifyPropertyChanged
    {
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

        public ObservableCollection<OrderElement> ElementsList { get; set; }

        public RepairServiceVM orderSingleton;
        public OrderElementsVM singleton;

        // For Elements add, redact, delete
        // Make word document

        public OrderElementsVM(RepairServiceVM OrderSingleton, Order order)
        {
            singleton = this;
            orderSingleton = OrderSingleton;

            SelectedOrder = order;
            ElementsList = new();
            FillElementsList();
        }


        private Command addElementWindowCommand;
        public Command AddElementWindowCommand
        {
            get => addElementWindowCommand ??= new Command(obj =>
            {
                var addWindow = new ElementAddWindow(singleton);
                addWindow.ShowDialog();
            });
        }

        private Command deleteElementWindowCommand;
        public Command DeleteElementWindowCommand
        {
            get => deleteElementWindowCommand ??= new Command(obj =>
            {
                OrderElement element = obj as OrderElement;
                if (element != null)
                {
                    var deleteWindow = new ElementDeleteWindow(singleton, element);
                    deleteWindow.ShowDialog();
                }
            });
        }

        private Command redactElementWindowCommand;
        public Command RedactElementWindowCommand
        {
            get => redactElementWindowCommand ??= new Command(obj =>
            {
                OrderElement element = obj as OrderElement;
                if (element != null)
                {
                    var redactWindow = new ElementRedactWindow(singleton, element);
                    redactWindow.ShowDialog();
                }
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


        public void FillElementsList()
        {
            ElementsList.Clear();

            var _elements = orderSingleton.dbManipulator.GetElementsList(SelectedOrder);
            if (_elements.Count() > 0)
            {
                foreach (OrderElement oe in _elements)
                {
                    ElementsList.Add(oe);
                }
            }
        }

        public event Action RequestClose;
        protected virtual void OnRequestClose()
        {
            RequestClose?.Invoke();
        }

        public void CloseWindow()
        {
            OnRequestClose();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
