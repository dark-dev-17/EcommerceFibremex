using DbManagerDark.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceApiLogic.ModelsSap
{
    [DarkTable(Name = "ORTT", IsMappedByLabels = true, IsStoreProcedure = false, IsView = false)]
    public class TipoCambio
    {
        [DarkColumn(Name = "RateDate", IsMapped = true, IsKey = true)]
        public DateTime RateDate { get; set; }

        [DarkColumn(Name = "Currency", IsMapped = true, IsKey = false)]
        public string Currency { get; set; }

        [DarkColumn(Name = "Rate", IsMapped = true, IsKey = false)]
        public float Rate { get; set; }
    }
}
