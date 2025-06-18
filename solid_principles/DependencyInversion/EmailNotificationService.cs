namespace SolidPrinciples.DependencyInversion;

/// <summary>
/// Dependency Inversion Principle: Concrete implementation that high-level modules
/// don't directly depend on - they depend on the INotificationService abstraction
/// </summary>
public class EmailNotificationService : INotificationService
{
    private readonly string _smtpServer;
    private readonly int _port;

    public EmailNotificationService(string smtpServer = "smtp.company.com", int port = 587)
    {
        _smtpServer = smtpServer;
        _port = port;
    }

    public bool SendNotification(string recipient, string subject, string message)
    {
        Console.WriteLine($"[EMAIL] Sending email to {recipient}");
        Console.WriteLine($"[EMAIL] Subject: {subject}");
        Console.WriteLine($"[EMAIL] Message: {message}");
        Console.WriteLine($"[EMAIL] Via SMTP: {_smtpServer}:{_port}");
        
        // Simulate email sending
        return SimulateEmailSending();
    }

    public bool SendUrgentNotification(string recipient, string subject, string message)
    {
        Console.WriteLine($"[EMAIL - URGENT] High priority email to {recipient}");
        Console.WriteLine($"[EMAIL - URGENT] Subject: {subject}");
        Console.WriteLine($"[EMAIL - URGENT] Message: {message}");
        
        return SimulateEmailSending();
    }

    private bool SimulateEmailSending()
    {
        Console.WriteLine("[EMAIL] Connecting to SMTP server...");
        Console.WriteLine("[EMAIL] Email sent successfully!");
        return true;
    }
} 