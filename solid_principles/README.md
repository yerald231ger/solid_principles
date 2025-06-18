# Fuel Station Management System - SOLID Principles Implementation

A comprehensive C# 9 console application demonstrating all five SOLID principles through an enterprise-grade fuel station management system.

## Overview

This application manages fuel/gas station operations including:
- **Inventory Management**: Track fuel levels, stock monitoring, automated alerts
- **Sales Processing**: Handle customer transactions, multiple payment methods
- **Fuel Price Updates**: Dynamic pricing with history tracking and notifications

## SOLID Principles Implementation

### 🔸 Single Responsibility Principle (SRP)
**"A class should have only one reason to change"**

**Location**: `SingleResponsibility/`

Each class has a single, well-defined responsibility:

- **`FuelInventoryManager`**: Only manages fuel inventory operations
- **`SalesProcessor`**: Only handles sales transaction processing
- **`PriceManager`**: Only manages fuel pricing operations

**Benefits**:
- Easy to understand and maintain
- Reduced coupling between components
- Clear separation of concerns

### 🔸 Open/Closed Principle (OCP)
**"Software entities should be open for extension but closed for modification"**

**Location**: `OpenClosed/`

The payment processing system demonstrates OCP:

- **`IPaymentProcessor`**: Interface that defines the contract
- **Multiple Implementations**: `CashPaymentProcessor`, `CreditCardPaymentProcessor`, `MobilePaymentProcessor`
- **`PaymentProcessorFactory`**: Allows registration of new payment methods without modifying existing code

**Benefits**:
- New payment methods can be added without changing existing code
- System is extensible and maintainable
- Follows the factory pattern for easy extension

### 🔸 Liskov Substitution Principle (LSP)
**"Objects of a superclass should be replaceable with objects of a subclass without breaking the application"**

**Location**: `LiskovSubstitution/`

All fuel dispensers are interchangeable:

- **`IFuelDispenser`**: Base interface for all dispensers
- **`StandardFuelDispenser`**: Basic fuel dispenser implementation
- **`HighVolumeDispenser`**: Enhanced dispenser with additional safety checks
- **`ElectricChargingStation`**: Electric vehicle charging station
- **`DispenserManager`**: Works with any `IFuelDispenser` implementation

**Benefits**:
- Any dispenser can be substituted without breaking functionality
- Polymorphism is properly implemented
- System is flexible and extensible

### 🔸 Interface Segregation Principle (ISP)
**"Clients should not be forced to depend on interfaces they do not use"**

**Location**: `InterfaceSegregation/`

Interfaces are segregated by functionality:

#### Inventory Interfaces:
- **`IInventoryReader`**: Read-only inventory operations
- **`IInventoryWriter`**: Write-only inventory operations

#### Sales Interfaces:
- **`ISalesProcessor`**: Sales transaction processing
- **`ISalesReporter`**: Sales reporting and analytics

**Benefits**:
- Clients depend only on methods they actually use
- Interfaces are focused and cohesive
- System is more modular and testable

### 🔸 Dependency Inversion Principle (DIP)
**"High-level modules should not depend on low-level modules. Both should depend on abstractions"**

**Location**: `DependencyInversion/`

High-level components depend on abstractions:

#### Abstractions:
- **`INotificationService`**: Notification contract
- **`IDataRepository`**: Data persistence contract

#### Implementations:
- **Notifications**: `EmailNotificationService`, `SmsNotificationService`
- **Data Storage**: `InMemoryRepository`

#### High-Level Module:
- **`FuelStationManager`**: Depends on abstractions, not concrete implementations

**Benefits**:
- Easy to swap implementations (email ↔ SMS notifications)
- Highly testable (can mock dependencies)
- Loose coupling between components

## Project Structure

