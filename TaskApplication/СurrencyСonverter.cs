namespace TaskApplication;

public class CurrencyConverter

{
    private readonly CreditCard _creditCard = new CreditCard();

    public void Application()
    {
        while (true)
        {
            string exitOrContinuePrompt = "Введіть 'exit' щоб завершити, натисніть Enter щоб продовжити";
            string amountInputLabel = "Вкажіть суму в";
            string conversionTargetText = "для конвертацію на банківську картку у";
            string positiveNumberPrompt = "Введіть додатнє число:";
            string insufficientFundsText = "Недостатньо коштів для цієї операції, знайдіть роботу";
            string successMessage = "Конвертацію зроблено успішно!";

            _creditCard.PrintBalances();

            Console.WriteLine(exitOrContinuePrompt);
            string userAnswer = Console.ReadLine().ToLower();

            if (userAnswer == "exit")
            {
                break;
            }
            (CreditCard.Balance fromCurrency, CreditCard.Balance toCurrency) = GetCurrency();
            Console.WriteLine($"{amountInputLabel} {fromCurrency} {conversionTargetText} {toCurrency}:");
            decimal amount;
            while (!decimal.TryParse(Console.ReadLine(), out amount) || amount <= 0)
            {
                Console.WriteLine(positiveNumberPrompt);
            }

            decimal currentBalance = _creditCard.GetBalance(fromCurrency);
            if (amount > currentBalance)
            {
                Console.WriteLine(insufficientFundsText);
                continue;
            }

            decimal convertedAmount = ConvertCurrency(amount, fromCurrency, toCurrency);
            _creditCard.SetBalance(fromCurrency, currentBalance - amount);

            decimal targetBalance = _creditCard.GetBalance(toCurrency);
            _creditCard.SetBalance(toCurrency, targetBalance + convertedAmount);

            Console.WriteLine(
                $"\n{successMessage} {amount} {fromCurrency} → {convertedAmount:F1} {toCurrency} ");
        }
    }

    private (CreditCard.Balance fromCurrency, CreditCard.Balance toCurrency) GetCurrency()
    {
        string fromCurrencyPrompt = "З якої валюти ви хочете конвертувати? (EUR, UAH, USD)";
        string toCurrencyPrompt = "В яку валюту ви хочете конвертувати? (EUR, UAH, USD)";
        string invalidCurrencyMessage = "Валюта введена некоректно. Спробуйте ще раз.";
        string sameCurrencyWarning = "Ви вказали однакові валюти, спробуйте ще раз!";

        Console.WriteLine(fromCurrencyPrompt);
        string fromCurrencyInput = Console.ReadLine().ToUpper();

        Console.WriteLine(toCurrencyPrompt);
        string toCurrencyInput = Console.ReadLine().ToUpper();

        if (!Enum.TryParse(fromCurrencyInput, true, out CreditCard.Balance fromCurrency) ||
            !Enum.TryParse(toCurrencyInput, true, out CreditCard.Balance toCurrency))
        {
            Console.WriteLine(invalidCurrencyMessage);
            return GetCurrency();
        }

        if (toCurrency == fromCurrency)
        {
            Console.WriteLine(sameCurrencyWarning);
            return GetCurrency();
        }

        return (fromCurrency, toCurrency);
    }


    private decimal ConvertCurrency(decimal amount, CreditCard.Balance fromCurrency, CreditCard.Balance toCurrency)
    {
        string unsupportedCurrencyMessage = "Непідтримувана валюта.";

        decimal fromRate = GetUSDRate(fromCurrency);
        decimal toRate = GetUSDRate(toCurrency);

        if (fromRate == 0 || toRate == 0)
        {
            Console.WriteLine(unsupportedCurrencyMessage);
            return 0m;
        }

        decimal amountInUsd = amount / fromRate;
        return amountInUsd * toRate;
    }

    private decimal GetUSDRate(CreditCard.Balance currency)
    {
        return currency switch
        {
            CreditCard.Balance.USD => 1m,
            CreditCard.Balance.EUR => 0.90m,
            CreditCard.Balance.UAH => 41.51m,
            _ => 0
        };
    }
}