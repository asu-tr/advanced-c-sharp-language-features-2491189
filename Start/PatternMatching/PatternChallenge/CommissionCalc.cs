// Example file for LinkedIn Learning Course "Advanced C#: Language Features by Joe Marini"
// C# Pattern Matching programming challenge

// Code to calculate the trade commission
public class CommisionCalculator
{
    public static decimal CalculateTradeCommission(SecuritiesTrade trade)
    {
        var totalTradeValue = trade.Quantity * trade.Price;

        if (trade is StockTrade)
            return CalculateStockCommission((StockTrade)trade, totalTradeValue);

        else if (trade is BondTrade)
            return CalculateBondCommission((BondTrade)trade, totalTradeValue);

        return 0.0m;
    }

    private static decimal CalculateStockCommission(StockTrade trade, decimal totalTradeVal) => (trade, totalTradeVal) switch
    {
        ({ Quantity: 0 }, _) => throw new ArgumentException("A Stock trade of 0 shares is caught and flagged as invalid"),
        ({ Quantity: >= 1000 }, >= 10000m) => 5m,
        ({ Quantity: >= 1000 }, _) => 10m,
        (_, < 5000) => totalTradeVal * 0.01m,
        (_, >= 5000) => totalTradeVal * 0.005m,
    };

    private static decimal CalculateBondCommission(BondTrade trade, decimal totalTradeVal) => (trade, totalTradeVal) switch
    {
        ({ Duration: 5 }, >= 10000m) => 20m,
        ({ Duration: 5 }, _) => 20m,
        ({ Duration: 10 }, _) => 12m,
        ({ Duration: 20 }, >= 5000m) => 5m,
        ({ Duration: 20 }, _) => 10m,
        (_, _) => throw new ArgumentException("A Bond trade of any other duration than 5, 10 or 20 is caught and flagged as invalid")
    };

    public static void PrintTradeCommission(SecuritiesTrade trade)
    {
        decimal commission = 0.0m;

        commission = CalculateTradeCommission(trade);

        if (trade is StockTrade)
            Console.WriteLine($"Stock Trade commission is ${commission}");

        else if (trade is BondTrade)
            Console.WriteLine($"Bond Trade commission is ${commission}");

    }
}
