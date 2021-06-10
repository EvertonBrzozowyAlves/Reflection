namespace ByteBank.Service.Card
{
    public class CardTestService : ICardService
    {
        public string GetPromotionalCreditCard() => "ByteBank Gold Platinum Extra Premium Special";
        public string GetPromotionalDebitCard() => "ByteBank Student No Fee";
    }
}