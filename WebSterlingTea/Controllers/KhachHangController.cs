using Microsoft.AspNetCore.Mvc;
using WebSterlingTea.Models;

namespace WebSterlingTea.Controllers
{
    public class KhachHangController : Controller
    {
        private readonly KhachHangActions _khActions;

        public KhachHangController()
        {
            _khActions = new KhachHangActions(); // Khởi tạo SinhVienActions
        }

        public IActionResult Index()
        {
            var dsKhachHang = _khActions.GetAll(); // Lấy tất cả sinh viên
            return View(dsKhachHang); // Trả về view với danh sách sinh viên
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(); // Trả về view tạo sinh viên
        }

        [HttpPost]
        public IActionResult Create(KhachHang kh)
        {
            if (ModelState.IsValid)
            {
                _khActions.Add(kh); // Thêm sinh viên
                return RedirectToAction("Index"); // Chuyển hướng về danh sách
            }
            return View(kh); // Trả về view nếu có lỗi
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var khachHang = _khActions.GetByID(id);
            if (khachHang == null)
            {
                return NotFound(); // Trả về 404 nếu không tìm thấy
            }
            return View(khachHang); // Trả về view chỉnh sửa sinh viên
        }

        [HttpPost]
        public IActionResult Edit(KhachHang kh)
        {
            if (ModelState.IsValid)
            {
                _khActions.Update(kh); // Cập nhật sinh viên
                return RedirectToAction("Index"); // Chuyển hướng về danh sách
            }
            return View(kh); // Trả về view nếu có lỗi
        }
        public IActionResult Detail(int id)
        {
            var khachHang = _khActions.GetByID(id);
            if (khachHang == null)
            {
                return NotFound(); // Trả về 404 nếu không tìm thấy
            }
            return View(khachHang); // Trả về view chỉnh sửa sinh viên
        }

        [HttpPost]
        public IActionResult Detail(KhachHang kh)
        {
            if (ModelState.IsValid)
            {
                _khActions.Update(kh); // Cập nhật sinh viên
                return RedirectToAction("Index"); // Chuyển hướng về danh sách
            }
            return View(kh); // Trả về view nếu có lỗi
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _khActions.DeleteByID(id); // Xóa sinh viên theo ID
            return RedirectToAction("Index"); // Chuyển hướng về danh sách
        }

        [HttpPost]
        public IActionResult DeleteAll()
        {
            _khActions.DeleteAll(); // Xóa tất cả sinh viên
            return RedirectToAction("Index"); // Chuyển hướng về danh sách
        }
    }
}

