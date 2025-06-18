namespace SolidPrinciples.OpenClosed;

/// <summary>
/// Open/Closed Principle: New payment method added without modifying existing code
/// This demonstrates how the system is open for extension
/// </summary>
public class MobilePaymentProcessor : IPaymentProcessor
{
    public bool ProcessPayment(decimal amount, string customerInfo)
    {
        Console.WriteLine($"Processing mobile payment of ${amount:F2}");
        Console.WriteLine($"Customer: {customerInfo}");
        
        if (SendPaymentRequest(amount) && VerifyMobileApp())
        {
            Console.WriteLine("Mobile payment completed successfully.");
            return true;
        }
        
        Console.WriteLine("Mobile payment failed.");
        return false;
    }

    public string GetPaymentMethodName()
    {
        return "Mobile Payment";
    }

    public bool ValidatePaymentDetails(string paymentDetails)
    {
        // Validate mobile payment details (phone number or app token)
        return !string.IsNullOrWhiteSpace(paymentDetails) && 
               (paymentDetails.StartsWith("+") || paymentDetails.Length == 10);
    }

    private bool SendPaymentRequest(decimal amount)
    {
        Console.WriteLine("Sending payment request to mobile app...");
        return true;
    }

    private bool VerifyMobileApp()
    {
        Console.WriteLine("Verifying mobile app authentication...");
        return true;
    }
} 