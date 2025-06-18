using SolidPrinciples.Models;

namespace SolidPrinciples.DependencyInversion;

/// <summary>
/// Dependency Inversion Principle: High-level module that depends on abstractions (interfaces)
/// rather than concrete implementations. This makes the system flexible and testable.
/// </summary>
public class FuelStationManager
{
    private readonly IDataRepository _repository;
    private readonly INotificationService _notificationService;
    private readonly string _managerEmail;

    public FuelStationManager(IDataRepository repository, INotificationService notificationService, 
                             string managerEmail = "manager@fuelstation.com")
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));
        _managerEmail = managerEmail;
    }

    public Sale ProcessFuelPurchase(FuelType fuelType, decimal quantity, PaymentMethod paymentMethod, 
                                   string customerName, int pumpNumber)
    {
        Console.WriteLine($"\n=== Processing Fuel Purchase ===");
        
        // Get current fuel information
        var fuel = _repository.GetFuelByType(fuelType);
        if (fuel == null)
        {
            throw new InvalidOperationException($"Fuel type {fuelType} not found");
        }

        // Check availability
        if (fuel.AvailableQuantity < quantity)
        {
            throw new InvalidOperationException($"Insufficient fuel. Available: {fuel.AvailableQuantity:F2}L");
        }

        // Create and process sale
        var sale = new Sale
        {
            Id = GetNextSaleId(),
            FuelType = fuelType,
            Quantity = quantity,
            PricePerLiter = fuel.PricePerLiter,
            PaymentMethod = paymentMethod,
            CustomerName = customerName,
            PumpNumber = pumpNumber
        };

        sale.CalculateTotal();

        // Update inventory
        fuel.AvailableQuantity -= quantity;
        fuel.LastUpdated = DateTime.Now;

        // Save changes
        _repository.SaveSale(sale);
        _repository.SaveFuel(fuel);

        // Check for low stock and notify if needed
        if (fuel.IsLowStock)
        {
            NotifyLowStock(fuel);
        }

        Console.WriteLine($"Sale processed successfully: {quantity:F2}L of {fuelType} for ${sale.TotalAmount:F2}");
        return sale;
    }

    public void UpdateFuelPrice(FuelType fuelType, decimal newPrice, string updatedBy, string reason)
    {
        Console.WriteLine($"\n=== Updating Fuel Price ===");
        
        var fuel = _repository.GetFuelByType(fuelType);
        if (fuel == null)
        {
            throw new InvalidOperationException($"Fuel type {fuelType} not found");
        }

        var priceUpdate = new PriceUpdate
        {
            Id = GetNextPriceUpdateId(),
            FuelType = fuelType,
            OldPrice = fuel.PricePerLiter,
            NewPrice = newPrice,
            UpdatedAt = DateTime.Now,
            UpdatedBy = updatedBy,
            Reason = reason
        };

        fuel.PricePerLiter = newPrice;
        fuel.LastUpdated = DateTime.Now;

        _repository.SaveFuel(fuel);
        _repository.SavePriceUpdate(priceUpdate);

        // Notify about price change
        NotifyPriceChange(priceUpdate);

        Console.WriteLine($"Price updated: {fuelType} from ${priceUpdate.OldPrice:F2} to ${newPrice:F2}");
    }

    public void CheckInventoryStatus()
    {
        Console.WriteLine($"\n=== Checking Inventory Status ===");
        
        var allFuels = _repository.GetAllFuels();
        var lowStockFuels = allFuels.Where(f => f.IsLowStock).ToList();

        if (lowStockFuels.Any())
        {
            Console.WriteLine($"Found {lowStockFuels.Count} fuel(s) with low stock:");
            foreach (var fuel in lowStockFuels)
            {
                Console.WriteLine($"- {fuel.Name}: {fuel.AvailableQuantity:F2}L (Min: {fuel.MinimumStockLevel:F2}L)");
                NotifyLowStock(fuel);
            }
        }
        else
        {
            Console.WriteLine("All fuel levels are adequate.");
        }
    }

    private void NotifyLowStock(Fuel fuel)
    {
        var subject = "Low Fuel Stock Alert";
        var message = $"Fuel {fuel.Name} is running low. " +
                     $"Current: {fuel.AvailableQuantity:F2}L, " +
                     $"Minimum: {fuel.MinimumStockLevel:F2}L. " +
                     $"Please arrange for refill.";

        _notificationService.SendUrgentNotification(_managerEmail, subject, message);
    }

    private void NotifyPriceChange(PriceUpdate priceUpdate)
    {
        var subject = "Fuel Price Update";
        var message = $"Price for {priceUpdate.FuelType} has been updated from " +
                     $"${priceUpdate.OldPrice:F2} to ${priceUpdate.NewPrice:F2} per liter. " +
                     $"Reason: {priceUpdate.Reason}. " +
                     $"Updated by: {priceUpdate.UpdatedBy}";

        _notificationService.SendNotification(_managerEmail, subject, message);
    }

    private int GetNextSaleId()
    {
        // Simplified ID generation for demo
        return DateTime.Now.Millisecond + new Random().Next(1000, 9999);
    }

    private int GetNextPriceUpdateId()
    {
        // Simplified ID generation for demo
        return DateTime.Now.Millisecond + new Random().Next(1000, 9999);
    }
} 