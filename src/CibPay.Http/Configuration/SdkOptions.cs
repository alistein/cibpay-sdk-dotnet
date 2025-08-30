using System.Text;

namespace CibPay.Http.Configuration;

/// <summary>
/// Represents configuration options for SDK authentication and payment processing.
/// This class encapsulates required and optional parameters for connecting to a service,
/// including credentials, URLs, certificate details, and payment settings.
/// </summary>
///
/// /// <example>
/// The following example demonstrates how to create an instance of <see cref="SdkOptions"/>
/// with required and optional parameters:
/// <code>
/// var options = new SdkOptions
/// {
///     Username = "user123",
///     Password = "pass123",
///     BaseUrl = "https://api.example.com",
///     CertificatePath = "/path/to/certificate.p12",
///     CertificatePassword = "certPass",
///     ReturnUrl = "https://example.com/return",
///     PaymentUrl = "https://example.com/payment",
///     AutoCharge = true,
///     Force3D = true,
///     Currency = "AZN",
///     ExpirationTimeout = "1m",
///     Language = "AZ"
/// };
///
/// </code>
/// </example>
public class SdkOptions
{
    /// <summary>
    /// Gets or sets the username for authentication. This is a required field.
    /// </summary>
    public required string Username { get; set; }

    /// <summary>
    /// Gets or sets the password for authentication. This is a required field.
    /// </summary>
    public required string Password { get; set; }

    /// <summary>
    /// Gets or sets the base URL of the service. This is a required field.
    /// </summary>
    public required string BaseUrl { get; set; }

    /// <summary>
    /// Gets or sets the file path to the certificate used for secure communication.
    /// This is a required field.
    /// </summary>
    public required string CertificatePath { get; set; }

    /// <summary>
    /// Gets or sets the password for the certificate. This is a required field.
    /// </summary>
    public required string CertificatePassword { get; set; }

    /// <summary>
    /// Gets or sets the optional return URL where the user is redirected after processing.
    /// </summary>
    public string? ReturnUrl { get; set; }

    /// <summary>
    /// Gets or sets the optional payment URL for payment processing.
    /// </summary>
    public string? PaymentUrl { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether automatic charging is enabled.
    /// If null, the default service behavior is used.
    /// </summary>
    public bool? AutoCharge { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether 3D Secure authentication is enforced.
    /// If null, the default service behavior is used.
    /// </summary>
    public bool? Force3D { get; set; }

    /// <summary>
    /// Gets or sets the currency code for transactions (e.g., "AZN", "EUR").
    /// If null, the default currency is used.
    /// </summary>
    public string? Currency { get; set; }

    /// <summary>
    /// Gets or sets the expiration timeout for transactions, in a format specified by the service.
    /// If null, the default timeout is used.
    /// </summary>
    public string? ExpirationTimeout { get; set; }

    /// <summary>
    /// Gets or sets the language code for the service (e.g., "az", "ru").
    /// If null, the default language is used.
    /// </summary>
    public string? Language { get; set; }

    /// <summary>
    /// Gets the Base64-encoded credentials string combining Username and Password.
    /// The format is "Username:Password" encoded in ASCII.
    /// </summary>
    public string Credentials =>
        Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{Password}"));
}
