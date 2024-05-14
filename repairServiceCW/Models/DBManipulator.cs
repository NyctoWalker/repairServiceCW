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
        public List<Order> GetOrderList()
        {
            List<Order> output = new();
            using (repair_serviceContext db = new())
            {
                var orders = db.Orders.Include(o => o.OrderElements);
                foreach (Order o in orders)
                {
                    output.Add(o);
                }
            }
            return output;
        }

        // Add, redact, delete

        #endregion

        #region OrderElements
        public List<OrderElement> GetElementList(Order o)
        {
            List<OrderElement> output = new();

            using (repair_serviceContext db = new())
            {
                var existingOrder = db.Orders.FirstOrDefault(x => x.OrderId == o.OrderId);
                if (existingOrder == null)
                    return output;
                
                var elements = db.OrderElements.Where(x => x.IdOrder == existingOrder.OrderId);
                foreach (OrderElement e in elements)
                {
                    output.Add(e);
                }
            }
            return output;
        }

        // Add, redact, delete

        #endregion
    }
}
