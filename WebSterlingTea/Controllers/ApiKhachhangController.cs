using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebSterlingTea.Models;

namespace WebSterlingTea.Controllers
{
    [Route("api/KhachHang")]
    [ApiController]
    public class ApiKhachhangController : ControllerBase
    {
        private readonly KhachHangActions _khActions;
        public ApiKhachhangController()
        {
            _khActions = new KhachHangActions(); // Khởi tạo SinhVienActions
        }

        // GET: api/<StudentController>
        [HttpGet]
        public string Get()
        {
            var dsKhachHang = _khActions.GetAll(); // Lấy tất cả sinh viên
            var opt = new JsonSerializerOptions() { WriteIndented = true };
            string strJson = JsonSerializer.Serialize<IList<KhachHang>>(dsKhachHang, opt);
            return strJson;
        }

        [HttpGet("sort")]
        public string GetSorted(string sortBy, string order = "asc")
        {
            var dsKhachHang = _khActions.GetAll(); // Lấy tất cả sinh viên

            if (!string.IsNullOrEmpty(sortBy))
            {
                // Lấy kiểu dữ liệu của thuộc tính được chỉ định để sắp xếp
                var propertyType = typeof(KhachHang).GetProperty(sortBy).PropertyType;
                // Kiểm tra xem thuộc tính có phải là chuỗi không
                var isString = propertyType == typeof(string);
                // Kiểm tra xem thứ tự sắp xếp có phải là giảm dần không
                var isDesc = order.ToLower() == "des";

                // Thực hiện sắp xếp dựa trên các điều kiện trên
                dsKhachHang = isDesc
                    ? isString
                        // Nếu giảm dần và thuộc tính là chuỗi, sắp xếp giảm dần theo chuỗi
                        ? dsKhachHang.OrderByDescending(kh => kh.GetType().GetProperty(sortBy).GetValue(kh).ToString()).ToList()
                        // Nếu giảm dần và thuộc tính không phải là chuỗi, sắp xếp giảm dần theo số
                        : dsKhachHang.OrderByDescending(kh => Convert.ToDouble(kh.GetType().GetProperty(sortBy).GetValue(kh))).ToList()
                    : isString
                        // Nếu tăng dần và thuộc tính là chuỗi, sắp xếp tăng dần theo chuỗi
                        ? dsKhachHang.OrderBy(kh => kh.GetType().GetProperty(sortBy).GetValue(kh).ToString()).ToList()
                        // Nếu tăng dần và thuộc tính không phải là chuỗi, sắp xếp tăng dần theo số
                        : dsKhachHang.OrderBy(kh => Convert.ToDouble(kh.GetType().GetProperty(sortBy).GetValue(kh))).ToList();
            }

            var opt = new JsonSerializerOptions() { WriteIndented = true };
            string strJson = JsonSerializer.Serialize<IList<KhachHang>>(dsKhachHang, opt);
            return strJson;
        }

        // GET api/<StudentController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            var kh = _khActions.GetByID(id); // Lấy tất cả sinh viên
            var opt = new JsonSerializerOptions() { WriteIndented = true };
            string strJson = JsonSerializer.Serialize<KhachHang>(kh, opt);
            return strJson;
        }

        // POST api/<StudentController>
        //[HttpPost]
        //public IActionResult Post([FromBody] SinhVien sv)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _svActions.Add(sv);
        //    return CreatedAtAction(nameof(Get), new { id = sv.Tt }, sv);
        //}


        // POST (CÁCH 2)
        [HttpPost]
        public string Post([FromBody] KhachHang kh)
        {
            if (!ModelState.IsValid)
            {
                return JsonSerializer.Serialize(new { error = "Invalid model state" });
            }

            _khActions.Add(kh);
            var opt = new JsonSerializerOptions() { WriteIndented = true };
            return JsonSerializer.Serialize(kh, opt);
        }

        // PUT api/<StudentController>/5
        //[HttpPut("{id}")]
        //public IActionResult Put(int id, [FromBody] SinhVien sv)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var existing_sv = _svActions.GetByID(id);
        //    if (existing_sv == null)
        //    {
        //        return NotFound(new { error = "SinhVien not found" });
        //    }

        //    // Update the existing SinhVien with new values
        //    existing_sv.Tt = sv.Tt;
        //    existing_sv.Hodem = sv.Hodem;
        //    existing_sv.Ten = sv.Ten;
        //    existing_sv.Cccd = sv.Cccd;
        //    existing_sv.Nickname = sv.Nickname;
        //    existing_sv.Email = sv.Email;
        //    existing_sv.Dienthoai = sv.Dienthoai;
        //    existing_sv.Diem_tichluy = sv.Diem_tichluy;
        //    existing_sv.Diem_renluyen = sv.Diem_renluyen;
        //    // Add other properties as needed

        //    _svActions.Update(existing_sv);

        //    return NoContent(); // 204 No Content
        //}


        // PUT (Cách 2)
        [HttpPut("{id}")]
        public string Put(int id, [FromBody] KhachHang kh)
        {
            if (!ModelState.IsValid)
            {
                return JsonSerializer.Serialize(new { error = "Invalid model state" });
            }

            var existing_kh = _khActions.GetByID(id);
            if (existing_kh == null)
            {
                return JsonSerializer.Serialize(new { error = "KhachHang not found" });
            }

            // Update the existing SinhVien with new values
            existing_kh.Tt = kh.Tt;
            existing_kh.Cccd = kh.Cccd;
            existing_kh.Khach_Hang = kh.Khach_Hang;
            existing_kh.Dien_Thoai = kh.Dien_Thoai;
            existing_kh.Email = kh.Email;
            existing_kh.So_Ban = kh.So_Ban;
            existing_kh.Coc = kh.Coc;
            // Add other properties as needed

            _khActions.Update(existing_kh);

            return JsonSerializer.Serialize(new { message = "Update successful" });
        }


        // DELETE api/<StudentController>/5
        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    var existing_sv = _svActions.GetByID(id);
        //    if (existing_sv == null)
        //    {
        //        return NotFound(new { error = "SinhVien not found" });
        //    }

        //    _svActions.DeleteByID(id);

        //    return NoContent(); // 204 No Content
        //}

        // DELETE (Cách 2)
        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            var existing_kh = _khActions.GetByID(id);
            if (existing_kh == null)
            {
                return JsonSerializer.Serialize(new { error = "KhachHang not found" });
            }

            _khActions.DeleteByID(id);

            return JsonSerializer.Serialize(new { message = "Delete successful" });
        }

        [HttpDelete]
        public string DeleteAll()
        {
            var allKhachHangs = _khActions.GetAll();
            if (allKhachHangs == null || !allKhachHangs.Any())
            {
                return JsonSerializer.Serialize(new { message = "No KhachHang records to delete" });
            }

            foreach (var sv in allKhachHangs)
            {
                _khActions.DeleteAll();
            }

            return JsonSerializer.Serialize(new { message = "All KhachHang records deleted successfully" });
        }
    }

    
}
