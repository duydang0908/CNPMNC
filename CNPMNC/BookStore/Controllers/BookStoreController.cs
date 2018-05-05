using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;
using PagedList;
using PagedList.Mvc;

namespace BookStore.Controllers
{
    public class BookStoreController : Controller
    {
        dbQLBansachDataContext data = new dbQLBansachDataContext();

        public ActionResult SPTheochude(int id)
        {
            var sach = from s in data.SACHes where s.MaCD == id select s;
            return View(sach);
        }

        public ActionResult SpTheoNXB(int id)
        {
            var sach = from s in data.SACHes where s.MaCD == id select s;
            return View(sach);
        }

        public ActionResult Details(int id)
        {
            var sach = from s in data.SACHes
                       where s.Masach == id
                       select s;
            return View(sach.Single());
        }

        private List<SACH> Laysachmoi(int count)
        {
            return data.SACHes.OrderByDescending(a => a.Ngaycapnhat).Take(count).ToList();
        }

        // GET: BookStore
        //public ActionResult Index(int? page)
        //{
        //    int pageSize = 5;
        //    int pageNum = (page ?? 1);
        //    var sachmoi = Laysachmoi(15);
        //    return View(sachmoi.ToPagedList(pageNum, pageSize));


        //    //var sachmoi = Laysachmoi(5);
        //    //return View(sachmoi);
        //}

        public ActionResult Index(int? page, string searchString)
        {
            var sp = from e in data.SACHes select e;
            if (!String.IsNullOrEmpty(searchString))
            {
                sp = sp.Where(s => s.Tensach.Contains(searchString));
            }
            ViewBag.SeachString = searchString;
            //tạo bien so trang
            int pageNumber = (page ?? 1);
            //tao bien quy dinh so san pham tren moi trang
            int pageSize = 6;
            return View(sp.ToList().OrderBy(n => n.Masach).ToPagedList(pageNumber, pageSize));
            //return View(db.SAN_PHAMs.ToList());
        }


        public ActionResult Chude()
        {
            var chude = from cd in data.CHUDEs select cd;
            return PartialView(chude);
        }

        public ActionResult Nhaxuatban()
        {
            var nxb = from cd in data.NHAXUATBANs select cd;
            return PartialView(nxb);
        }

    }
}