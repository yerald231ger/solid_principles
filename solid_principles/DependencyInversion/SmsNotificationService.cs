namespace SolidPrinciples.DependencyInversion;

/// <summary>
/// Dependency Inversion Principle: Another concrete implementation that can be
/// substituted without changing high-level code
/// </summary>
public class SmsNotificationService : INotificationService
{
    private readonly string _apiKey;
    private readonly string _serviceProvider;

    public SmsNotificationService(string apiKey = "mock-api-key", string serviceProvider = "TwilioMock")
    {
        _apiKey = apiKey;
        _serviceProvider = serviceProvider;
    }

    public bool SendNotification(string recipient, string subject, string message)
    {
        Console.WriteLine($"[SMS] Sending SMS to {recipient}");
        Console.WriteLine($"[SMS] Subject: {subject}");
        Console.WriteLine($"[SMS] Message: {message}");
        Console.WriteLine($"[SMS] Via: {_serviceProvider}");
        
        return SimulateSmsSending();
    }

    public bool SendUrgentNotification(string recipient, string subject, string message)
    {
        Console.WriteLine($"[SMS - URGENT] Priority SMS to {recipient}");
        Console.WriteLine($"[SMS - URGENT] Subject: {subject}");
        Console.WriteLine($"[SMS - URGENT] Message: {message}");
        
        return SimulateSmsSending();
    }

    private bool SimulateSmsSending()
    {
        Console.WriteLine("[SMS] Connecting to SMS gateway...");
        Console.WriteLine("[SMS] SMS sent successfully!");
        return true;
    }
} 