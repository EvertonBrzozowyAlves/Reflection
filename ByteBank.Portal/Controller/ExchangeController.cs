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

        public string MXN()
        {
            var finalValue = _exchangeService.Calculate("MXN", "BRL", 1);
            var pageText = View();
            var finalPageText = pageText.Replace("VALOR_EM_REAIS", finalValue.ToString());

            return finalPageText;
        }

        public string USD()
        {
            var finalValue = _exchangeService.Calculate("USD ", "BRL", 1);
            var pageText = View();
            var finalPageText = pageText.Replace("VALOR_EM_REAIS", finalValue.ToString());

            return finalPageText;
        }

        public string Calculate(string originCurrency, string destinyCurrency, decimal value)
        {
            var finalValue = _exchangeService.Calculate(originCurrency, destinyCurrency, value);
            var pageText = View();
            var finalPageText =
                pageText
                    .Replace("VALOR_MOEDA_ORIGEM", value.ToString())
                    .Replace("VALOR_MOEDA_DESTINO", finalValue.ToString())
                    .Replace("MOEDA_ORIGEM", originCurrency)
                    .Replace("MOEDA_DESTINO", destinyCurrency);


            return finalPageText;
        }

        public string Calculate(string destinyCurrency, decimal value) =>
            Calculate("BRL", destinyCurrency, value);

        public string Calculate(string destinyCurrency) =>
            Calculate("BRL", destinyCurrency, 1);
    }
}