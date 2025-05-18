namespace TaskApplication;

public class CurrencyConverter 

{
    public void Application()
    {
        CurrencyConverter currencyConverter = new CurrencyConverter();
        
        (string fromCurrency, string toCurrency) result = currencyConverter.Currency();

        decimal converted = currencyConverter.Conversion(result.fromCurrency, result.toCurrency);

        Console.WriteLine($"{result.fromCurrency} в {result.toCurrency}: {converted}");
    }
    public (string fromCurrency, string toCurrency)  Currency()
    {
        Console.WriteLine("З якої валюти ви хочете конвертувати? (EUR, UAH, USD)");
        string fromCurrency = Console.ReadLine().ToUpper();
        
        Console.WriteLine("В яку валюти ви хочете конвертувати? (EUR, UAH, USD)");
        string toCurrency = Console.ReadLine().ToUpper();
        
        if (toCurrency == fromCurrency)
        {
            Console.WriteLine("Ви вказали однакові валюти, cпробуйте ще раз! (EUR, UAH, USD)");
            return Currency();
        }
        
       return (fromCurrency, toCurrency); 
    }

    public decimal Conversion(string fromCurrency, string toCurrency)
    {
        Console.WriteLine($"Вкажіть суму в {fromCurrency} яку ви хочете конвертувати у {toCurrency}");
        
        decimal amount;
        while (!decimal.TryParse(Console.ReadLine(), out amount)|| amount < 0)
        {
            Console.WriteLine("Введіть додатнє число:");
        }
        
        decimal fromRate = GetUSDRate(fromCurrency);
        decimal toRate = GetUSDRate(toCurrency);
        
        if (fromRate == 0 || toRate == 0)
        {
            Console.WriteLine("Непідтримувана валюта.");
            return 0m;
        }

        decimal amountInUsd = amount / fromRate;
        decimal convertedAmount = amountInUsd * toRate;

        return convertedAmount;
    }

    private decimal GetUSDRate(string currency)
    {
        return currency switch
        {
            "USD" => 1m,
            "EUR" => 0.90m,
            "UAH" => 41.51m,
            _ => 0
        };
    
    }
}