namespace SolidPrinciples.Models;

public class PriceUpdate
{
    public int Id { get; set; }
    public FuelType FuelType { get; set; }
    public decimal OldPrice { get; set; }
    public decimal NewPrice { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string UpdatedBy { get; set; } = string.Empty;
    public string Reason { get; set; } = string.Empty;

    public decimal PriceChange => NewPrice - OldPrice;
    public decimal PercentageChange => OldPrice > 0 ? (PriceChange / OldPrice) * 100 : 0;
} 