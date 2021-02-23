using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EcommerceApiLogic.ModelsSap
{
    public class DocumentReportSap
    {
        [Display(Name = "No.Documento")]
        public string DocEntry { get; set; }
        [Display(Name = "No.Documento")]
        public string DocNum { get; set; }
        [Display(Name = "Fecha")]
        public DateTime DocDate { get; set; }
        [Display(Name = "Total")]
        public double DocTotal { get; set; }
        [Display(Name = "Tipo documento")]
        public string DocType { get; set; }
        [Display(Name = "No.Cliente")]
        public string CardCode { get; set; }
        [Display(Name = "Moneda")]
        public string DocCur { get; set; }
        [Display(Name = "Guia")]
        public string TrackNo { get; set; }
        [Display(Name = "Cliente")]
        public string Cardname { get; set; }
        [Display(Name = "Estatus")]
        public string Status { get; set; }
        [Display(Name = "Comentarios")]
        public string Remarks { get; set; }
        [Display(Name = "NO.E-commerce")]
        public string DocNumEcommerce { get; set; }
    }
}
