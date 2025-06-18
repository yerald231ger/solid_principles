using SolidPrinciples.Models;

namespace SolidPrinciples.OpenClosed;

/// <summary>
/// Open/Closed Principle: Factory that can be extended with new payment processors
/// without modifying existing code (through registration pattern)
/// </summary>
public class PaymentProcessorFactory
{
    private readonly Dictionary<PaymentMethod, Func<IPaymentProcessor>> _processors;

    public PaymentProcessorFactory()
    {
        _processors = new Dictionary<PaymentMethod, Func<IPaymentProcessor>>();
        RegisterDefaultProcessors();
    }

    public IPaymentProcessor CreateProcessor(PaymentMethod paymentMethod)
    {
        if (_processors.ContainsKey(paymentMethod))
        {
            return _processors[paymentMethod]();
        }

        throw new NotSupportedException($"Payment method {paymentMethod} is not supported.");
    }

    public void RegisterProcessor(PaymentMethod paymentMethod, Func<IPaymentProcessor> processorFactory)
    {
        _processors[paymentMethod] = processorFactory;
    }

    public IEnumerable<PaymentMethod> GetSupportedPaymentMethods()
    {
        return _processors.Keys;
    }

    private void RegisterDefaultProcessors()
    {
        RegisterProcessor(PaymentMethod.Cash, () => new CashPaymentProcessor());
        RegisterProcessor(PaymentMethod.CreditCard, () => new CreditCardPaymentProcessor());
        RegisterProcessor(PaymentMethod.MobilePayment, () => new MobilePaymentProcessor());
    }
} 