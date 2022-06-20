using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebHocAnhVanNew.Models;


namespace WebHocAnhVan.Controllers
{
    public class KhoaHocController : Controller
    {
        // GET: KhoaHoc
        MyDataDataContext data = new MyDataDataContext();
        public ActionResult ListKH(string searchBy, string search)
        {
            if (searchBy == "TenKhoaHoc")
                return View(data.KhoaHocs.Where(s => s.TenKhoaHoc.StartsWith(search)).ToList());
            else if (searchBy == "GiangVien")
                return View(data.Giangviens.Where(s => s.Id.StartsWith(search)).ToList());
            else
                return View(data.KhoaHocs.ToList());
        }
        public ActionResult CreateKH()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateKH(FormCollection collection, KhoaHoc KH)
        {
            var E_IdKH = collection["idkh"];
            var E_TenKH = collection["tenkh"];
            var E_MoTa = collection["mota"];
            var E_Gia = collection["gia"];
            var E_NgayXB = collection["ngayXB"];
            var E_Hinh = collection["hinh"];
            var E_IdGV = collection["IdGV"];


            if (string.IsNullOrEmpty(E_TenKH))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                KH.IdKhoaHoc = int.Parse(E_IdKH);
                KH.TenKhoaHoc = E_TenKH.ToString();
                KH.Mota = E_MoTa.ToString();
                KH.Gia = (decimal?)double.Parse(E_Gia);
                KH.NgayXuatBan = E_NgayXB.ToString();
                KH.Hinh = E_Hinh.ToString();
                KH.IdGV = int.Parse(E_IdGV);
                data.KhoaHocs.InsertOnSubmit(KH);
                data.SubmitChanges();
                return RedirectToAction("ListKH");
            }
            return this.CreateKH();
        }
        [Authorize]
        public ActionResult Buy(int id)
        {
            MyDataDataContext context = new MyDataDataContext();
            KhoaHoc khoaHoc = context.KhoaHocs.SingleOrDefault(p => p.IdKhoaHoc == id);
            if (khoaHoc == null)
            {
                return HttpNotFound();

            }
            return View(khoaHoc);
        }
        public ActionResult EditKH(int id)
        {
            var E_Khoahoc = data.KhoaHocs.First(m => m.IdKhoaHoc == id);
            return View(E_Khoahoc);
        }
        [HttpPost]
        public ActionResult EditKH(int id, FormCollection collection)
        {
            var E_KhoaHoc = data.KhoaHocs.First(m => m.IdKhoaHoc == id);
            var E_TenKH = collection["tenkh"];
            var E_MoTa = collection["mota"];
            var E_Gia = collection["gia"];
            var E_NgayXB = collection["ngayXB"];
            var E_Hinh = collection["hinh"];
            var E_IdGV = collection["IdGV"];
            E_KhoaHoc.IdKhoaHoc = id;
            if (string.IsNullOrEmpty(E_TenKH))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {   
                E_KhoaHoc.TenKhoaHoc = E_TenKH;
                E_KhoaHoc.Mota = E_MoTa;
                E_KhoaHoc.Gia = (decimal?)double.Parse(E_Gia);
                E_KhoaHoc.NgayXuatBan = E_NgayXB;
                E_KhoaHoc.Hinh = E_Hinh;
                E_KhoaHoc.IdGV = int.Parse(E_IdGV);
                UpdateModel(E_KhoaHoc);
                data.SubmitChanges();
                return RedirectToAction("ListKH");
            }
            return this.EditKH(id);
        }
        public ActionResult Delete(int id)
        {
            var D_KhoaHoc = data.KhoaHocs.First(m => m.IdKhoaHoc == id);
            return View(D_KhoaHoc);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var D_KhoaHoc = data.KhoaHocs.Where(m => m.IdKhoaHoc == id).First();
            data.KhoaHocs.DeleteOnSubmit(D_KhoaHoc);
            data.SubmitChanges();
            return RedirectToAction("ListKH");
        }

    }
}