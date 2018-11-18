namespace AnyCompany
{
    public class OrderRequestFactory
    {
        public static Order GenerateOrderRequest(Order order, string country)
        {
            switch (country)
            {
                case "UK":
                    order = new UKOrderRequest().GetOrderRequest(order);
                    break;
                default:
                    order = new NonUKOrderRequest().GetOrderRequest(order);
                    break;
            }

            return order;
        }
    }
}
