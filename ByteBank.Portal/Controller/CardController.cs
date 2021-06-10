using ByteBank.Portal.Infra;
using ByteBank.Service;
using ByteBank.Service.Card;

namespace ByteBank.Portal.Controller
{
    public class CardController : ControllerBase
    {
        private ICardService _cardService;

        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }

        public string Credit() => View(new { CardName = _cardService.GetPromotionalCreditCard() });
        public string Debit() => View(new { CardName = _cardService.GetPromotionalDebitCard() });

    }
}