using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeETL.Observables.Dxf;

namespace WeETL.Tests
{
    [TestClass]
    public class TestDxf
    {
      
        ETLContext ctx;


        [ClassInitialize]
        public static void ClassIntilialize(TestContext ctx)
        {

        }
        [TestInitialize]
        public void TestInitialize()
        {
            ctx = new ETLContext();
            Assert.IsNotNull(ctx);
            ctx.ConfigureService(cfg => {
                cfg.UseDxf();
            
            });
        }
        [TestCleanup]
        public void TestCleanup()
        {

        }
        [TestMethod]
        public void TestCreateSimpleDocument()
        {
            var doc=ctx.GetService<IDxfDocument>();
            Assert.IsNotNull(doc);

            Debug.WriteLine(doc.ToString());
        }
    }
}
