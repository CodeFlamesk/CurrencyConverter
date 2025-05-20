namespace TaskApplication;

public class CreditCard
{
    public decimal BalanceUAH { get; set; } = 10000m;
    public decimal BalanceUSD { get; set; } = 1000m;
    public decimal BalanceEUR { get; set; } = 1500m;
   public enum Balance 
    {
        UAH,
        USD,
        EUR
    }
    public decimal GetBalance(Balance currency)
    {
      
        return currency switch
        {
            Balance.UAH => BalanceUAH,
            Balance.USD => BalanceUSD,
            Balance.EUR => BalanceEUR,
            _ => 0
        };
    }

    public void SetBalance(Balance currency, decimal newBalance)
    {
       switch (currency)
        {
            case Balance.USD:
                BalanceUSD = newBalance;
                break; 
            case Balance.EUR:
                BalanceEUR = newBalance;
                break; 
            case Balance.UAH:
                BalanceUAH = newBalance;
                break;
        }
    }
    
    public static bool TryParseCurrency(string input, out Balance currency)
    {
        return Enum.TryParse(input, true, out currency);
    }
    public void PrintBalances()
    {
        Console.WriteLine($"\nВаш баланс:");
        Console.WriteLine($"UAH: {BalanceUAH}");
        Console.WriteLine($"USD: {BalanceUSD}");
        Console.WriteLine($"EUR: {BalanceEUR}\n");
    }

}