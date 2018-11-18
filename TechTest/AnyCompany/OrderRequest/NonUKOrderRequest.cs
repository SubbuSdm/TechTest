namespace AnyCompany
{
    class NonUKOrderRequest : OrderRequest
    {
        public override Order GetOrderRequest(Order order)
        {
            order.VAT = 0;
            return order;
        }
    }
}
