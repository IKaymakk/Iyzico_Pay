using Iyzico_SanalPos.Models;
using Iyzico_SanalPos.Models.PaymentModels;
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
    [HttpPost]
    public async Task<IActionResult> Pay(PaymentRequestModel model)
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
            CallbackUrl = Url.Action("PayCallBack", "Payments", null, Request.Scheme)
        };

        // Payment card info
        PaymentCard paymentCard = new PaymentCard
        {
            CardHolderName = model.PaymentCard.CardHolderName,
            CardNumber = model.PaymentCard.CardNumber,
            ExpireMonth = model.PaymentCard.ExpireMonth,
            ExpireYear = model.PaymentCard.ExpireYear,
            Cvc = model.PaymentCard.Cvc,
            RegisterCard = 0
        };
        request.PaymentCard = paymentCard;

        // Buyer info
        Buyer buyer = new Buyer
        {
            Id = Guid.NewGuid().ToString(),
            Name = model.Buyer.Name,
            Surname = model.Buyer.Surname,
            GsmNumber = model.Buyer.GsmNumber,
            Email = model.Buyer.Email,
            IdentityNumber = "33131",
            LastLoginDate = "2015-10-05 12:43:35",
            RegistrationDate = "2013-04-21 15:12:09",
            RegistrationAddress = model.Buyer.City,
            City = model.Buyer.City,
            Country = model.Buyer.Country,
            ZipCode = model.Buyer.ZipCode
        };
        request.Buyer = buyer;

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
            Price = "1"
        }
    };
        request.BasketItems = basketItems;

        ThreedsInitialize threedsInitialize = await ThreedsInitialize.Create(request, options);

        if (threedsInitialize.Status != "success")
        {
            return Json(new
            {
                error = true,
                errorMessage = threedsInitialize.ErrorMessage
            });
        }

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

    [HttpGet]
    public IActionResult PaymentForm()
    {
        return View();
    }

}


