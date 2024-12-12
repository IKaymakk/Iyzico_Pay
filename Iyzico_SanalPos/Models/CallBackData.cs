namespace Iyzico_SanalPos.Models
{
    public sealed record CallBackData(
        string status,
        string paymentId,
        string conversationData,
        string mdStatus,
        string conversationId // Eğer 'long' olarak kullanmanız gerekiyorsa, türü 'long' yapın.
    );
}
