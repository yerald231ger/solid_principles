namespace SolidPrinciples.DependencyInversion;

/// <summary>
/// Dependency Inversion Principle: High-level modules depend on this abstraction,
/// not on concrete notification implementations
/// </summary>
public interface INotificationService
{
    bool SendNotification(string recipient, string subject, string message);
    bool SendUrgentNotification(string recipient, string subject, string message);
} 