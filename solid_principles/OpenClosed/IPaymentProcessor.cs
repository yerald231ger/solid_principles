using SolidPrinciples.Models;

namespace SolidPrinciples.OpenClosed;

/// <summary>
/// Open/Closed Principle: This interface is open for extension but closed for modification
/// New payment methods can be added by implementing this interface without changing existing code
/// </summary>
public interface IPaymentProcessor
{
    bool ProcessPayment(decimal amount, string customerInfo);
    string GetPaymentMethodName();
    bool ValidatePaymentDetails(string paymentDetails);
} 