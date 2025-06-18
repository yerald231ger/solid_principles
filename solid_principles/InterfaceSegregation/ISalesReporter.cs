using SolidPrinciples.Models;

namespace SolidPrinciples.InterfaceSegregation;

/// <summary>
/// Interface Segregation Principle: Clients that only need sales reporting
/// don't need to know about sales processing operations
/// </summary>
public interface ISalesReporter
{
    List<Sale> GetSalesByDate(DateTime date);
    List<Sale> GetSalesByFuelType(FuelType fuelType);
    List<Sale> GetSalesByPaymentMethod(PaymentMethod paymentMethod);
    decimal GetTotalSalesAmount(DateTime startDate, DateTime endDate);
    decimal GetTotalFuelSold(FuelType fuelType, DateTime startDate, DateTime endDate);
    Dictionary<FuelType, decimal> GetSalesSummaryByFuelType(DateTime date);
} 