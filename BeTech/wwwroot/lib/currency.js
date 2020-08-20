$(document).ready(function () {

    $.ajax('Currency/NeedUpdateCurrency', {
        success: function (result) {
            console.log(result);
            if (result == true) {
                var confirmUpdate = confirm("Обновить курс валют на сегодня?");
                if (confirmUpdate === true)
                    setActualRate();
            }
        }
    });

    function setActualRate() {
        $.ajax('https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?json', {
            success: function (data) {
                sendRate(data);
            }
        });
    }

    function sendRate(data) {
        $.ajax('Currency/UpdateCurrencies', {
            type: "post",
            data: { currencyData: data },
            dataType: "json",
            success: function (result) {
                if (result == true) alert("Курсы валют успешно обновлены");
                else alert("Ошибка обновления курсов валют");
            }
        });
    }

});