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
    public class AddElementVM : INotifyPropertyChanged
    {
        private OrderElement element;
        public OrderElement Element
        {
            get { return element; }
            set
            {
                if (element != value)
                {
                    element = value;
                    OnPropertyChanged(nameof(Element));
                }
            }
        }

        public Order Order;

        public ObservableCollection<ElementType> TypesList { get; set; }

        private ElementType selectedType;
        public ElementType SelectedType
        {
            get { return selectedType; }
            set
            {
                if (selectedType != value)
                {
                    selectedType = value;
                    if (selectedType != null)
                    {
                        Element.CodeElement = selectedType.ElementCode;
                        HasQuantity = SelectedType.ElementType1 == "Работа" ? false : true;
                    }
                    

                    OnPropertyChanged(nameof(SelectedType));
                }
            }
        }

        private bool hasQuantity;
        public bool HasQuantity
        {
            get { return hasQuantity; }
            set
            {
                if (hasQuantity != value)
                {
                    hasQuantity = value;
                    OnPropertyChanged(nameof(HasQuantity));
                }
            }
        }

        private OrderElementsVM singleton;

        public event Action RequestClose;
        protected virtual void OnRequestClose()
        {
            RequestClose?.Invoke();
        }

        public void CloseWindow()
        {
            OnRequestClose();
        }


        public AddElementVM(OrderElementsVM vm)
        {
            singleton = vm;
            Order = vm.SelectedOrder;

            Element = new OrderElement
            {
                ElementEndDate = DateTime.Today
            };

            TypesList = new();
            foreach (ElementType et in vm.orderSingleton.dbManipulator.GetElementTypeList())
                TypesList.Add(et);
            SelectedType = TypesList[0];
        }


        private Command addElementCommand;
        public Command AddElementCommand
        {
            get => addElementCommand ??= new Command(obj =>
            {
                if (!HasQuantity)
                    Element.ElementQuantity = null;
                singleton.orderSingleton.dbManipulator.AddElementRecord(Element, Order);
                singleton.FillElementsList();

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
