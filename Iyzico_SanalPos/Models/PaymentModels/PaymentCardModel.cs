namespace Iyzico_SanalPos.Models.PaymentModels
{
    public class PaymentCardModel
    {
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public string ExpireMonth { get; set; }
        public string ExpireYear { get; set; }
        public string Cvc { get; set; }
    }
}
