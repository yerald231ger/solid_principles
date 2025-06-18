using SolidPrinciples.Models;

namespace SolidPrinciples.SingleResponsibility;

/// <summary>
/// Single Responsibility: This class is only responsible for processing sales
/// It doesn't manage inventory or handle price updates
/// </summary>
public class SalesProcessor
{
    private readonly List<Sale> _sales;
    private int _nextSaleId;

    public SalesProcessor()
    {
        _sales = new List<Sale>();
        _nextSaleId = 1;
    }

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

        return sale;
    }

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

    public Sale? GetSaleById(int saleId)
    {
        return _sales.FirstOrDefault(s => s.Id == saleId);
    }

    public List<Sale> GetAllSales()
    {
        return new List<Sale>(_sales);
    }
} 