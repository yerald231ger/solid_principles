using SolidPrinciples.Models;

namespace SolidPrinciples.DependencyInversion;

/// <summary>
/// Dependency Inversion Principle: High-level modules depend on this abstraction,
/// not on concrete database implementations
/// </summary>
public interface IDataRepository
{
    // Sales operations
    bool SaveSale(Sale sale);
    Sale? GetSaleById(int saleId);
    List<Sale> GetSalesByDate(DateTime date);
    
    // Fuel operations
    bool SaveFuel(Fuel fuel);
    Fuel? GetFuelByType(FuelType fuelType);
    List<Fuel> GetAllFuels();
    
    // Price operations
    bool SavePriceUpdate(PriceUpdate priceUpdate);
    List<PriceUpdate> GetPriceHistory(FuelType fuelType);
} 