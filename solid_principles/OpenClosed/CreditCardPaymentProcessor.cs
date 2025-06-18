namespace SolidPrinciples.OpenClosed;

/// <summary>
/// Open/Closed Principle: Another concrete implementation that extends functionality
/// without modifying existing payment processing code
/// </summary>
public class CreditCardPaymentProcessor : IPaymentProcessor
{
    public bool ProcessPayment(decimal amount, string customerInfo)
    {
        Console.WriteLine($"Processing credit card payment of ${amount:F2}");
        Console.WriteLine($"Customer: {customerInfo}");
        
        // Simulate credit card processing
        if (ConnectToPaymentGateway() && AuthorizeTransaction(amount))
        {
            Console.WriteLine("Credit card payment authorized and completed.");
            return true;
        }
        
        Console.WriteLine("Credit card payment failed.");
        return false;
    }

    public string GetPaymentMethodName()
    {
        return "Credit Card";
    }

    public bool ValidatePaymentDetails(string paymentDetails)
    {
        // Basic credit card validation (simplified)
        return !string.IsNullOrWhiteSpace(paymentDetails) && 
               paymentDetails.Length >= 15 && 
               paymentDetails.All(char.IsDigit);
    }

    private bool ConnectToPaymentGateway()
    {
        // Simulate connection to payment gateway
        Console.WriteLine("Connecting to payment gateway...");
        return true;
    }

    private bool AuthorizeTransaction(decimal amount)
    {
        // Simulate transaction authorization
        Console.WriteLine($"Authorizing transaction for ${amount:F2}...");
        return amount <= 10000; // Simulate limit
    }
} 