namespace SolidPrinciples.OpenClosed;

/// <summary>
/// Open/Closed Principle: Concrete implementation that can be added without modifying existing code
/// </summary>
public class CashPaymentProcessor : IPaymentProcessor
{
    public bool ProcessPayment(decimal amount, string customerInfo)
    {
        // Cash payments are always processed immediately
        Console.WriteLine($"Processing cash payment of ${amount:F2}");
        Console.WriteLine($"Customer: {customerInfo}");
        Console.WriteLine("Cash payment completed successfully.");
        
        return true;
    }

    public string GetPaymentMethodName()
    {
        return "Cash";
    }

    public bool ValidatePaymentDetails(string paymentDetails)
    {
        // Cash doesn't need validation of payment details
        return true;
    }
} 