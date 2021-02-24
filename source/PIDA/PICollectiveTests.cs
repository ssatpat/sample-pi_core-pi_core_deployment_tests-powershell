using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OSIsoft.AF.Asset;
using OSIsoft.AF.Collective;
using OSIsoft.AF.PI;
using OSIsoft.AF.Time;
using Xunit;
using Xunit.Abstractions;

namespace OSIsoft.PISystemDeploymentTests
{
    /// <summary>
    /// Testing PI Collectives
    /// </summary>
    public class PICollectiveTests : IClassFixture<PIFixture>
    {
        /// <summary>
        /// Constructor for PICollectiveTests Class.
        /// </summary>
        /// <param name="output">The output logger used for writing messages.</param>
        /// <param name="fixture">Fixture to manage connection and specific helper functions</param>
        public PICollectiveTests(ITestOutputHelper output, PIFixture fixture)
        {
            Output = output;
            Fixture = fixture;
        }

        private PIFixture Fixture { get; }
        private ITestOutputHelper Output { get; }

        /// <summary>
        /// Tests to see if the PIDataArchive is a PI Collective
        /// </summary>
        /// <remarks>
        /// Errors if the current PIDataArchive is not a PI Collective
        /// </remarks>
        [Fact]
        public void IsCollective()
        {
            var collective = Fixture.PIServer.Collective;
            Assert.NotNull(collective);
            Output.WriteLine("PI Server is a Collective!");
        }

        /// <summary>
        /// Tests to see if the PIDataArchive is a PI Collective
        /// </summary>
        /// <remarks>
        /// Errors if the current PIDataArchive is not a PI Collective
        /// </remarks>
        [Fact]
        public void CanConnectToAllMembers()
        {
            PICollective collective = Fixture.PIServer.Collective;
            List<PICollectiveMember> cantConnect = new List<PICollectiveMember>();
            foreach (var member in collective.Members)
            {
                member.Connect();
                if (!member.IsConnected) 
                {
                    cantConnect.Append(member);
                }
            }
            
            Assert.Empty(cantConnect);
            Output.WriteLine($"Cant connect to {cantConnect.Count} members!");
        }

        /// <summary>
        /// Test to check whether data is being replicated
        /// </summary>
        [Fact]
        public void DataReplication()
        {
            PIServer srv = Fixture.PIServer;
            PICollective collective = Fixture.PIServer.Collective;
            string pointName = "CollectiveDataReplicationTest";
            var check = PIPoint.TryFindPIPoint(srv, pointName, out PIPoint piPoint);
            if (srv.Collective.ConnectedMemberType == AFServerRole.Primary)
            {
                if (!check)
                {
                    srv.CreatePIPoint(pointName);
                }
            }

            List<PIPoint> pplist = new List<PIPoint>();
            int found_count = 0;
            foreach (var member in collective.Members)
            {
                member.Connect();
                if (member.IsConnected)
                {
                    var foundPoint = PIPoint.TryFindPIPoint(member.PIServer, pointName, out PIPoint pipoint);
                    if (foundPoint)
                    {
                        found_count++;
                    }                       
                }
            }

            Assert.Equal(collective.Members.Count, found_count);
        }
    }
}
