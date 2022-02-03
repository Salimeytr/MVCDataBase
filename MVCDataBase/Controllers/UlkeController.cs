using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dapper;
using MVCDataBase.Models;
using MVCDataBase.Models.Siniflar;
using MVCDataBase.Models.ViewModel;

namespace MVCDataBase.Controllers
{
    public class UlkeController : Controller
    {
        // GET: Unvan
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Baglanti"].ConnectionString);//SQL Bağlantı Kurmak için
        public ActionResult Liste()
        {
            string qry = "select * from Ulke";
            var ulist = con.Query<Ulke>(qry); // ulist içinde unvanların listesi var
            return View(ulist);
        }
        [HttpGet]//Kayıt alma
        public ActionResult Guncelle(string Id)
        {
            UlkeModel model = new UlkeModel();

            string qry = $"select * from ulke  where UlkeId = '{Id}'";
            model.Ulke = con.Query<Ulke>(qry).First();
            model.BtnClass = "btn btn - success";
            model.Header = "Güncelleme İşlemi";
            model.BtnVal = "Güncelle";
            return View("CRUD", model);



        }
        //Güncelleme işlemi için Post işlemi

        [HttpPost]//Kayıt alma
        public ActionResult Guncelle(Ulke model)
        {
            string qry = $"update ulke set ulkeAd= @UlkeAd where UlkeId = '{model.UlkeId}'";
            con.ExecuteScalar<string>(qry, model);
            return RedirectToAction("Liste");


        }




        [HttpGet]// Silme işlemi için Kayıt alma
        public ActionResult Sil(string Id)
        {
            UlkeModel model = new UlkeModel();
            string qry = $"select * from ulke where UlkeId='{Id}'";
            model.Ulke = con.Query<Ulke>(qry).First();
            model.BtnClass = "btn btn-danger";
            model.Header = "Silme İşlemi";
            model.BtnVal = "Sil";
            return View("CRUD", model);
        }
        //Silme işlemi için Post işlemi

        [HttpPost]//Kayıt Silme Delete komutu ekleme kısmı
        public ActionResult Sil(Ulke model)
        {
            string qry = $"delete from Ulke where UlkeId ='{model.UlkeId}'";
            string ulke = con.ExecuteScalar<string>(qry, model);
            return RedirectToAction("Liste");


        }



        [HttpGet]//Yeni kayıt ekleme
        public ActionResult Yeni()
        {
            UlkeModel model = new UlkeModel();

            model.Ulke = new Ulke();
            model.BtnClass = "btn btn-danger";
            model.Header = "Yeni Kayıt İşlemi";
            model.BtnVal = "Yeni Kayıt";
            return View("CRUD", model);
        }


        [HttpPost]//Kayıt Silme Delete komutu ekleme kısmı
        public ActionResult Yeni(Ulke model)
        {
            string qry = $"insert into ulke (UlkeAd,UlkeId) values (@UlkeAd,@UlkeId)  ";
            con.ExecuteScalar<string>(qry, model);
            return RedirectToAction("Liste");


        }


    }
}