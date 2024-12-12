namespace Iyzico_SanalPos.Models
{
    public class ThreedsCallbackModel
    {
        public string Status { get; set; } // Ödeme durumu (success, failure)
        public string PaymentId { get; set; } // Ödeme ID
        public string ConversationId { get; set; } // Konuşma ID
        public string ConversationData { get; set; } // Konuşma verisi
        public string MdStatus { get; set; } // 3D Secure durumu
        public string ErrorMessage { get; set; } // Hata mesajı (varsa)
        public string ErrorCode { get; set; } // Hata kodu (varsa)
    }

}
