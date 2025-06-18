using SolidPrinciples.Models;

namespace SolidPrinciples.InterfaceSegregation;

/// <summary>
/// Interface Segregation Principle: This service implements both reader and writer interfaces
/// Clients can depend only on the interface they need (read-only or write-only)
/// </summary>
public class InventoryService : IInventoryReader, IInventoryWriter
{
    private readonly Dictionary<FuelType, Fuel> _inventory;

    public InventoryService()
    {
        _inventory = new Dictionary<FuelType, Fuel>();
        InitializeInventory();
    }

    // IInventoryReader implementation
    public Fuel? GetFuel(FuelType fuelType)
    {
        return _inventory.ContainsKey(fuelType) ? _inventory[fuelType] : null;
    }

    public decimal GetAvailableQuantity(FuelType fuelType)
    {
        return _inventory.ContainsKey(fuelType) ? _inventory[fuelType].AvailableQuantity : 0;
    }

    public bool IsLowStock(FuelType fuelType)
    {
        return _inventory.ContainsKey(fuelType) && _inventory[fuelType].IsLowStock;
    }

    public List<Fuel> GetLowStockFuels()
    {
        return _inventory.Values.Where(f => f.IsLowStock).ToList();
    }

    public List<Fuel> GetAllFuels()
    {
        return _inventory.Values.ToList();
    }

    // IInventoryWriter implementation
    public void AddFuel(FuelType fuelType, decimal quantity)
    {
        if (_inventory.ContainsKey(fuelType))
        {
            _inventory[fuelType].AvailableQuantity += quantity;
            _inventory[fuelType].LastUpdated = DateTime.Now;
            Console.WriteLine($"Added {quantity:F2} liters of {fuelType}. New total: {_inventory[fuelType].AvailableQuantity:F2}");
        }
    }

    public bool RemoveFuel(FuelType fuelType, decimal quantity)
    {
        if (!_inventory.ContainsKey(fuelType) || _inventory[fuelType].AvailableQuantity < quantity)
        {
            return false;
        }

        _inventory[fuelType].AvailableQuantity -= quantity;
        _inventory[fuelType].LastUpdated = DateTime.Now;
        Console.WriteLine($"Removed {quantity:F2} liters of {fuelType}. Remaining: {_inventory[fuelType].AvailableQuantity:F2}");
        return true;
    }

    public bool UpdateFuelPrice(FuelType fuelType, decimal newPrice)
    {
        if (!_inventory.ContainsKey(fuelType))
        {
            return false;
        }

        var oldPrice = _inventory[fuelType].PricePerLiter;
        _inventory[fuelType].PricePerLiter = newPrice;
        _inventory[fuelType].LastUpdated = DateTime.Now;
        Console.WriteLine($"Updated {fuelType} price from ${oldPrice:F2} to ${newPrice:F2} per liter");
        return true;
    }

    public void SetMinimumStockLevel(FuelType fuelType, decimal minimumLevel)
    {
        if (_inventory.ContainsKey(fuelType))
        {
            _inventory[fuelType].MinimumStockLevel = minimumLevel;
            Console.WriteLine($"Set minimum stock level for {fuelType} to {minimumLevel:F2} liters");
        }
    }

    private void InitializeInventory()
    {
        _inventory[FuelType.Regular] = new Fuel
        {
            Id = 1,
            Type = FuelType.Regular,
            Name = "Regular Gasoline",
            AvailableQuantity = 5000,
            MinimumStockLevel = 500,
            PricePerLiter = 1.25m,
            LastUpdated = DateTime.Now
        };

        _inventory[FuelType.Premium] = new Fuel
        {
            Id = 2,
            Type = FuelType.Premium,
            Name = "Premium Gasoline",
            AvailableQuantity = 3000,
            MinimumStockLevel = 300,
            PricePerLiter = 1.45m,
            LastUpdated = DateTime.Now
        };

        _inventory[FuelType.Diesel] = new Fuel
        {
            Id = 3,
            Type = FuelType.Diesel,
            Name = "Diesel",
            AvailableQuantity = 4000,
            MinimumStockLevel = 400,
            PricePerLiter = 1.35m,
            LastUpdated = DateTime.Now
        };
    }
} 