using SwiftLocator.Services.ServiceLocatorServices;

public class ServiceBuildTester : IPreliminarySetup
{
    public int Order => IPreliminarySetup.TEST;

    public void Setup()
    {
        // Ensure all instances run correctly.
        ServiceLocator.Build();
    }
}
