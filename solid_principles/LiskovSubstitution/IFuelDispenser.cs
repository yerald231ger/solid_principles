using SolidPrinciples.Models;

namespace SolidPrinciples.LiskovSubstitution;

/// <summary>
/// Liskov Substitution Principle: Base interface that all fuel dispensers must implement
/// Any implementation should be substitutable without changing the correctness of the program
/// </summary>
public interface IFuelDispenser
{
    int PumpNumber { get; }
    FuelType SupportedFuelType { get; }
    bool IsOperational { get; }
    decimal MaxDispenseRate { get; }
    
    bool DispenseFuel(decimal quantity);
    bool StartDispensing();
    bool StopDispensing();
    decimal GetTotalDispensed();
    void ResetCounter();
} 