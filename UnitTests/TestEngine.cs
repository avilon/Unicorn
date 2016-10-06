using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Unicorn;

namespace UnitTests
{
    [TestClass]
    public class TestEngine
    {
        [TestMethod]
        public void TestCreate()
        {
            Engine engine = new Engine();
            Assert.IsNotNull(engine);
        }
    }
}
