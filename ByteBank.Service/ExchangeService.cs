using System;

namespace ByteBank.Service
{
    public interface IExchangeService
    {
        decimal Calculate(string originCurrency, string destinyCurrency, decimal value);
    }
}
