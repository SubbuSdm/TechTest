using NUnit.Framework;
using AnyCompany;

namespace AnyCompany.Tests
{
    class OrderRequestFactoryTest
    {
        [Test]
        public void GivenRequestOrderWithCountryUKThenOrderReturnsWithVAT()
        {
            var order = new Order() { Amount = 100, VAT = 0};

            var orderResult = OrderRequestFactory.GenerateOrderRequest(order, "UK");

            Assert.IsTrue(orderResult.VAT == 0.2d);

        }

        [Test]
        public void GivenRequestOrderWithCountryNonUKThenOrderReturnsWithZeroVAT()
        {
            var order = new Order() { Amount = 100, VAT = 0 };

            var orderResult = OrderRequestFactory.GenerateOrderRequest(order, "FR");

            Assert.IsTrue(orderResult.VAT == 0);
        }
    }
}
