using SolidPrinciples.Models;

namespace SolidPrinciples.DependencyInversion;

/// <summary>
/// Dependency Inversion Principle: Concrete implementation of data storage
/// High-level modules don't depend directly on this, but on the IDataRepository abstraction
/// </summary>
public class InMemoryRepository : IDataRepository
{
    private readonly List<Sale> _sales;
    private readonly Dictionary<FuelType, Fuel> _fuels;
    private readonly List<PriceUpdate> _priceUpdates;

    public InMemoryRepository()
    {
        _sales = new List<Sale>();
        _fuels = new Dictionary<FuelType, Fuel>();
        _priceUpdates = new List<PriceUpdate>();
        InitializeDefaultData();
    }

    // Sales operations
    public bool SaveSale(Sale sale)
    {
        var existing = _sales.FirstOrDefault(s => s.Id == sale.Id);
        if (existing != null)
        {
            _sales.Remove(existing);
        }
        
        _sales.Add(sale);
        Console.WriteLine($"[REPOSITORY] Saved sale #{sale.Id} to in-memory storage");
        return true;
    }

    public Sale? GetSaleById(int saleId)
    {
        return _sales.FirstOrDefault(s => s.Id == saleId);
    }

    public List<Sale> GetSalesByDate(DateTime date)
    {
        return _sales.Where(s => s.Timestamp.Date == date.Date).ToList();
    }

    // Fuel operations
    public bool SaveFuel(Fuel fuel)
    {
        _fuels[fuel.Type] = fuel;
        Console.WriteLine($"[REPOSITORY] Saved fuel {fuel.Type} to in-memory storage");
        return true;
    }

    public Fuel? GetFuelByType(FuelType fuelType)
    {
        return _fuels.ContainsKey(fuelType) ? _fuels[fuelType] : null;
    }

    public List<Fuel> GetAllFuels()
    {
        return _fuels.Values.ToList();
    }

    // Price operations
    public bool SavePriceUpdate(PriceUpdate priceUpdate)
    {
        _priceUpdates.Add(priceUpdate);
        Console.WriteLine($"[REPOSITORY] Saved price update #{priceUpdate.Id} to in-memory storage");
        return true;
    }

    public List<PriceUpdate> GetPriceHistory(FuelType fuelType)
    {
        return _priceUpdates
            .Where(p => p.FuelType == fuelType)
            .OrderByDescending(p => p.UpdatedAt)
            .ToList();
    }

    private void InitializeDefaultData()
    {
        _fuels[FuelType.Regular] = new Fuel
        {
            Id = 1,
            Type = FuelType.Regular,
            Name = "Regular Gasoline",
            AvailableQuantity = 5000,
            MinimumStockLevel = 500,
            PricePerLiter = 1.25m,
            LastUpdated = DateTime.Now
        };

        _fuels[FuelType.Premium] = new Fuel
        {
            Id = 2,
            Type = FuelType.Premium,
            Name = "Premium Gasoline",
            AvailableQuantity = 3000,
            MinimumStockLevel = 300,
            PricePerLiter = 1.45m,
            LastUpdated = DateTime.Now
        };

        _fuels[FuelType.Diesel] = new Fuel
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