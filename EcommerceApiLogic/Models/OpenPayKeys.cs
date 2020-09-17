using DbManagerDark.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceApiLogic.Models
{
    [DarkTable(Name = "t04_open_pay_keys", IsMappedByLabels = true, IsStoreProcedure = false, IsView = false)]
    public class OpenPayKeys
    {
        [DarkColumn(Name = "t04_pk01", IsMapped = true, IsKey = true)]
        public int Id { get; set; }

        [DarkColumn(Name = "t04_f001", IsMapped = true, IsKey = false)]
        public string IdKey { get; set; }

        [DarkColumn(Name = "t04_f002", IsMapped = true, IsKey = false)]
        public string PrivateKey { get; set; }

        [DarkColumn(Name = "t04_f003", IsMapped = true, IsKey = false)]
        public string PublicKey { get; set; }

        [DarkColumn(Name = "t04_f004", IsMapped = true, IsKey = false)]
        public string SandBoxMode { get; set; }

        [DarkColumn(Name = "t04_f005", IsMapped = true, IsKey = false)]
        public string ProductionMode { get; set; }

        [DarkColumn(Name = "t04_f006", IsMapped = true, IsKey = false)]
        public string DashBoardpath { get; set; }

        [DarkColumn(Name = "t04_f098", IsMapped = true, IsKey = false)]
        public DateTime Created { get; set; }

        [DarkColumn(Name = "t04_f099", IsMapped = true, IsKey = false)]
        public DateTime Updated { get; set; }

    }
}
