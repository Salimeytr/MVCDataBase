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

namespace MVCDataBase.Controllers
{
    public class UnvanController : Controller
    {
        // GET: Unvan
        SqlConnection con=new SqlConnection(ConfigurationManager.ConnectionStrings["Baglanti"].ConnectionString);//SQL Bağlantı Kurmak için
        public ActionResult Liste()
        {
            string qry = "select * from Unvan";
            var ulist = con.Query<Unvan>(qry); // ulist içinde unvanların listesi var
            return View(ulist);
        }
        [HttpGet]//Kayıt alma
        public ActionResult Guncelle(int Id)
        {
            string qry = $"select * from unvan  where UnvanId = {Id}";
            var unvan = con.Query<Unvan>(qry).First();
            return View(unvan);

        }
        //Güncelleme işlemi için Post işlemi

        [HttpPost]//Kayıt alma
        public ActionResult Guncelle(Unvan model)
        {
            string qry = $"update unvan set unvanAd= @UnvanAd where UnvanId = {model.UnvanId}";
            con.ExecuteScalar<int>(qry,model);
            return RedirectToAction("Liste"); 


        }
    }
}