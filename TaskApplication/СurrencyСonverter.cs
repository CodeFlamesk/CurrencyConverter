namespace TaskApplication;

public class CurrencyConverter 

{
    private readonly CreditCard _creditCard = new CreditCard();
    
    public void Application()
    {
        while (true)
        {
           _creditCard.PrintBalances();
           Console.WriteLine("Введіть 'exit' щоб завершити, натисніть Enter щоб продовжити  ");
           string userAnswer = Console.ReadLine().ToLower();
           if (userAnswer == "exit") break;
           (string fromCurrency, string toCurrency) = Currency();
           Console.WriteLine($"Вкажіть суму в {fromCurrency} для конвертацію на банківську картку у {toCurrency}:");
           decimal amount;
           while (!decimal.TryParse(Console.ReadLine(), out amount) || amount <= 0)
           {
               Console.WriteLine("Введіть додатнє число:");
           }

           decimal currentBalance = _creditCard.GetBalance(fromCurrency);
           if (amount > currentBalance)
           {
               Console.WriteLine("Недостатньо коштів для цієї операції, знайдіть роботу");
               continue;
           }
           decimal convertedAmount = ConvertCurrency(amount, fromCurrency, toCurrency);
           _creditCard.SetBalance(fromCurrency, currentBalance - amount);
           
           decimal targetBalance = _creditCard.GetBalance(toCurrency);
           _creditCard.SetBalance(toCurrency, targetBalance + convertedAmount);
           
           Console.WriteLine($"/nКонвертацію зроблено успішно! {amount} {fromCurrency} → {convertedAmount:F1} {toCurrency} ");
          
        }
       
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

    private decimal ConvertCurrency(decimal amount, string fromCurrency, string toCurrency)
    {
        decimal fromRate = GetUSDRate(fromCurrency);
        decimal toRate = GetUSDRate(toCurrency);

        if (fromRate == 0 || toRate == 0)
        {
            Console.WriteLine("Непідтримувана валюта.");
            return 0m;
        }

        decimal amountInUsd = amount / fromRate;
        return amountInUsd * toRate;
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