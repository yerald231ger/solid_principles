namespace SolidPrinciples.Models;

public class Sale
{
    public int Id { get; set; }
    public DateTime Timestamp { get; set; }
    public FuelType FuelType { get; set; }
    public decimal Quantity { get; set; }
    public decimal PricePerLiter { get; set; }
    public decimal TotalAmount { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public int PumpNumber { get; set; }

    public Sale()
    {
        Timestamp = DateTime.Now;
    }

    public void CalculateTotal()
    {
        TotalAmount = Quantity * PricePerLiter;
    }
} 