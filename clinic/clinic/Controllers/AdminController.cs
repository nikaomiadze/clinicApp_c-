using clinic.models;
using clinic.packpages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace clinic.Controllers
{
    [Route("api/[controller]/[actions]")]
    [ApiController]
    public class Admincontroller : ControllerBase
    {
        private readonly IPKG_ADMIN _package;
        private readonly IPKG_CATEGORY _pkg;


        public Admincontroller(IPKG_ADMIN package,IPKG_CATEGORY pkg)
        {
            _package = package;
            _pkg = pkg;
        }
        [HttpPost("/add_doctor")]
        [Authorize(Policy = "AdminOnly")]

        public IActionResult Add_Doctor([FromForm] Doctor doctor)
        {
            try
            {
                byte[]? cvData = null;
                byte[]? picData = null;

                // Process CV file
                if (doctor.Cv != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        doctor.Cv.CopyTo(ms);
                        cvData = ms.ToArray();
                    }
                }

                // Process Profile Image file
                if (doctor.Profile_img != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        doctor.Profile_img.CopyTo(ms);
                        picData = ms.ToArray();
                    }
                }

                // Call your logic to handle the doctor insertion
                _package.Add_Doctor(doctor,picData,cvData);

                return Ok("Doctor added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"InnerException: {ex.InnerException.Message}");
                }
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpGet("/get_category")]
        public List<Category> Get_Category()
        {
            List<Category> list = new List<Category>();
            try
            {
                list = _pkg.Get_Category();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return list;
        }
        [HttpPost("/update_doctor/{id}")]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult Update_Doctor([FromForm] Doctor_update newData,int id)
        {
            try
            {
                byte[]? picData = null;

                if (newData.Profile_img != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        newData.Profile_img.CopyTo(ms);
                        picData = ms.ToArray();
                    }
                }

                // Call your logic to handle the doctor insertion
                _package.Update_Doctor(id,newData, picData);

                return Ok("Doctor updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"InnerException: {ex.InnerException.Message}");
                }
                return StatusCode(500, new { message = ex.Message });
            }
        }

    }
}
