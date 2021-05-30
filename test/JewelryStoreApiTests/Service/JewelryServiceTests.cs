using JewelryStoreApi.Model;
using Xunit;

namespace JewelryStoreApiTests.Service
{
    public class JewelryServiceTests
    {
        private IJewelryService _service;
        public JewelryServiceTests()
        {
            _service = new JewelryService();
        }

        [Theory]
        [InlineData(1000, 20, null, 20000)]
        [InlineData(1000, 20, 2, 19600)]
        public void TestCalculate(double pricePerGram, double weight, double? discount, double expectedTotal)
        {
            var jewelryDetail = new JewelryDetail { PricePerGram = pricePerGram, Weight = weight, Discount = discount };

            var actualTotal = _service.Calculate(jewelryDetail);

            Assert.Equal(expectedTotal, actualTotal);
        }

        [Fact]
        public void TestDownloadPdf()
        {
            var jewelryDetail = new JewelryDetail { PricePerGram = 1000, Weight = 20, Discount = 2 };

            var result = _service.CreateByteArray(jewelryDetail);

            Assert.NotNull(result);
        }

    }
}
