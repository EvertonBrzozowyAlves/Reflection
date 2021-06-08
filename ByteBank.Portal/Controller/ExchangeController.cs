using System;
using ByteBank.Portal.Filter;
using ByteBank.Portal.Infra;
using ByteBank.Service;
using ByteBank.Service.Exchange;

namespace ByteBank.Portal.Controller
{
    public class ExchangeController : ControllerBase
    {
        private IExchangeService _exchangeService;
        public ExchangeController()
        {
            _exchangeService = new ExchangeTestService();
        }

        [OnlyBusinessHours]
        public string MXN()
        {
            var finalValue = _exchangeService.Calculate("MXN", "BRL", 1);
            var pageText = View();
            var finalPageText = pageText.Replace("VALOR_EM_REAIS", finalValue.ToString());

            return finalPageText;
        }

        [OnlyBusinessHours]
        public string USD()
        {
            var finalValue = _exchangeService.Calculate("USD ", "BRL", 1);
            var pageText = View();
            var finalPageText = pageText.Replace("VALOR_EM_REAIS", finalValue.ToString());

            return finalPageText;
        }

        [OnlyBusinessHours]
        public string Calculate(string originCurrency, string destinyCurrency, decimal value)
        {
            var currentDate = DateTime.Now;
            var currentHour = currentDate.Hour;
            if (currentHour >= 16 || currentHour <= 9)
                return null;


            var finalValue = _exchangeService.Calculate(originCurrency, destinyCurrency, value);

            var model = new
            {
                DesintyCurrency = destinyCurrency,
                OriginCurrency = originCurrency,
                DesintyValue = finalValue,
                OriginValue = value
            };

            return View(model);
        }

        [OnlyBusinessHours]
        public string Calculate(string destinyCurrency, decimal value) =>
            Calculate("BRL", destinyCurrency, value);

        [OnlyBusinessHours]
        public string Calculate(string destinyCurrency) =>
            Calculate("BRL", destinyCurrency, 1);
    }
}