namespace SolidPrinciples.Models;

public class Fuel
{
    public int Id { get; set; }
    public FuelType Type { get; set; }
    public decimal PricePerLiter { get; set; }
    public decimal AvailableQuantity { get; set; }
    public DateTime LastUpdated { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal MinimumStockLevel { get; set; }

    public bool IsLowStock => AvailableQuantity <= MinimumStockLevel;
} 