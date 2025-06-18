using SolidPrinciples.Models;

namespace SolidPrinciples.SingleResponsibility;

/// <summary>
/// Single Responsibility: This class is only responsible for managing fuel prices
/// It doesn't handle inventory or sales processing
/// </summary>
public class PriceManager
{
    private readonly List<PriceUpdate> _priceHistory;
    private int _nextUpdateId;

    public PriceManager()
    {
        _priceHistory = new List<PriceUpdate>();
        _nextUpdateId = 1;
    }

    public PriceUpdate UpdatePrice(FuelType fuelType, decimal newPrice, string updatedBy, string reason)
    {
        var priceUpdate = new PriceUpdate
        {
            Id = _nextUpdateId++,
            FuelType = fuelType,
            NewPrice = newPrice,
            UpdatedAt = DateTime.Now,
            UpdatedBy = updatedBy,
            Reason = reason
        };

        // Set old price from previous update or default
        var lastUpdate = GetLatestPriceUpdate(fuelType);
        priceUpdate.OldPrice = lastUpdate?.NewPrice ?? 0;

        _priceHistory.Add(priceUpdate);
        return priceUpdate;
    }

    public decimal GetCurrentPrice(FuelType fuelType)
    {
        var latestUpdate = GetLatestPriceUpdate(fuelType);
        return latestUpdate?.NewPrice ?? 0;
    }

    public PriceUpdate? GetLatestPriceUpdate(FuelType fuelType)
    {
        return _priceHistory
            .Where(p => p.FuelType == fuelType)
            .OrderByDescending(p => p.UpdatedAt)
            .FirstOrDefault();
    }

    public List<PriceUpdate> GetPriceHistory(FuelType fuelType)
    {
        return _priceHistory
            .Where(p => p.FuelType == fuelType)
            .OrderByDescending(p => p.UpdatedAt)
            .ToList();
    }

    public List<PriceUpdate> GetPriceHistoryByDate(DateTime date)
    {
        return _priceHistory
            .Where(p => p.UpdatedAt.Date == date.Date)
            .OrderByDescending(p => p.UpdatedAt)
            .ToList();
    }
} 