using SolidPrinciples.Models;

namespace SolidPrinciples.LiskovSubstitution;

/// <summary>
/// Liskov Substitution Principle: Enhanced dispenser that can be substituted
/// for the standard dispenser without breaking client code
/// Strengthens postconditions but maintains the contract
/// </summary>
public class HighVolumeDispenser : StandardFuelDispenser
{
    private readonly decimal _minimumDispenseQuantity;

    public HighVolumeDispenser(int pumpNumber, FuelType supportedFuelType, 
                              decimal maxDispenseRate = 100.0m, decimal minimumDispenseQuantity = 10.0m) 
        : base(pumpNumber, supportedFuelType, maxDispenseRate)
    {
        _minimumDispenseQuantity = minimumDispenseQuantity;
    }

    public override bool DispenseFuel(decimal quantity)
    {
        // LSP: We can add additional checks but must maintain the base behavior
        // This strengthens the precondition but maintains compatibility
        if (quantity < _minimumDispenseQuantity)
        {
            Console.WriteLine($"Pump {PumpNumber}: High-volume dispenser requires minimum {_minimumDispenseQuantity:F2} liters");
            return false;
        }

        // Call base implementation to maintain LSP
        return base.DispenseFuel(quantity);
    }

    public override bool StartDispensing()
    {
        Console.WriteLine($"Pump {PumpNumber}: High-volume dispenser starting with enhanced safety checks");
        
        // Additional safety checks for high-volume dispensing
        if (PerformSafetyCheck())
        {
            return base.StartDispensing();
        }

        Console.WriteLine($"Pump {PumpNumber}: Safety check failed, cannot start dispensing");
        return false;
    }

    private bool PerformSafetyCheck()
    {
        // Simulate enhanced safety checks for high-volume dispensing
        Console.WriteLine($"Pump {PumpNumber}: Performing enhanced safety checks...");
        return true;
    }
} 