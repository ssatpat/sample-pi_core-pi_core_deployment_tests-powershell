using System;
using System.Net;

namespace OSIsoft.PISystemDeploymentTests
{
    /// <summary>
    /// THe EdgeDataStoreFixture is a partial test context to be shared in the
    /// Edge Data Store relate xUnit test classes
    /// </summary>
    public sealed class EdgeDataStoreFixture : IDisposable
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public EdgeDataStoreFixture()
        {
            Client.Headers.Add(HttpRequestHeader.ContentType, "application/json; charset=utf-8");
            Client.BaseAddress = $"http://localhost:{Settings.EdgeDataStorePort}/api/v1/";
        }

        /// <summary>
        /// The WebClient used for Rest enpoint calls
        /// </summary>
        public WebClient Client { get; private set; } = new WebClient();

        /// <summary>
        /// clean up
        /// </summary>
        public void Dispose()
        {
        }
    }
}
