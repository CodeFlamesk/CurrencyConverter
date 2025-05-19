namespace TaskApplication;

public class CreditCard
{
    public decimal BalanceUAH { get; set; } = 10000m;
    public decimal BalanceUSD { get; set; } = 1000m;
    public decimal BalanceEUR { get; set; } = 1500m;
    
    public decimal GetBalance(string currency)
    {
        return currency switch
        {
            "UAH" => BalanceUAH,
            "USD" => BalanceUSD,
            "EUR" => BalanceEUR,
            _ => 0
        };
    }

    public void SetBalance(string currency, decimal newBalance)
    {
       switch (currency)
        {
            case "USD":
                BalanceUSD = newBalance;
                break; 
            case "EUR":
                BalanceEUR = newBalance;
                break; 
            case "UAH":
                BalanceUAH = newBalance;
                break;
        }
    }
    public void PrintBalances()
    {
        Console.WriteLine($"\nВаш баланс:");
        Console.WriteLine($"UAH: {BalanceUAH}");
        Console.WriteLine($"USD: {BalanceUSD}");
        Console.WriteLine($"EUR: {BalanceEUR}\n");
    }

}