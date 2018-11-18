namespace AnyCompany
{
    public class UKOrderRequest :OrderRequest
    {
        public override Order GetOrderRequest(Order order)
        {
            order.VAT = 0.2d;
            return order;
        }
    }
}