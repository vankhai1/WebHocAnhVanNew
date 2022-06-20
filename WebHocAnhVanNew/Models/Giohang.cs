using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebHocAnhVanNew.Models;

namespace WebHocAnhVanNew.Models
{
    public class Giohang
    {
        MyDataDataContext data = new MyDataDataContext();
        public int id { get; set; }
        [Display(Name = "Ten")]
        public string ten { get; set; }
        [Display(Name = "Anh Bia")]
        public string hinh { get; set; }
        [Display(Name = "Gia ban")]
        public Double gia { get; set; }
        [Display(Name = "So luong")]
        public int iSoluong { get; set; }
        [Display(Name = "Thanh tien")]
        public Double dThanhtien
        {
            get { return iSoluong * gia; }
        }
        public Giohang(int id)
        {
            this.id = id;
            KhoaHoc khoaHoc = data.KhoaHocs.Single(n => n.IdKhoaHoc == id);
            ten = khoaHoc.TenKhoaHoc;
            hinh = khoaHoc.Hinh;
            gia = double.Parse(khoaHoc.Gia.ToString());
            iSoluong = 1;

        }
    }
}