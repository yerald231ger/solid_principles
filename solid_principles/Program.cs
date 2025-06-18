using SolidPrinciples.Models;
using SolidPrinciples.SingleResponsibility;
using SolidPrinciples.OpenClosed;
using SolidPrinciples.LiskovSubstitution;
using SolidPrinciples.InterfaceSegregation;
using SolidPrinciples.DependencyInversion;

namespace SolidPrinciples;

/// <summary>
/// Fuel Station Management System - Demonstrating SOLID Principles
/// 
/// This application showcases all five SOLID principles:
/// S - Single Responsibility Principle
/// O - Open/Closed Principle  
/// L - Liskov Substitution Principle
/// I - Interface Segregation Principle
/// D - Dependency Inversion Principle
/// </summary>
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== FUEL STATION MANAGEMENT SYSTEM ===");
        Console.WriteLine("Demonstrating SOLID Principles in Enterprise Application");
        Console.WriteLine("========================================================\n");

        try
        {
            // Demonstrate all SOLID principles
            DemonstrateSingleResponsibility();
            DemonstrateOpenClosed();
            DemonstrateLiskovSubstitution();
            DemonstrateInterfaceSegregation();
            DemonstrateDependencyInversion();

            Console.WriteLine("\n=== DEMONSTRATION COMPLETED SUCCESSFULLY ===");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error occurred: {ex.Message}");
        }

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }

    /// <summary>
    /// Single Responsibility Principle: Each class has one reason to change
    /// </summary>
    static void DemonstrateSingleResponsibility()
    {
        Console.WriteLine("1. SINGLE RESPONSIBILITY PRINCIPLE");
        Console.WriteLine("==================================");
        Console.WriteLine("Each class has a single, well-defined responsibility:\n");

        // Each class handles only one concern
        var inventoryManager = new FuelInventoryManager();
        var salesProcessor = new SalesProcessor();
        var priceManager = new PriceManager();

        // Inventory management - only handles fuel stock
        Console.WriteLine("→ FuelInventoryManager: Handles only inventory operations");
        inventoryManager.AddFuel(FuelType.Regular, 500);
        Console.WriteLine($"Regular fuel available: {inventoryManager.GetAvailableQuantity(FuelType.Regular):F2}L");

        // Sales processing - only handles sales transactions
        Console.WriteLine("\n→ SalesProcessor: Handles only sales transactions");
        var sale = salesProcessor.ProcessSale(FuelType.Regular, 25.50m, 1.25m, 
                                             PaymentMethod.CreditCard, "John Doe", 1);
        Console.WriteLine($"Sale processed: #{sale.Id}");

        // Price management - only handles price updates
        Console.WriteLine("\n→ PriceManager: Handles only price management");
        var priceUpdate = priceManager.UpdatePrice(FuelType.Premium, 1.50m, "Manager", "Market adjustment");
        Console.WriteLine($"Price update: {priceUpdate.PercentageChange:F1}% change");

        Console.WriteLine("\n");
    }

    /// <summary>
    /// Open/Closed Principle: Open for extension, closed for modification
    /// </summary>
    static void DemonstrateOpenClosed()
    {
        Console.WriteLine("2. OPEN/CLOSED PRINCIPLE");
        Console.WriteLine("========================");
        Console.WriteLine("System is open for extension but closed for modification:\n");

        var paymentFactory = new PaymentProcessorFactory();

        // Existing payment methods work without modification
        Console.WriteLine("→ Processing payments with existing methods:");
        ProcessPaymentDemo(paymentFactory, PaymentMethod.Cash, 50.00m, "Alice Smith");
        ProcessPaymentDemo(paymentFactory, PaymentMethod.CreditCard, 75.25m, "Bob Johnson");

        // New payment method can be added without modifying existing code
        Console.WriteLine("\n→ Adding new payment method without modifying existing code:");
        ProcessPaymentDemo(paymentFactory, PaymentMethod.MobilePayment, 30.00m, "Carol Davis");

        // The factory can be extended with new payment processors
        Console.WriteLine("\n→ System supports extension through registration pattern");
        Console.WriteLine($"Supported payment methods: {string.Join(", ", paymentFactory.GetSupportedPaymentMethods())}");

        Console.WriteLine("\n");
    }

    /// <summary>
    /// Liskov Substitution Principle: Subtypes must be substitutable for their base types
    /// </summary>
    static void DemonstrateLiskovSubstitution()
    {
        Console.WriteLine("3. LISKOV SUBSTITUTION PRINCIPLE");
        Console.WriteLine("================================");
        Console.WriteLine("All dispenser types can be used interchangeably:\n");

        var dispenserManager = new DispenserManager();

        // Add different types of dispensers - all implement IFuelDispenser
        Console.WriteLine("→ Adding different dispenser types:");
        dispenserManager.AddDispenser(new StandardFuelDispenser(1, FuelType.Regular));
        dispenserManager.AddDispenser(new HighVolumeDispenser(2, FuelType.Premium, 80.0m, 15.0m));
        dispenserManager.AddDispenser(new ElectricChargingStation(3, 120.0m));

        // All dispensers can be used interchangeably through the same interface
        Console.WriteLine("\n→ Processing fuel requests - all dispensers work the same way:");
        dispenserManager.ProcessFuelRequest(FuelType.Regular, 40.0m);
        dispenserManager.ProcessFuelRequest(FuelType.Premium, 20.0m);
        dispenserManager.ProcessFuelRequest(FuelType.Electric, 50.0m);

        // Generate report works with all dispenser types
        dispenserManager.GenerateReport();

        Console.WriteLine("\n");
    }

    /// <summary>
    /// Interface Segregation Principle: Clients should not depend on interfaces they don't use
    /// </summary>
    static void DemonstrateInterfaceSegregation()
    {
        Console.WriteLine("4. INTERFACE SEGREGATION PRINCIPLE");
        Console.WriteLine("==================================");
        Console.WriteLine("Clients depend only on the interfaces they need:\n");

        var inventoryService = new InventoryService();
        var salesService = new SalesService();

        // Read-only client only needs IInventoryReader
        Console.WriteLine("→ Read-only operations use only IInventoryReader:");
        DemonstrateReadOnlyOperations(inventoryService);

        // Write operations use IInventoryWriter
        Console.WriteLine("\n→ Write operations use only IInventoryWriter:");
        DemonstrateWriteOperations(inventoryService);

        // Sales processing and reporting are segregated
        Console.WriteLine("\n→ Sales processing and reporting are separate concerns:");
        DemonstrateSalesOperations(salesService, salesService);

        Console.WriteLine("\n");
    }

    /// <summary>
    /// Dependency Inversion Principle: Depend on abstractions, not concretions
    /// </summary>
    static void DemonstrateDependencyInversion()
    {
        Console.WriteLine("5. DEPENDENCY INVERSION PRINCIPLE");
        Console.WriteLine("=================================");
        Console.WriteLine("High-level modules depend on abstractions, not concrete implementations:\n");

        // Dependencies are injected - high-level code doesn't create concrete implementations
        Console.WriteLine("→ Using Email notification system:");
        var emailRepository = new InMemoryRepository();
        var emailNotificationService = new EmailNotificationService();
        var emailManager = new FuelStationManager(emailRepository, emailNotificationService);

        DemonstrateManagerOperations(emailManager, "Email");

        Console.WriteLine("\n→ Switching to SMS notification system (no code changes in manager):");
        var smsRepository = new InMemoryRepository();
        var smsNotificationService = new SmsNotificationService();
        var smsManager = new FuelStationManager(smsRepository, smsNotificationService);

        DemonstrateManagerOperations(smsManager, "SMS");

        Console.WriteLine("\n");
    }

    // Helper methods for demonstrations

    static void ProcessPaymentDemo(PaymentProcessorFactory factory, PaymentMethod method, decimal amount, string customer)
    {
        var processor = factory.CreateProcessor(method);
        processor.ProcessPayment(amount, customer);
    }

    static void DemonstrateReadOnlyOperations(IInventoryReader reader)
    {
        var fuel = reader.GetFuel(FuelType.Regular);
        Console.WriteLine($"  Regular fuel: {fuel?.AvailableQuantity:F2}L available");
        
        var lowStockFuels = reader.GetLowStockFuels();
        Console.WriteLine($"  Low stock items: {lowStockFuels.Count}");
    }

    static void DemonstrateWriteOperations(IInventoryWriter writer)
    {
        Console.WriteLine("  Adding fuel to inventory:");
        writer.AddFuel(FuelType.Diesel, 200);
        writer.UpdateFuelPrice(FuelType.Regular, 1.30m);
    }

    static void DemonstrateSalesOperations(ISalesProcessor processor, ISalesReporter reporter)
    {
        var sale = processor.ProcessSale(FuelType.Premium, 30.0m, 1.45m, 
                                        PaymentMethod.DebitCard, "Demo Customer", 2);
        
        var todaysSales = reporter.GetSalesByDate(DateTime.Today);
        Console.WriteLine($"  Today's sales count: {todaysSales.Count}");
    }

    static void DemonstrateManagerOperations(FuelStationManager manager, string systemType)
    {
        try
        {
            Console.WriteLine($"  [{systemType}] Processing purchase:");
            manager.ProcessFuelPurchase(FuelType.Regular, 45.0m, PaymentMethod.CreditCard, "Test Customer", 1);
            
            Console.WriteLine($"  [{systemType}] Updating price:");
            manager.UpdateFuelPrice(FuelType.Diesel, 1.40m, "System Admin", "Weekly adjustment");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  Error: {ex.Message}");
        }
    }
}