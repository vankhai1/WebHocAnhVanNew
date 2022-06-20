using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebHocAnhVanNew.Models;


namespace WebHocAVNew.Controllers
{
    [Authorize]
    public class GioHangController : Controller
    {
        // GET: GioHang
        MyDataDataContext data = new MyDataDataContext();
        public List<Giohang> LayGioHang()
        {
            List<Giohang> listGioHang = Session["GioHang"] as List<Giohang>;
            if (listGioHang == null)
            {
                listGioHang = new List<Giohang>();
                Session["Giohang"] = listGioHang;
            }
            return listGioHang;
        }

        public ActionResult ThemGioHang(int id, string strURL)
        {
            List<Giohang> listGioHang = LayGioHang();
            Giohang sanpham = listGioHang.Find(n => n.id == id);
            if (sanpham == null)
            {
                sanpham = new Giohang(id);
                listGioHang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.iSoluong++;
                return Redirect(strURL);
            }
        }

        private int TongSoLuong()
        {
            int tsl = 0;
            List<Giohang> listGioHang = Session["GioHang"] as List<Giohang>;
            if (listGioHang != null)
            {
                tsl = listGioHang.Sum(n => n.iSoluong);
            }
            return tsl;
        }

        private int TongSoLuongSanPham()
        {
            int tsl = 0;
            List<Giohang> listGioHang = Session["GioHang"] as List<Giohang>;
            if (listGioHang != null)
            {
                tsl = listGioHang.Count;
            }
            return tsl;
        }

        private double TongTien()
        {
            double tt = 0;
            List<Giohang> listGioHang = Session["GioHang"] as List<Giohang>;
            if (listGioHang != null)
            {
                tt = listGioHang.Sum(n => n.dThanhtien);
            }
            return tt;
        }

        public ActionResult Giohang()
        {
            List<Giohang> listGioHang = LayGioHang();
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            ViewBag.TongSoLuongSanPham = TongSoLuongSanPham();

            return View(listGioHang);
        }

        public ActionResult GioHangPartial()
        {
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            ViewBag.TongSoLuongSanPham = TongSoLuongSanPham();

            return View();
        }

        public ActionResult XoaGioHang(int id)
        {
            List<Giohang> listGioHang = LayGioHang();
            Giohang sanpham = listGioHang.SingleOrDefault(n => n.id == id);
            if (sanpham != null)
            {
                listGioHang.RemoveAll(n => n.id == id);
                return RedirectToAction("Giohang");
            }
            return RedirectToAction("Giohang");
        }

        public ActionResult CapNhatGioHang(int id, FormCollection collection)
        {
            List<Giohang> listGioHang = LayGioHang();
            Giohang sanpham = listGioHang.SingleOrDefault(n => n.id == id);
            if (sanpham != null)
            {
                sanpham.iSoluong = int.Parse(collection["txtSoLg"].ToString());
            }
            return RedirectToAction("Giohang");
        }

        public ActionResult XoaTatCaGioHang()
        {
            List<Giohang> listGioHang = LayGioHang();
            listGioHang.Clear();
            return RedirectToAction("Giohang");
        }

        // GET: GioHang
        public ActionResult Detail(int id)
        {
            var all_rubik = data.KhoaHocs.Where(m => m.IdKhoaHoc == id).First();
            return View(all_rubik);
        }
        public ActionResult Buy(int id)
        {
            var all_rubik = data.ChiTietDonHangs.Where(m => m.IdKhoaHoc == id).First();
            return View(all_rubik);

        }
    }
}