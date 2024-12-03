using System;
using System.ComponentModel.DataAnnotations;

namespace WebSterlingTea.Models
{
    public class KhachHang
    {
        // tt	cccd	hodem	ten	nickname	email	dienthoai	diem_tichluy	diem_renluyen	xet_hocbong
        int tt;
        string cccd;
        string khack_hang;
        string dien_thoai;
        string email;
        int so_ban;
       
        double coc;

        [Key]
        public int Tt { get => tt; set => tt = value; }
        [Required]
        public string Cccd { get => cccd; set => cccd = value; }
        [Required]
        public string Khach_Hang { get => khack_hang; set => khack_hang = value; }
        [Required]
        public string Dien_Thoai { get => dien_thoai; set => dien_thoai = value; }
        public string Email { get => email; set => email = value; }
        public int So_Ban { get => so_ban; set => so_ban = value; }
        [Range(1, 10)]
        public double Coc { get => coc; set => coc = value; }
    }
}