```
solid_principles/
├── Models/                          # Domain models
│   ├── FuelType.cs                 # Fuel type enumeration
│   ├── PaymentMethod.cs            # Payment method enumeration
│   ├── Fuel.cs                     # Fuel entity
│   ├── Sale.cs                     # Sale transaction entity
│   └── PriceUpdate.cs              # Price update entity
├── SingleResponsibility/            # SRP implementations
│   ├── FuelInventoryManager.cs     # Inventory management only
│   ├── SalesProcessor.cs           # Sales processing only
│   └── PriceManager.cs             # Price management only
├── OpenClosed/                      # OCP implementations
│   ├── IPaymentProcessor.cs        # Payment processor interface
│   ├── CashPaymentProcessor.cs     # Cash payment implementation
│   ├── CreditCardPaymentProcessor.cs # Credit card implementation
│   ├── MobilePaymentProcessor.cs   # Mobile payment implementation
│   └── PaymentProcessorFactory.cs  # Factory for extensions
├── LiskovSubstitution/              # LSP implementations
│   ├── IFuelDispenser.cs           # Base dispenser interface
│   ├── StandardFuelDispenser.cs    # Standard implementation
│   ├── HighVolumeDispenser.cs      # Enhanced implementation
│   ├── ElectricChargingStation.cs  # Electric charging implementation
│   └── DispenserManager.cs         # Works with any dispenser
├── InterfaceSegregation/            # ISP implementations
│   ├── IInventoryReader.cs         # Read-only inventory interface
│   ├── IInventoryWriter.cs         # Write-only inventory interface
│   ├── ISalesProcessor.cs          # Sales processing interface
│   ├── ISalesReporter.cs           # Sales reporting interface
│   ├── InventoryService.cs         # Implements both read/write
│   └── SalesService.cs             # Implements processing/reporting
├── DependencyInversion/             # DIP implementations
│   ├── INotificationService.cs     # Notification abstraction
│   ├── IDataRepository.cs          # Data persistence abstraction
│   ├── EmailNotificationService.cs # Email implementation
│   ├── SmsNotificationService.cs   # SMS implementation
│   ├── InMemoryRepository.cs       # In-memory storage implementation
│   └── FuelStationManager.cs       # High-level coordinator
└── Program.cs                       # Main application demonstrating all principles
```

## Technical Requirements

- **Framework**: .NET 9.0
- **Language**: C# 9 with modern features
- **Application Type**: Console Application
- **Architecture**: Modular, enterprise-ready design

## Key Features

### Enterprise-Grade Functionality
- ✅ Multi-fuel type support (Regular, Premium, Diesel, Electric)
- ✅ Multiple payment methods (Cash, Credit Card, Mobile Payment, etc.)
- ✅ Real-time inventory tracking with low stock alerts
- ✅ Comprehensive sales reporting and analytics
- ✅ Price history tracking with audit trail
- ✅ Automated notifications (Email/SMS)
- ✅ Configurable business rules

### Design Benefits
- 🔧 **Maintainable**: Clear separation of concerns
- 🔄 **Extensible**: Easy to add new features without breaking existing code
- 🧪 **Testable**: Dependencies can be easily mocked
- 📈 **Scalable**: Modular architecture supports growth
- 🔒 **Robust**: Proper error handling and validation

## Running the Application

1. **Prerequisites**: .NET 9.0 SDK installed
2. **Build**: `dotnet build`
3. **Run**: `dotnet run`

The application will demonstrate each SOLID principle with practical examples and detailed console output.

## Learning Outcomes

After studying this implementation, you will understand:

1. **How to apply SOLID principles** in real-world scenarios
2. **Enterprise application architecture** patterns
3. **Dependency injection** and inversion of control
4. **Interface design** and segregation strategies
5. **Extensible system design** patterns
6. **Modern C# development** practices

## Best Practices Demonstrated

- **Clean Code**: Descriptive names, clear intent
- **Documentation**: Comprehensive XML comments
- **Error Handling**: Proper exception management
- **Validation**: Input validation and business rules
- **Separation of Concerns**: Logical component boundaries
- **Dependency Management**: Proper abstraction layers

---

*This implementation serves as a practical guide for applying SOLID principles in enterprise C# applications, demonstrating how these principles lead to maintainable, scalable, and robust software solutions.* 