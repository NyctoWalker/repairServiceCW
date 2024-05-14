using System;
using System.Collections.Generic;

#nullable disable

namespace repairServiceCW.Models
{
    public partial class OrderElement
    {
        public long ElementId { get; set; }
        public short CodeElement { get; set; }
        public long IdOrder { get; set; }
        public string ElementName { get; set; }
        public DateTime ElementEndDate { get; set; }
        public int ElementPrice { get; set; }
        public short? ElementQuantity { get; set; }

        public virtual ElementType CodeElementNavigation { get; set; }
        public virtual Order IdOrderNavigation { get; set; }
    }
}
