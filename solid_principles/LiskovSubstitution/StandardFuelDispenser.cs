using SolidPrinciples.Models;

namespace SolidPrinciples.LiskovSubstitution;

/// <summary>
/// Liskov Substitution Principle: Standard implementation that can be substituted
/// for the base interface without breaking functionality
/// </summary>
public class StandardFuelDispenser : IFuelDispenser
{
    public int PumpNumber { get; }
    public FuelType SupportedFuelType { get; }
    public bool IsOperational { get; private set; }
    public decimal MaxDispenseRate { get; }

    private decimal _totalDispensed;
    private bool _isDispensing;

    public StandardFuelDispenser(int pumpNumber, FuelType supportedFuelType, decimal maxDispenseRate = 50.0m)
    {
        PumpNumber = pumpNumber;
        SupportedFuelType = supportedFuelType;
        MaxDispenseRate = maxDispenseRate;
        IsOperational = true;
        _totalDispensed = 0;
        _isDispensing = false;
    }

    public virtual bool DispenseFuel(decimal quantity)
    {
        if (!IsOperational || quantity <= 0 || quantity > MaxDispenseRate)
        {
            return false;
        }

        if (!_isDispensing)
        {
            StartDispensing();
        }

        _totalDispensed += quantity;
        Console.WriteLine($"Pump {PumpNumber}: Dispensed {quantity:F2} liters of {SupportedFuelType}");
        
        return true;
    }

    public virtual bool StartDispensing()
    {
        if (!IsOperational || _isDispensing)
        {
            return false;
        }

        _isDispensing = true;
        Console.WriteLine($"Pump {PumpNumber}: Started dispensing {SupportedFuelType}");
        return true;
    }

    public virtual bool StopDispensing()
    {
        if (!_isDispensing)
        {
            return false;
        }

        _isDispensing = false;
        Console.WriteLine($"Pump {PumpNumber}: Stopped dispensing");
        return true;
    }

    public decimal GetTotalDispensed()
    {
        return _totalDispensed;
    }

    public virtual void ResetCounter()
    {
        _totalDispensed = 0;
        Console.WriteLine($"Pump {PumpNumber}: Counter reset");
    }
} 