using SolidPrinciples.Models;

namespace SolidPrinciples.InterfaceSegregation;

/// <summary>
/// Interface Segregation Principle: Clients that need to modify inventory
/// can use this interface without being forced to implement read operations
/// </summary>
public interface IInventoryWriter
{
    void AddFuel(FuelType fuelType, decimal quantity);
    bool RemoveFuel(FuelType fuelType, decimal quantity);
    bool UpdateFuelPrice(FuelType fuelType, decimal newPrice);
    void SetMinimumStockLevel(FuelType fuelType, decimal minimumLevel);
} 