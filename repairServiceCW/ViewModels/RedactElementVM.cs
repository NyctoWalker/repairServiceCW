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
    public class RedactElementVM : INotifyPropertyChanged
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

        public string FormatDate
        {
            get
            {
                return Element.ElementEndDate.ToString("dd MMM yyyy'г,' ddd", new CultureInfo("ru-RU"));
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


        public RedactElementVM(OrderElementsVM vm, OrderElement redactedElement)
        {
            singleton = vm;

            Element = new OrderElement
            {
                ElementId = redactedElement.ElementId,
                ElementName = redactedElement.ElementName,
                ElementEndDate = redactedElement.ElementEndDate,
                ElementPrice = redactedElement.ElementPrice,
                ElementQuantity = redactedElement.ElementQuantity,
                CodeElement = redactedElement.CodeElement,
                CodeElementNavigation = redactedElement.CodeElementNavigation
            };
            
            Order = vm.SelectedOrder;

            TypesList = new();
            foreach (ElementType et in vm.orderSingleton.dbManipulator.GetElementTypeList())
            {
                TypesList.Add(et);
                if (et.ElementCode == Element.CodeElement)
                    SelectedType = et;
            }
        }


        private Command redactElementCommand;
        public Command RedactElementCommand
        {
            get => redactElementCommand ??= new Command(obj =>
            {
                OrderElement element = obj as OrderElement;

                if (element != null)
                {
                    if (!HasQuantity)
                        Element.ElementQuantity = null;
                    singleton.orderSingleton.dbManipulator.RedactElementRecord(element);
                    singleton.FillElementsList();
                }
                CloseWindow();
            });
        }

        private Command deleteElementCommand;
        public Command DeleteElementCommand
        {
            get => deleteElementCommand ??= new Command(obj =>
            {
                OrderElement element = obj as OrderElement;

                if (element != null)
                {
                    singleton.orderSingleton.dbManipulator.DeleteElementRecord(element);
                    singleton.FillElementsList();
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
