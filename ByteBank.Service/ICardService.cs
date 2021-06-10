using System;

namespace ByteBank.Service
{
    public interface ICardService
    {
        string GetPromotionalCreditCard();
        string GetPromotionalDebitCard();
    }
}
