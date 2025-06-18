using SolidPrinciples.Models;

namespace SolidPrinciples.LiskovSubstitution;

/// <summary>
/// Liskov Substitution Principle: Electric charging station that implements the same interface
/// Can be substituted anywhere a fuel dispenser is expected, maintaining behavioral compatibility
/// </summary>
public class ElectricChargingStation : IFuelDispenser
{
    public int PumpNumber { get; }
    public FuelType SupportedFuelType => FuelType.Electric;
    public bool IsOperational { get; private set; }
    public decimal MaxDispenseRate { get; } // kWh per hour

    private decimal _totalEnergyDispensed;
    private bool _isCharging;
    private DateTime _chargingStartTime;

    public ElectricChargingStation(int pumpNumber, decimal maxDispenseRate = 150.0m)
    {
        PumpNumber = pumpNumber;
        MaxDispenseRate = maxDispenseRate;
        IsOperational = true;
        _totalEnergyDispensed = 0;
        _isCharging = false;
    }

    public bool DispenseFuel(decimal quantity)
    {
        // For electric, quantity represents kWh of energy
        if (!IsOperational || quantity <= 0 || quantity > MaxDispenseRate)
        {
            return false;
        }

        if (!_isCharging)
        {
            StartDispensing();
        }

        _totalEnergyDispensed += quantity;
        Console.WriteLine($"Charging Station {PumpNumber}: Delivered {quantity:F2} kWh of electricity");
        
        return true;
    }

    public bool StartDispensing()
    {
        if (!IsOperational || _isCharging)
        {
            return false;
        }

        _isCharging = true;
        _chargingStartTime = DateTime.Now;
        Console.WriteLine($"Charging Station {PumpNumber}: Started charging");
        return true;
    }

    public bool StopDispensing()
    {
        if (!_isCharging)
        {
            return false;
        }

        _isCharging = false;
        var chargingDuration = DateTime.Now - _chargingStartTime;
        Console.WriteLine($"Charging Station {PumpNumber}: Stopped charging after {chargingDuration.TotalMinutes:F1} minutes");
        return true;
    }

    public decimal GetTotalDispensed()
    {
        return _totalEnergyDispensed;
    }

    public void ResetCounter()
    {
        _totalEnergyDispensed = 0;
        Console.WriteLine($"Charging Station {PumpNumber}: Energy counter reset");
    }
} 