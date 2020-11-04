using DbManagerDark.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceApiLogic.Models
{
    [DarkTable(Name = "t16_open_pay_errores_log", IsMappedByLabels = true, IsStoreProcedure = false, IsView = false)]
    public class OpenPayLog
    {
        
        [DarkColumn(Name = "t16_pk01", IsMapped = true, IsKey = true)]
        public int IdOpenPayLog { get; set; }
        [DarkColumn(Name = "t16_f001", IsMapped = true, IsKey = false)]
        public string ErroCode { get; set; }
        [DarkColumn(Name = "t16_f002", IsMapped = true, IsKey = false)]
        public string ErrorHttp { get; set; }
        [DarkColumn(Name = "t16_f003", IsMapped = true, IsKey = false)]
        public string Descripcion { get; set; }
        [DarkColumn(Name = "t16_f004", IsMapped = true, IsKey = false)]
        public string DetailsJson { get; set; }
        [DarkColumn(Name = "t16_f005", IsMapped = true, IsKey = false)]
        public int IdPedido { get; set; }
        [DarkColumn(Name = "t16_f098", IsMapped = true, IsKey = false)]
        public DateTime Created { get; set; }
        [DarkColumn(Name = "t16_f099", IsMapped = true, IsKey = false)]
        public DateTime Update { get; set; }

    }
}
