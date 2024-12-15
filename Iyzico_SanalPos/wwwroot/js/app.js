function SendData() {
    var formData = $("#paymentForm").serialize();

    $.ajax({
        url: '/Payments/Pay',
        type: 'POST',
        data: formData,
        success: function (response) {
            if (response.error) {
                alert('Error: ' + response.errorMessage);
            } else {
                $("#resultDiv").html(response.htmlContent);
            }
        },
        error: function () {
            alert('An error occurred while processing the payment.');
        }
    });
}
