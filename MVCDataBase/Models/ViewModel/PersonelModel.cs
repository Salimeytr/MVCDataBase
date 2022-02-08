using MVCDataBase.Models.DTOs;
using MVCDataBase.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCDataBase.Models.ViewModel
{
    public class PersonelModel
    {
        public string Header { get; set; }
        public string BtnVal { get; set; }
        public string BtnClass { get; set; }
        public Personel Personel { get; set; }

        public List <Unvan> UnvanList { get; set; }
        public List <Ulke> UlkeList { get; set; }
        public List<PersonelSelect> YoneticiList { get; set; }

        public List<PersonelListe > TumPersonel  { get; set; }


    }
}