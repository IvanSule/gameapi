namespace RPSSL.WebApi.E2ETests
{    
    public class BaseE2ETest : IClassFixture<E2ETestWebAppFactory>
    {
        protected HttpClient HttpClient { get; init; }

        public BaseE2ETest(E2ETestWebAppFactory factory) 
        {
            HttpClient = factory.CreateClient();          
        }
    }
}
