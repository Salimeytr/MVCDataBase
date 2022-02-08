using MVCDataBase.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dapper;
using MVCDataBase.Models.ViewModel;
using MVCDataBase.Models.DTOs;

namespace MVCDataBase.Controllers
{
    public class PersonelController : Controller
    {
        SqlConnection con = new  SqlConnection(ConfigurationManager.ConnectionStrings["Baglanti"].ConnectionString);


        // GET: Personel
        public ActionResult Liste()
        { string qry = (" select p.PersonelId, p.Ad + p.Soyad Calisan,UnvanAd, ik.UlkeAd Ikamet," +
                " uy.UlkeAd Uyruk, isNull(yp.Ad + '' + yp.Soyad, 'Baskan') Yonetici from Personel p inner join " +
                "Unvan u on(u.UnvanId = p.UnvanId) inner join Ulke ik on(ik.UlkeId = p.UlkeId)" +
                " inner join Ulke uy on(uy.UlkeId = p.UyrukId) left join Personel yp on(yp.PersonelId = p.YoneticiId)");
            PersonelModel personelModel = new PersonelModel();
            personelModel.TumPersonel = con.Query<PersonelListe>(qry).ToList();
           
            return View(personelModel);
        }
        [HttpGet]
        public ActionResult Yeni( )
        {
            PersonelModel personel = new PersonelModel();
            personel.BtnClass = "btn btn - primary";
            personel.BtnVal = "Kaydet";
            personel.Header = "Kaydet";
            personel.UlkeList=con.Query<Ulke>("select * from ulke").ToList();
            personel.UnvanList = con.Query<Unvan>("select * from unvan").ToList();
            personel.YoneticiList = con.Query<PersonelSelect>($"select YoneticiId, Ad + '' + SoyAd Adsoy from Personel where PersonelId in (select distinct YoneticiId from" +
            "personel where YoneticiId is not null)").ToList();
            personel.Personel = new Personel();
            return View("CRUD",personel);

            
        }
        [HttpPost] 
        public ActionResult Yeni(Personel personel)
        {
            con.ExecuteScalar<Personel>($"insert into personel (Ad,Soyad,Maas,UnvanId,UlkeId) values ('{personel.Ad}','{personel.Soyad}',{personel.Maas},{personel.UnvanId},'{personel.UlkeId}')");
            return RedirectToAction("Liste");
        }




        [HttpGet]
        public ActionResult Guncelle(int Id)
        {
            PersonelModel personel = new PersonelModel();
            personel.BtnClass = "btn btn - primary";
            personel.BtnVal = "Guncelle";
            personel.Header = "Guncelle";
            personel.UlkeList = con.Query<Ulke>("select * from ulke").ToList();
            personel.UnvanList = con.Query<Unvan>("select * from unvan").ToList();
            personel.YoneticiList = con.Query<PersonelSelect>($"select YoneticiId, Ad+ '' +SoyAd from Personel where PersonelId in (select distinct YoneticiId from" +
                "personel where YoneticiId is not null)").ToList();
            personel.Personel = con.Query<Personel>($"select * from Personel where PersonelId = {Id}" ).First();
            return View("CRUD", personel);


        }
        [HttpPost]
        public ActionResult Guncelle(Personel personel)
        {
            con.ExecuteScalar<Personel>($"insert into personel (Ad,Soyad,Maas,UnvanId,UlkeId) values ('{personel.Ad}','{personel.Soyad}',{personel.Maas},{personel.UnvanId},'{personel.UlkeId}')");
            return RedirectToAction("Liste");
        }







        [httpGet]
        public ActionResult Sil(int Id)
        {
            return View();
        }
        [HttpPost]
        public ActionResult Sil(int Id,Personel personel)
        {
            return View();
        }

    }
}