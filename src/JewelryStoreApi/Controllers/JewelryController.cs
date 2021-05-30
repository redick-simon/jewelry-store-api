using JewelryStoreApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace JewelryStoreApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JewelryController : ControllerBase
    {
        private readonly IJewelryService _jewelryService;

        public JewelryController(IJewelryService service)
        {
            _jewelryService = service;
        }

        [HttpPost("Calculate")]
        public IActionResult Calculate(JewelryDetail detail)
        {
            double totalPrice = _jewelryService.Calculate(detail);
           

            return Ok(new { TotalPrice = totalPrice });

        }

        [HttpPost("DownloadPdf")]
        public IActionResult DownloadPdf(JewelryDetail detail)
        {

            byte[]  bytes = _jewelryService.CreateByteArray(detail);

            HttpContext.Response.ContentType = "application/pdf";

            FileContentResult result = new FileContentResult(bytes, "application/pdf")
            {
                FileDownloadName = "recipt.pdf"
            };

            return result;

        }
    }
}
