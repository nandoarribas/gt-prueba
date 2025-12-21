using Xunit;

namespace GtMotive.Estimate.Microservice.InfrastructureTests.Infrastructure
{
    [Collection(TestCollections.TestServer)]
    public class InfrastructureTestBase(GenericInfrastructureTestServerFixture fixture)
    {
        public GenericInfrastructureTestServerFixture Fixture { get; } = fixture;
    }
}
