using Microsoft.VisualStudio.TestTools.UnitTesting;
using CintRobot;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using CintRobotTest;
using System.Collections.Generic;

namespace CintRobotUnitTests
{
    [TestClass]
    public class RobotUnitTests
    {
        [TestMethod]
        public void ExecuteCommands_Test_Positive()
        {
           string startLocation = "0 0";
            var commands = new List<string>
            {
                "E 5",
                "W 5",
                "N 5",
                "S 5"
            };
           var service = new RobotService();
           int spacesCleaned = service.ExecuteCommands(commands, startLocation);
            Assert.AreEqual(11, spacesCleaned);
        }

        [TestMethod]
        public void ExecuteCommands_Test_SemiStressTest()
        {
            string startLocation = "10 15";
            var commands = new List<string>
            {
                "E 100000",
                "W 100000",
                "N 100000",
                "S 100000"
            };
            var service = new RobotService();
            int spacesCleaned = service.ExecuteCommands(commands, startLocation);
            Assert.AreEqual(200001, spacesCleaned);

        }

        [TestMethod]
        public void CalculateCoordinatesTouched_N()
        {
            List<string> coordinateList = new List<string>();
            RobotService service = new RobotService();
            coordinateList = service.CalculateCoordinatesTouched("0 0","N",10, out string stoppedLocation);
            CollectionAssert.Contains(coordinateList, "10 0");
            Assert.IsTrue(coordinateList.Count == 10);
        }

        [TestMethod]
        public void CalculateCoordinatesTouched_S()
        {
            List<string> coordinateList = new List<string>();
            RobotService service = new RobotService();
            coordinateList = service.CalculateCoordinatesTouched("0 0", "S", 10, out string stoppedLocation);
            CollectionAssert.Contains(coordinateList, "-10 0");
            Assert.IsTrue(coordinateList.Count == 10);
        }

        [TestMethod]
        public void CalculateCoordinatesTouched_E()
        {
            List<string> coordinateList = new List<string>();
            RobotService service = new RobotService();
            coordinateList = service.CalculateCoordinatesTouched("0 0", "E", 10, out string stoppedLocation);
            CollectionAssert.Contains(coordinateList, "0 10");
            Assert.IsTrue(coordinateList.Count == 10);
        }

        [TestMethod]
        public void CalculateCoordinatesTouched_W()
        {
            List<string> coordinateList = new List<string>();
            RobotService service = new RobotService();
            coordinateList = service.CalculateCoordinatesTouched("0 0", "W", 10, out string stoppedLocation);
            CollectionAssert.Contains(coordinateList, "0 -10");
            Assert.IsTrue(coordinateList.Count == 10);
        }
    }
}
