namespace BloodPressure.IntegrationTests
{
    [CollectionDefinition(Name)]
    public class IntegrationTestsCollection : ICollectionFixture<TestHost>
    {
        public const string Name = nameof(IntegrationTestsCollection);
    }
}
