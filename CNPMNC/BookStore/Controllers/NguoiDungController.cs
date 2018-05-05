using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    public class NguoiDungController : Controller
    {
        // GET: NguoiDung
        public ActionResult Index()
        {
            return View();
        }

        dbQLBansachDataContext db = new dbQLBansachDataContext();

        [HttpGet]
        public ActionResult Dangky()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Dangky(FormCollection collection, KHACHHANG kh)
        {
            var hoten = collection["HotenKH"];
            var tendn = collection["TenDN"];
            var matkhau = collection["Matkhau"];
            var nhaplaimatkhau = collection["nhaplaimatkhau"];
            var diachi = collection["Diachi"];
            var email = collection["Email"];
            var dienthoai = collection["Dienthoai"];
            var ngaysinh = String.Format("{0:MM/dd/yyyy}", collection["Ngaysinh"]);
            if (String.IsNullOrEmpty(hoten))
                ViewData["Loi1"] = "Ho ten khach hang khong duoc de trong";
            else if (String.IsNullOrEmpty(tendn))
                ViewData["Loi2"] = "Phai nhap ten dang nhap";
            else if (String.IsNullOrEmpty(matkhau))
                ViewData["Loi3"] = "Phai nhap mat khau";
            else if (String.IsNullOrEmpty(nhaplaimatkhau))
                ViewData["Loi4"] = "Mat khau nhap lai khong bi de trong";
            else if (string.IsNullOrEmpty(email))
                ViewData["Loi5"] = "Email khong duoc bo trong";
            else if (string.IsNullOrEmpty(dienthoai))
                ViewData["Loi6"] = "Khong duoc bo trong sdt";
            else
            {
                kh.HoTenKH = hoten;
                kh.TenDN = tendn;
                kh.Matkhau = matkhau;
                kh.Email = email;
                kh.DiachiKH = diachi;
                kh.DienthoaiKH = dienthoai;
                kh.Ngaysinh = DateTime.Parse(ngaysinh);
                db.KHACHHANGs.InsertOnSubmit(kh);
                db.SubmitChanges();
                return RedirectToAction("Dangnhap");
            }
            return this.Dangky();
        }

        [HttpGet]
        public ActionResult Dangnhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Dangnhap(FormCollection collection)
        {
            var tendn = collection["TenDN"];
            var matkhau = collection["Matkhau"];
            if (String.IsNullOrEmpty(tendn))
                ViewData["Loi1"] = "Phai nhap ten dang nhap";
            else if (String.IsNullOrEmpty(matkhau))
                ViewData["Loi2"] = "Phai nhap mat khau";
            else
            {
                KHACHHANG kh = db.KHACHHANGs.SingleOrDefault(n => n.TenDN == tendn && n.Matkhau == matkhau);
                if (kh != null)
                {
                    ViewBag.ThongBao = "Dang nhap thanh cong";
                    Session["Taikhoan"] = kh;
                }
                else
                    ViewBag.ThongBao = "Ten dang nhap hoac mat khau khong dung";
            }
            return View();
        }
    }
}