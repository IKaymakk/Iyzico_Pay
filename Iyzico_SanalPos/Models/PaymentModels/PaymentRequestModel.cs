namespace Iyzico_SanalPos.Models.PaymentModels
{
    public class PaymentRequestModel
    {
        public PaymentCardModel PaymentCard { get; set; }
        public BuyerModel Buyer { get; set; }
        public AddressModel ShippingAddress { get; set; }
        public AddressModel BillingAddress { get; set; }
        public List<BasketItemModel> BasketItems { get; set; }
    }
}
