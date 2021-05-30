using JewelryStoreApi.Controllers;
using JewelryStoreApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Xunit;

namespace JewelryStoreApiTests
{
    public class JewelryControllerTests
    {
        private IJewelryService _jewelryService;
        private JewelryController _controller;

        public JewelryControllerTests()
        {
            _jewelryService = new JewelryService();
            _controller = new JewelryController(_jewelryService);
;       }


        [Theory]
        [InlineData(1000, 20, null, 20000)]
        [InlineData(1000, 20, 2, 19600)]
        public void TestCalculate(double pricePerGram, double weight, double? discount, double expectedPrice)
        {
            var jewelryDetail = new JewelryDetail { PricePerGram = pricePerGram, Weight = weight, Discount = discount };
            var result = _controller.Calculate(jewelryDetail) as ObjectResult;

            Assert.Equal(expectedPrice, result.Value.GetType().GetProperty("TotalPrice").GetValue(result.Value, null));
        }

        [Fact]
        public void TestDownloadPdf()
        {
            var jewelryDetail = new JewelryDetail { PricePerGram = 1000, Weight = 20, Discount = 2 };

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            var result = _controller.DownloadPdf(jewelryDetail);

            Assert.IsType<FileContentResult>(result);

            var fileResult = result as FileContentResult;

            Assert.Equal("application/pdf", fileResult.ContentType);
            Assert.Equal("recipt.pdf", fileResult.FileDownloadName);
            Assert.NotNull(fileResult.FileContents);
        }
    }
}
