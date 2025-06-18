using SolidPrinciples.Models;

namespace SolidPrinciples.LiskovSubstitution;

/// <summary>
/// Liskov Substitution Principle: This class works with any IFuelDispenser implementation
/// All dispensers are substitutable without changing this code
/// </summary>
public class DispenserManager
{
    private readonly List<IFuelDispenser> _dispensers;

    public DispenserManager()
    {
        _dispensers = new List<IFuelDispenser>();
    }

    public void AddDispenser(IFuelDispenser dispenser)
    {
        _dispensers.Add(dispenser);
        Console.WriteLine($"Added {dispenser.GetType().Name} (Pump {dispenser.PumpNumber}) for {dispenser.SupportedFuelType}");
    }

    public bool ProcessFuelRequest(FuelType fuelType, decimal quantity)
    {
        // LSP: Any IFuelDispenser implementation should work here
        var availableDispenser = _dispensers
            .FirstOrDefault(d => d.SupportedFuelType == fuelType && d.IsOperational);

        if (availableDispenser == null)
        {
            Console.WriteLine($"No operational dispenser available for {fuelType}");
            return false;
        }

        Console.WriteLine($"Processing fuel request using Pump {availableDispenser.PumpNumber}");
        var success = availableDispenser.DispenseFuel(quantity);
        
        if (success)
        {
            availableDispenser.StopDispensing();
        }

        return success;
    }

    public void GenerateReport()
    {
        Console.WriteLine("\n=== Dispenser Status Report ===");
        foreach (var dispenser in _dispensers)
        {
            Console.WriteLine($"Pump {dispenser.PumpNumber} ({dispenser.SupportedFuelType}):");
            Console.WriteLine($"  Status: {(dispenser.IsOperational ? "Operational" : "Out of Service")}");
            Console.WriteLine($"  Total Dispensed: {dispenser.GetTotalDispensed():F2}");
            Console.WriteLine($"  Max Rate: {dispenser.MaxDispenseRate:F2}");
            Console.WriteLine();
        }
    }

    public List<IFuelDispenser> GetDispensersByFuelType(FuelType fuelType)
    {
        return _dispensers.Where(d => d.SupportedFuelType == fuelType).ToList();
    }
} 