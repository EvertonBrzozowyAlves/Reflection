using System;

namespace ByteBank.Service.Exchange
{
    public class ExchangeTestService : IExchangeService
    {
        private readonly Random _random = new Random();
        public decimal Calculate(string originCurrency, string destinyCurrency, decimal value) => value * (decimal)_random.NextDouble();
    }
}