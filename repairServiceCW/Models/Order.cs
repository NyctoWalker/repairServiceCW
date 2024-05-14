using System;
using System.Collections.Generic;

#nullable disable

namespace repairServiceCW.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderElements = new HashSet<OrderElement>();
        }

        public long OrderId { get; set; }
        public short CodeStatus { get; set; }
        public string OrderCode { get; set; }
        public string OrderDescription { get; set; }
        public DateTime OrderTakeDate { get; set; }
        public string OrderDevice { get; set; }
        public string OrderDeviceModel { get; set; }

        public virtual OrderStatus CodeStatusNavigation { get; set; }
        public virtual ICollection<OrderElement> OrderElements { get; set; }
    }
}
