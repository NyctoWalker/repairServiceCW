using System;
using System.Collections.Generic;

#nullable disable

namespace repairServiceCW.Models
{
    public partial class ElementType
    {
        public ElementType()
        {
            OrderElements = new HashSet<OrderElement>();
        }

        public short ElementCode { get; set; }
        public string ElementType1 { get; set; }

        public virtual ICollection<OrderElement> OrderElements { get; set; }
    }
}
