using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebHocAnhVanNew.Models;
namespace WebHocAVNew.Controllers
{
    [Authorize]
    public class GiangVienController : Controller
    {
        MyDataDataContext data = new MyDataDataContext();
        // GET: GiangVien
        public ActionResult ListGV()
        {
            var all_giangvien = from GV in data.Giangviens select GV;
            return View(all_giangvien);
        }
        public ActionResult CreateGV()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateGV(FormCollection collection, Giangvien s)
        {
            var E_IDGV = collection["idgv"];
            var E_HotenGV = collection["Hotengv"];
            var E_SDT = collection["sdt"];
            var E_DiaChi = collection["diachi"];
            var E_NgaysinhGV = collection["ngaysinhgv"];
            var E_id = collection["id"];
            if (string.IsNullOrEmpty(E_HotenGV))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {

                s.HoTenGV = E_HotenGV.ToString();
                s.SDT = E_SDT.ToString();
                s.DCGV = E_DiaChi;
                s.NgaySinhGV = E_NgaysinhGV;
                s.Id =E_id;
                data.Giangviens.InsertOnSubmit(s);
                data.SubmitChanges();
                return RedirectToAction("ListGV");
            }
            return this.CreateGV();
        }
        ///edit/
        public ActionResult EditGV(int id)
        {
            var E_GiangVien = data.KhoaHocs.First(m => m.IdGV == id);
            return View(E_GiangVien);
        }
        [HttpPost]
        public ActionResult EditGV(int id, FormCollection collection)
        {
            var E_GiangVien = data.Giangviens.First(m => m.IdGV == id);
            var E_HotenGV = collection["Hotengv"];
            var E_SDT = collection["sdt"];
            var E_DiaChi = collection["diachi"];
            var E_NgaysinhGV = collection["ngaysinhgv"];
            var E_id = collection["id"];
                E_GiangVien.IdGV = id;
            if (string.IsNullOrEmpty(E_HotenGV))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                E_GiangVien.HoTenGV = E_HotenGV;
                E_GiangVien.SDT = E_SDT;
                E_GiangVien.DCGV = E_DiaChi;
                E_GiangVien.NgaySinhGV = E_NgaysinhGV;
                E_GiangVien.Id = E_id;
                UpdateModel(E_GiangVien);
                data.SubmitChanges();
                return RedirectToAction("ListGV");
            }
            return this.EditGV(id);
        }
        public ActionResult DeleteGV(int IdGV)
        {
            var E_Khoahoc = data.Giangviens.First(n => n.IdGV == IdGV);
            return View(E_Khoahoc);
        }







    }
}