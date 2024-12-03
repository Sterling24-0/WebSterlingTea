using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.ComponentModel;
using System.Data;
using System.IO;
using OfficeOpenXml;


namespace WebSterlingTea.Models
{
    public class KhachHangActions
    {
        // Khai báo đường dẫn file excel. Duong dan nay la o file ds_khachhang tren tmuc Datas. click phai->copy path
        private string filePath = @"C:\Users\PC\source\repos\WebSterlingTea\WebSterlingTea\Data\ds_kh.xlsx";
        private FileInfo GetFileExcel()
        {
            return new FileInfo(filePath);
        }

        // Lấy tất cả các khách hàng (GetAll)
        public List<KhachHang> GetAll()
        {
            var ds_kh = new List<KhachHang>();

            // Thiết lập LicenseContext
            //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
           OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            var file_excel = GetFileExcel();

            using (var package = new ExcelPackage(file_excel))
            {
                var worksheet = package.Workbook.Worksheets[0]; // Lấy về sheet đầu tiên
                var rowCount = worksheet.Dimension.Rows; // Đếm số dòng trong excel (có dữ liệu
                var colCount = worksheet.Dimension.Columns;

                for (int row = 2; row <= rowCount; row++) // Mỗi một dong trong danh là một khách hàng
                {
                    // Ex Row Data: 0 |   1   |    2       | 3  | ....
                    // Ex Row Data: 1 | 11234 | Nguyễn Văn | An | ....
                    KhachHang kh = new KhachHang();
                    kh.Tt = Int32.Parse(worksheet.Cells[row, 1].Text);
                    kh.Cccd = worksheet.Cells[row, 2].Text;
                    kh.Khach_Hang = worksheet.Cells[row, 3].Text;
                    kh.Dien_Thoai = worksheet.Cells[row, 4].Text;
                    kh.Email = worksheet.Cells[row, 5].Text;
                    kh.So_Ban = int.Parse(worksheet.Cells[row, 6].Text);
                    kh.Coc = Double.Parse(worksheet.Cells[row,7].Text);

                    ds_kh.Add(kh);
                }
            }

            return ds_kh;
        }

        // Lấy thông chi tiết của một khách hàng (GetByID)
        public KhachHang GetByID(int id)
        {
            var file_excel = GetFileExcel();
            KhachHang kh = null;

            using (var package = new ExcelPackage(file_excel))
            {
                var worksheet = package.Workbook.Worksheets[0];
                var rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++)
                {
                    if (Int32.Parse(worksheet.Cells[row, 1].Text) == id) // Kiểm tra ID
                    {
                        kh = new KhachHang
                        {
                            Tt = Int32.Parse(worksheet.Cells[row, 1].Text),
                            Cccd = worksheet.Cells[row, 2].Text,
                            Khach_Hang = worksheet.Cells[row, 3].Text,
                            Dien_Thoai = worksheet.Cells[row, 4].Text,
                            Email = worksheet.Cells[row, 5].Text,
                            So_Ban = Int32.Parse(worksheet.Cells[row, 6].Text),
                         
                            Coc = Double.Parse(worksheet.Cells[row, 7].Text)
                        };
                        break; // Dừng vòng lặp khi tìm thấy khách hàng
                    }
                }
            }

            return kh; // Trả về khách hàng hoặc null nếu không tìm thấy
        }

        // Thêm (Add)
        public void Add(KhachHang kh)
        {
            var file_excel = GetFileExcel(); // Sử dụng phương thức để lấy FileInfo

            using (var package = new ExcelPackage(file_excel))
            {
                var worksheet = package.Workbook.Worksheets[0];
                var rowCount = worksheet.Dimension.Rows;

                // Thêm khách hàng mới vào hàng tiếp theo
                worksheet.Cells[rowCount + 1, 1].Value = kh.Tt;
                worksheet.Cells[rowCount + 1, 2].Value = kh.Cccd;
                worksheet.Cells[rowCount + 1, 3].Value = kh.Khach_Hang;
                worksheet.Cells[rowCount + 1, 4].Value = kh.Dien_Thoai;
                worksheet.Cells[rowCount + 1, 5].Value = kh.Email;
                worksheet.Cells[rowCount + 1, 6].Value = kh.So_Ban;
               
                worksheet.Cells[rowCount + 1, 7].Value = kh.Coc;

                package.Save(); // Lưu thay đổi vào tệp
            }
        }

        // Cập nhật (UpdateByID)
        public void Update(KhachHang kh)
        {
            var file_excel = GetFileExcel(); // Sử dụng phương thức để lấy FileInfo

            using (var package = new ExcelPackage(file_excel))
            {
                var worksheet = package.Workbook.Worksheets[0];
                var rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++)
                {
                    if (Int32.Parse(worksheet.Cells[row, 1].Text) == kh.Tt) // Kiểm tra ID
                    {
                        // Cập nhật thông tin khách hàng
                        worksheet.Cells[row, 2].Value = kh.Cccd;
                        worksheet.Cells[row, 3].Value = kh.Khach_Hang;
                        worksheet.Cells[row, 4].Value = kh.Dien_Thoai;
                        worksheet.Cells[row, 5].Value = kh.Email;
                        worksheet.Cells[row, 6].Value = kh.So_Ban;
                      
                        worksheet.Cells[row, 7].Value = kh.Coc;

                        break; // Dừng vòng lặp khi đã cập nhật
                    }
                }

                package.Save(); // Lưu thay đổi vào tệp
            }
        }

        // Xóa tất cả (DeleteAll)
        public void DeleteAll()
        {
            var file_excel = GetFileExcel(); // Sử dụng phương thức để lấy FileInfo

            using (var package = new ExcelPackage(file_excel))
            {
                var worksheet = package.Workbook.Worksheets[0];
                var rowCount = worksheet.Dimension.Rows;

                // Xóa tất cả hàng từ hàng 2 trở đi
                for (int row = 2; row <= rowCount; row++)
                {
                    worksheet.DeleteRow(2); // Luôn xóa hàng thứ 2 cho đến khi không còn hàng
                }

                package.Save(); // Lưu thay đổi vào tệp
            }
        }

        // Xóa một khách hàng (DeleteByID)
        public void DeleteByID(int id)
        {
            var file_excel = GetFileExcel(); // Sử dụng phương thức để lấy FileInfo

            using (var package = new ExcelPackage(file_excel))
            {
                var worksheet = package.Workbook.Worksheets[0];
                var rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++)
                {
                    if (Int32.Parse(worksheet.Cells[row, 1].Text) == id) // Kiểm tra ID
                    {
                        worksheet.DeleteRow(row); // Xóa hàng khách hàng
                        break; // Dừng vòng lặp khi đã xóa
                    }
                }

                package.Save(); // Lưu thay đổi vào tệp
            }
        }

    }
}
