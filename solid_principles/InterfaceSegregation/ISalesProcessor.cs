using SolidPrinciples.Models;

namespace SolidPrinciples.InterfaceSegregation;

/// <summary>
/// Interface Segregation Principle: Clients that process sales don't need
/// to implement reporting capabilities
/// </summary>
public interface ISalesProcessor
{
    Sale ProcessSale(FuelType fuelType, decimal quantity, decimal pricePerLiter,
                    PaymentMethod paymentMethod, string customerName, int pumpNumber);
    bool CancelSale(int saleId);
    bool RefundSale(int saleId);
} 