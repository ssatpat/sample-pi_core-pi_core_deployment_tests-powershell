using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace OSIsoft.PISystemDeploymentTests
{
    /// <summary>
    /// asdasd
    /// </summary>
    public class EdgeDataStoreTests : IClassFixture<EdgeDataStoreFixture>
    {
        internal const string KeySetting = "EdgeDataStoreTests";
        internal const TypeCode KeySettingTypeCode = TypeCode.Boolean;
        internal const string PortSetting = "EdgeDataStorePort";

        /// <summary>
        /// Constructor for PIWebAPITests class.
        /// </summary>
        /// <param name="output">The output logger used for writing messages.</param>
        /// <param name="fixture">Fixture to manage PI Web API connection and specific helper functions.</param>
        public EdgeDataStoreTests(ITestOutputHelper output, EdgeDataStoreFixture fixture)
        {
            Output = output;
            Fixture = fixture;
        }

        private EdgeDataStoreFixture Fixture { get; }
        private ITestOutputHelper Output { get; }

        /// <summary>
        /// Verifies that EDS config can be retrieved
        /// </summary>
        [OptionalFact(KeySetting, KeySettingTypeCode)]
        public void ConfigurationTest()
        {
            string url = "configuration";
            Output.WriteLine($"Verify {Fixture.Client.BaseAddress}{url}");
            string content = Fixture.Client.DownloadString(url);
            Assert.True(!string.IsNullOrEmpty(content), "Failed to get local EDS config");
        }

        /// <summary>
        /// Verify passing things to test
        /// </summary>
        /// <param name="path"> endpoint to verify</param>
        [OptionalTheory(KeySetting, KeySettingTypeCode)]
        [InlineData("configuration")]
        [InlineData("tenants/default/namespaces/default/types")]

        public void EndpointsTest(string path)
        {
            Output.WriteLine($"Verify {Fixture.Client.BaseAddress}{path}");
            string content = Fixture.Client.DownloadString(path);
            Assert.True(!string.IsNullOrEmpty(content), "Failed to get local EDS config");
        }
    }
}
