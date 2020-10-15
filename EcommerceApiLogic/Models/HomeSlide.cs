using DbManagerDark.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EcommerceApiLogic.Models
{
    [DarkTable(Name = "", IsMappedByLabels = true, IsStoreProcedure = false, IsView = true)]
    public class HomeSlide
    {
        [DarkColumn(Name = "id", IsMapped = true, IsKey = true)]
        public int IdHomeSlide { get; set; }

        [DarkColumn(Name = "PathImg1", IsMapped = true, IsKey = false)]
        public string PathImg1 { get; set; }

        [DarkColumn(Name = "PathImg2", IsMapped = true, IsKey = false)]
        public string PathImg2 { get; set; }

        [DarkColumn(Name = "UrlImg1", IsMapped = true, IsKey = false)]
        public string UrlImg1 { get; set; }

        [DarkColumn(Name = "UrlImg2", IsMapped = true, IsKey = false)]
        public string UrlImg2 { get; set; }

        [DarkColumn(Name = "Position", IsMapped = true, IsKey = false)]
        public int Position { get; set; }

        [DarkColumn(Name = "TargetBlank1", IsMapped = true, IsKey = false)]
        public string TargetBlank1 { get; set; }

        [DarkColumn(Name = "TargetBlank2", IsMapped = true, IsKey = false)]
        public string TargetBlank2 { get; set; }
    }
}
