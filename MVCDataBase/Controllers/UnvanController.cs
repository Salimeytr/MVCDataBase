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
    public class UnvanController : Controller
    {
        // GET: Unvan
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Baglanti"].ConnectionString);//SQL Bağlantı Kurmak için
        public ActionResult Liste()
        {
            string qry = "select * from Unvan";
            var ulist = con.Query<Unvan>(qry); // ulist içinde unvanların listesi var
            return View(ulist);
        }
        [HttpGet]//Kayıt alma
        public ActionResult Guncelle(int Id)
        {
            UnvanModel model = new UnvanModel();

            string qry = $"select * from unvan  where UnvanId = {Id}";
            model.Unvan  = con.Query<Unvan>(qry).First();
            model.BtnClass = "btn btn - success";
            model.Header = "Güncelleme İşlemi";
            model.BtnVal = "Güncelle";
            return View("CRUD", model);

            

        }
        //Güncelleme işlemi için Post işlemi

        [HttpPost]//Kayıt alma
        public ActionResult Guncelle(Unvan model)
        {
            string qry = $"update unvan set unvanAd= @UnvanAd where UnvanId = {model.UnvanId}";
            con.ExecuteScalar<int>(qry, model);
            return RedirectToAction("Liste");


        }




        [HttpGet]// Silme işlemi için Kayıt alma
        public ActionResult Sil(int Id)
        {
            UnvanModel model= new UnvanModel();
            string qry = $"select * from unvan where UnvanId={Id}";
            model.Unvan=con.Query<Unvan>(qry).First();
            model.BtnClass = "btn btn-danger";
            model.Header = "Silme İşlemi";
            model.BtnVal = "Sil";
            return View ("CRUD",model);
        }
        //Silme işlemi için Post işlemi

        [HttpPost]//Kayıt Silme Delete komutu ekleme kısmı
        public ActionResult Sil(Unvan model)
        {
            string qry = $"delete from Unvan where UnvanId ={model.UnvanId}";
            var unvan= con.ExecuteScalar<int>(qry, model);
            return RedirectToAction("Liste");


        }



        [HttpGet]//Yeni kayıt ekleme
        public ActionResult Yeni()
        {
            UnvanModel model = new UnvanModel();
            
            model.Unvan = new Unvan();
            model.BtnClass = "btn btn-danger";
            model.Header = "Yeni Kayıt İşlemi";
            model.BtnVal = "Yeni Kayıt";
            return View("CRUD", model);
        }


        [HttpPost]//Kayıt Silme Delete komutu ekleme kısmı
        public ActionResult Yeni(Unvan model)
        {
            string qry = $"insert into unvan (UnvanAd) values (@UnvanAd) " ;
            con.ExecuteScalar<int>(qry, model);
            return RedirectToAction("Liste");


        }


    }
}