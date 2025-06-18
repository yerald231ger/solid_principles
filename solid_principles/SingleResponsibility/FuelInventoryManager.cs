using SolidPrinciples.Models;

namespace SolidPrinciples.SingleResponsibility;

/// <summary>
/// Single Responsibility: This class is only responsible for managing fuel inventory
/// It doesn't handle sales, pricing, or reporting - each has its own dedicated class
/// </summary>
public class FuelInventoryManager
{
    private readonly Dictionary<FuelType, Fuel> _inventory;

    public FuelInventoryManager()
    {
        _inventory = new Dictionary<FuelType, Fuel>();
        InitializeInventory();
    }

    public void AddFuel(FuelType fuelType, decimal quantity)
    {
        if (_inventory.ContainsKey(fuelType))
        {
            _inventory[fuelType].AvailableQuantity += quantity;
            _inventory[fuelType].LastUpdated = DateTime.Now;
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
        return true;
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

    public Fuel? GetFuel(FuelType fuelType)
    {
        return _inventory.ContainsKey(fuelType) ? _inventory[fuelType] : null;
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