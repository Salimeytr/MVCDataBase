using MVCDataBase.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCDataBase.Models.ViewModel
{
    public class UlkeModel
    {
        public string Header { get; set; }
        public string BtnVal { get; set; }
        public string BtnClass { get; set; }
        public Ulke Ulke { get; set; }

    }
}