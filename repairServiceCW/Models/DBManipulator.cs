using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace repairServiceCW.Models
{
    public class DBManipulator
    {
        #region Orders
        public List<Order> GetOrdersList()
        {
            List<Order> output = new();
            using (repair_serviceContext db = new())
            {
                var orders = db.Orders.Include(o => o.OrderElements).ThenInclude(oe => oe.CodeElementNavigation).Include(o => o.CodeStatusNavigation);
                foreach (Order o in orders)
                {
                    output.Add(o);
                }
            }
            return output;
        }

        public void DeleteOrderRecord(Order o)
        {
            using (repair_serviceContext db = new())
            {
                // Удалить все элементы заказа
                foreach (OrderElement oe in o.OrderElements)
                {
                    DeleteElementRecord(oe);
                }

                //Удалить заказ
                db.Remove(o);
                db.SaveChanges();
            }
        }

        public void AddOrderRecord(Order o)
        {
            using (repair_serviceContext db = new())
            { 
                Order newOrder = new Order
                { 
                    OrderCode = o.OrderCode,
                    OrderTakeDate = o.OrderTakeDate,
                    OrderDescription = o.OrderDescription,
                    OrderDevice = o.OrderDevice,
                    OrderDeviceModel = o.OrderDeviceModel,
                    CodeStatus = o.CodeStatus
                };
                
                db.Add(newOrder);
                db.SaveChanges();
            }
        }

        public void RedactOrderRecord(Order o)
        {
            using (repair_serviceContext db = new())
            {
                var existingOrder = db.Orders.FirstOrDefault(x => x.OrderId == o.OrderId);

                if (existingOrder != null)
                {
                    existingOrder.OrderCode = o.OrderCode;
                    existingOrder.OrderTakeDate = o.OrderTakeDate;
                    existingOrder.OrderDescription = o.OrderDescription;
                    existingOrder.OrderDevice = o.OrderDevice;
                    existingOrder.OrderDeviceModel = o.OrderDeviceModel;
                    existingOrder.CodeStatus = o.CodeStatus;

                    db.SaveChanges();
                }
            }
        }

        #endregion

        #region OrderElements
        public List<OrderElement> GetElementsList(Order o)
        {
            List<OrderElement> output = new();

            using (repair_serviceContext db = new())
            {
                var existingOrder = db.Orders.FirstOrDefault(x => x.OrderId == o.OrderId);
                if (existingOrder == null)
                    return output;
                
                var elements = db.OrderElements.Where(x => x.IdOrder == existingOrder.OrderId).Include(oe => oe.CodeElementNavigation);
                foreach (OrderElement e in elements)
                {
                    output.Add(e);
                }
            }
            return output;
        }

        public void DeleteElementRecord(OrderElement e)
        {
            using (repair_serviceContext db = new())
            {
                var elementToDelete = db.OrderElements.FirstOrDefault(x => x.ElementId == e.ElementId);
                db.Remove(elementToDelete);
                db.SaveChanges();
            }
        }

        public void AddElementRecord(OrderElement oe, Order o)
        {
            using (repair_serviceContext db = new())
            {
                OrderElement newElement = new OrderElement
                {
                    ElementName = oe.ElementName,
                    ElementPrice = oe.ElementPrice,
                    IdOrder = o.OrderId,
                    ElementEndDate = oe.ElementEndDate,
                    ElementQuantity = oe.ElementQuantity,
                    CodeElement = oe.CodeElement
                };

                db.Add(newElement);
                db.SaveChanges();
            }
        }

        public void RedactElementRecord(OrderElement oe)
        {
            using (repair_serviceContext db = new())
            {
                var existingElement = db.OrderElements.FirstOrDefault(x => x.ElementId == oe.ElementId);

                if (existingElement != null)
                {
                    existingElement.ElementName = oe.ElementName;
                    existingElement.ElementPrice = oe.ElementPrice;
                    existingElement.ElementEndDate = oe.ElementEndDate;
                    existingElement.ElementQuantity = oe.ElementQuantity;
                    existingElement.CodeElement = oe.CodeElement;

                    db.SaveChanges();
                }
            }
        }

        #endregion

        #region Dictionaries

        public List<OrderStatus> GetStatusList()
        {
            List<OrderStatus> output = new();
            using (repair_serviceContext db = new())
            {
                var statuses = db.OrderStatuses;
                foreach (OrderStatus os in statuses)
                {
                    output.Add(os);
                }
            }
            return output;
        }

        public List<ElementType> GetElementTypeList()
        {
            List<ElementType> output = new();
            using (repair_serviceContext db = new())
            {
                var types = db.ElementTypes;
                foreach (ElementType et in types)
                {
                    output.Add(et);
                }
            }
            return output;
        }

        #endregion
    }
}
