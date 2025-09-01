# Changelog

All notable changes to the CibPay .NET SDK will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/), and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [v1.0.0-preview.2] - September 1, 2025

### Fixed
- **Package Resolution**: Fixed `NU1101: Unable to find package CibPay.Http` error that prevented package installation
- **Packaging**: `CibPay.Http` solution is now properly compiled and packed with `CibPay.Sdk` as a single unified package
- **Installation**: Package can now be installed successfully using `dotnet add package CibPay.Sdk`

### Changed
- **Architecture**: Consolidated `CibPay.Http` into the main `CibPay.Sdk` package to eliminate dependency resolution issues
- **Distribution**: Simplified package distribution to single NuGet package for easier consumption

## [v1.0.0-preview.1] - September 1, 2025

### Added
- **Initial Release**: First preview release of the CibPay .NET SDK
- **Core Features**: 
  - 🔒 **Secure Authentication**: Certificate-based authentication with basic auth support for CibPay API integration
  - 💳 **Order Management**: Complete order lifecycle management with create and retrieve operations
  - 🏗️ **Easy Integration**: Simple client setup with factory pattern and dependency injection support
  - ⚡ **Async/Await Support**: Modern async programming patterns throughout the SDK
  - 🛡️ **Type Safety**: Strongly-typed models and comprehensive error handling
  - 📱 **Multi-Platform**: Compatible with console apps, ASP.NET Core, and other .NET platforms

- **Payment Operations**:
  - Create payment orders with comprehensive request options
  - Retrieve order details with expansion support for card information
  - Support for custom fields, client information, and merchant order IDs
  - Configurable payment options including auto-charge, 3D Secure, and recurring payments

- **Configuration**:
  - Flexible SDK configuration through `SdkOptions`
  - Support for multiple environments (production, preprod)
  - Certificate-based security with P12 certificate support
  - Configurable return URLs and timeout settings

- **Models & Types**:
  - `CreateOrderRequest` with comprehensive payment details
  - `OrderResponse` with detailed order information
  - Order status tracking and conversion utilities
  - Order expansion types for detailed data retrieval

- **Error Handling**:
  - Custom `ApiException` for API-specific errors
  - Comprehensive error response models
  - Validation for required configuration parameters

- **Developer Experience**:
  - Simple factory pattern for client creation (`CibPayClientFactory.Create()`)
  - Intuitive fluent API design
  - Comprehensive XML documentation
  - Sample applications for console and ASP.NET Core

### Technical Details
- **Target Framework**: .NET 6.0+
- **Dependencies**: Minimal external dependencies for optimal performance
- **Architecture**: Clean separation of concerns with Core, Http, and SDK layers
- **Authentication**: X.509 certificate authentication with HTTP basic auth
- **Serialization**: System.Text.Json for optimal performance and compatibility

### Note
> **⚠️ PREVIEW VERSION**  
> This SDK is currently in preview. Features and APIs may change in future releases. Use in production environments at your own discretion.
