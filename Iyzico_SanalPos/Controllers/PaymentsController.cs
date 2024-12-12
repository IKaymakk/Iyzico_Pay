using Iyzico_SanalPos.Models;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Mvc;

namespace Iyzico_SanalPos.Controllers;

public class PaymentsController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
    [HttpGet]
    public async Task<IActionResult> Pay()
    {
        Options options = new()
        {
            ApiKey = "sandbox-SnXN8jqHAclrSypy8psRPAeiCunW4JQj",
            SecretKey = "sandbox-8bUMTmiMn4u67p7S2FdqPozkJh7buITJ",
            BaseUrl = "https://sandbox-api.iyzipay.com"
        };

        // Create payment request
        CreatePaymentRequest request = new CreatePaymentRequest
        {
            Locale = Locale.TR.ToString(),
            ConversationId = Guid.NewGuid().ToString(),
            Price = "1",
            PaidPrice = "1.2",
            Currency = Currency.TRY.ToString(),
            Installment = 1,
            BasketId = "B67832",
            PaymentChannel = PaymentChannel.WEB.ToString(),
            PaymentGroup = PaymentGroup.PRODUCT.ToString(),
            CallbackUrl = Url.Action("PayCallBack", "Payments", null, Request.Scheme) // MVC için Callback URL
        };

        // Payment card info
        PaymentCard paymentCard = new PaymentCard
        {
            CardHolderName = "John Doe",
            CardNumber = "5528790000000008",
            ExpireMonth = "12",
            ExpireYear = "2030",
            Cvc = "123",
            RegisterCard = 0
        };
        request.PaymentCard = paymentCard;

        // Buyer info
        Buyer buyer = new Buyer
        {
            Id = "BY789",
            Name = "John",
            Surname = "Doe",
            GsmNumber = "+905350000000",
            Email = "email@email.com",
            IdentityNumber = "74300864791",
            LastLoginDate = "2015-10-05 12:43:35",
            RegistrationDate = "2013-04-21 15:12:09",
            RegistrationAddress = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1",
            Ip = "85.34.78.112",
            City = "Istanbul",
            Country = "Turkey",
            ZipCode = "34732"
        };
        request.Buyer = buyer;

        // Shipping and billing addresses
        Address shippingAddress = new Address
        {
            ContactName = "Jane Doe",
            City = "Istanbul",
            Country = "Turkey",
            Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1",
            ZipCode = "34742"
        };
        request.ShippingAddress = shippingAddress;

        Address billingAddress = new Address
        {
            ContactName = "Jane Doe",
            City = "Istanbul",
            Country = "Turkey",
            Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1",
            ZipCode = "34742"
        };
        request.BillingAddress = billingAddress;

        // Basket items
        List<BasketItem> basketItems = new List<BasketItem>
        {
            new BasketItem
            {
                Id = "BI101",
                Name = "Binocular",
                Category1 = "Collectibles",
                Category2 = "Accessories",
                ItemType = BasketItemType.PHYSICAL.ToString(),
                Price = "0.3"
            },
            new BasketItem
            {
                Id = "BI102",
                Name = "Game code",
                Category1 = "Game",
                Category2 = "Online Game Items",
                ItemType = BasketItemType.VIRTUAL.ToString(),
                Price = "0.5"
            },
            new BasketItem
            {
                Id = "BI103",
                Name = "Usb",
                Category1 = "Electronics",
                Category2 = "Usb / Cable",
                ItemType = BasketItemType.PHYSICAL.ToString(),
                Price = "0.2"
            }
        };
        request.BasketItems = basketItems;

        // Initialize 3D Secure payment
        ThreedsInitialize threedsInitialize = await ThreedsInitialize.Create(request, options);

        // If the payment initialization fails
        if (threedsInitialize.Status != "success")
        {
            return Json(new
            {
                error = true,
                errorMessage = threedsInitialize.ErrorMessage
            });
        }

        // Return the HTML content for 3D Secure
        return Json(new
        {
            htmlContent = threedsInitialize.HtmlContent,
            error = false
        });
    }

    [HttpPost]
    public IActionResult PayCallBack([FromForm] ThreedsCallbackModel callbackData)
    {
        if (callbackData.Status == "failure")
        {
            // Hata durumunda kullanıcıyı bilgilendirin
            string errorMessage = callbackData.ErrorMessage ?? "Bilinmeyen bir hata oluştu.";
            string errorCode = callbackData.ErrorCode ?? "UnknownError";

            // Hata bilgilerini kullanıcıya göstermek için
            ViewData["ErrorMessage"] = $"Ödeme başarısız: {errorMessage} (Kod: {errorCode})";
        }
        else
        {
            // Başarılı ödeme durumu
            ViewData["SuccessMessage"] = "Ödeme başarılı!";
        }

        return View(callbackData); // Hata veya başarılı sonucu View'a gönder
    }


}


