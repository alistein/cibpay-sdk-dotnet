using System.Reflection;

namespace CibPayTest.Certificate;

public class CertificatePath
{
    public string CurrentPath { get; set; }
    
    public CertificatePath()
    {
        var assemblyFolder = Assembly.GetExecutingAssembly().Location;
        var certificatePath = Path.Combine(Path.GetDirectoryName(assemblyFolder), "Certificate/api-cibpay.p12");
        CurrentPath = certificatePath;
    }

}