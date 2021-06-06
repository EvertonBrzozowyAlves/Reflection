using System.IO;
using System.Reflection;
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
    }
}