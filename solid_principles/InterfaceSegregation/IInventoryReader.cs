using SolidPrinciples.Models;

namespace SolidPrinciples.InterfaceSegregation;

/// <summary>
/// Interface Segregation Principle: Clients that only need to read inventory
/// don't need to know about write operations
/// </summary>
public interface IInventoryReader
{
    Fuel? GetFuel(FuelType fuelType);
    decimal GetAvailableQuantity(FuelType fuelType);
    bool IsLowStock(FuelType fuelType);
    List<Fuel> GetLowStockFuels();
    List<Fuel> GetAllFuels();
} 