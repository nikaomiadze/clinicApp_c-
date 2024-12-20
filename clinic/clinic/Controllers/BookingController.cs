using clinic.models;
using clinic.packpages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace clinic.Controllers
{
    [Route("api/[controller]/[actions]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IPKG_BOOKING package;
        public BookingController(IPKG_BOOKING pkg)
        {
            package = pkg;
        }
        [HttpPost("/add-booking")]
        public IActionResult Add_Booking([FromBody] Booking booking)
        {
            try
            {
                package.Add_booking(booking);
                return Ok("Booking added successfully");

            }
            catch (Exception ex) { 
            Console.WriteLine(ex.Message);
            }
            return Ok();
        }



    }
}
