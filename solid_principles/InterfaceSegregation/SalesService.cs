using SolidPrinciples.Models;

namespace SolidPrinciples.InterfaceSegregation;

/// <summary>
/// Interface Segregation Principle: This service implements both processor and reporter interfaces
/// Different clients can depend on only the interface they need
/// </summary>
public class SalesService : ISalesProcessor, ISalesReporter
{
    private readonly List<Sale> _sales;
    private int _nextSaleId;

    public SalesService()
    {
        _sales = new List<Sale>();
        _nextSaleId = 1;
    }

    // ISalesProcessor implementation
    public Sale ProcessSale(FuelType fuelType, decimal quantity, decimal pricePerLiter,
                           PaymentMethod paymentMethod, string customerName, int pumpNumber)
    {
        var sale = new Sale
        {
            Id = _nextSaleId++,
            FuelType = fuelType,
            Quantity = quantity,
            PricePerLiter = pricePerLiter,
            PaymentMethod = paymentMethod,
            CustomerName = customerName,
            PumpNumber = pumpNumber
        };

        sale.CalculateTotal();
        _sales.Add(sale);

        Console.WriteLine($"Processed sale #{sale.Id}: {quantity:F2}L of {fuelType} for ${sale.TotalAmount:F2}");
        return sale;
    }

    public bool CancelSale(int saleId)
    {
        var sale = _sales.FirstOrDefault(s => s.Id == saleId);
        if (sale != null)
        {
            _sales.Remove(sale);
            Console.WriteLine($"Cancelled sale #{saleId}");
            return true;
        }
        return false;
    }

    public bool RefundSale(int saleId)
    {
        var sale = _sales.FirstOrDefault(s => s.Id == saleId);
        if (sale != null)
        {
            Console.WriteLine($"Refunded sale #{saleId} - Amount: ${sale.TotalAmount:F2}");
            return true;
        }
        return false;
    }

    // ISalesReporter implementation
    public List<Sale> GetSalesByDate(DateTime date)
    {
        return _sales.Where(s => s.Timestamp.Date == date.Date).ToList();
    }

    public List<Sale> GetSalesByFuelType(FuelType fuelType)
    {
        return _sales.Where(s => s.FuelType == fuelType).ToList();
    }

    public List<Sale> GetSalesByPaymentMethod(PaymentMethod paymentMethod)
    {
        return _sales.Where(s => s.PaymentMethod == paymentMethod).ToList();
    }

    public decimal GetTotalSalesAmount(DateTime startDate, DateTime endDate)
    {
        return _sales
            .Where(s => s.Timestamp.Date >= startDate.Date && s.Timestamp.Date <= endDate.Date)
            .Sum(s => s.TotalAmount);
    }

    public decimal GetTotalFuelSold(FuelType fuelType, DateTime startDate, DateTime endDate)
    {
        return _sales
            .Where(s => s.FuelType == fuelType && 
                       s.Timestamp.Date >= startDate.Date && 
                       s.Timestamp.Date <= endDate.Date)
            .Sum(s => s.Quantity);
    }

    public Dictionary<FuelType, decimal> GetSalesSummaryByFuelType(DateTime date)
    {
        return _sales
            .Where(s => s.Timestamp.Date == date.Date)
            .GroupBy(s => s.FuelType)
            .ToDictionary(g => g.Key, g => g.Sum(s => s.TotalAmount));
    }
} 