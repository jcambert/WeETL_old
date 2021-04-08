using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WeETL.Observables.PN2000;
using WeETL.Observables.PN2000.IO;

namespace WeETL.Tests
{
    [TestClass]
    public class TestPnCad
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
                cfg.UseCommonUtilities()
                .UsePnCad();
            });
        }
        [TestCleanup]
        public void TestCleanup()
        {

        }
        [TestMethod]
        public void TestLoadExistingDocument()
        {
            var reader = ctx.GetService<IPnCadReader>();
            Assert.IsNotNull(reader);
            var doc = ctx.GetService<IPnCadDocument>();
            Assert.IsNotNull(doc);
            bool isLoaded = false;
            reader.OnLoaded.Subscribe(doc =>
            {
                isLoaded = true;
            });
            reader.OnError.Subscribe(e =>
            {
                Assert.Fail(e.Message);
            });
            string filename = @"P:\u\ar\ar0032\c2d\TEST";
            Assert.IsTrue(File.Exists(filename));
            reader.Load(filename);
            while (!isLoaded)
                Thread.Sleep(200);
            Debug.WriteLine("PN Cad Document i readed");
        }
        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void TestLoadInexistingDocument()
        {
            var reader = ctx.GetService<IPnCadReader>();
            var doc = ctx.GetService<IPnCadDocument>();
            Assert.IsNotNull(reader);
            Assert.IsNotNull(doc);
           
            bool isLoaded = false;
            reader.OnLoaded.Subscribe(doc =>
            {
                isLoaded = true;
            });
            reader.OnError.Subscribe(e =>
            {
                throw e;
            });
            string filename = @"P:\toto.toto";
            Assert.IsFalse(File.Exists(filename));
            reader.Load(filename);
            while (!isLoaded)
                Thread.Sleep(200);
        }

        [TestMethod]
        public void TestReadExistingDocument()
        {
            var reader = ctx.GetService<IPnCadReader>();
            Assert.IsNotNull(reader);
            var doc = ctx.GetService<IPnCadDocument>();
            Assert.IsNotNull(doc);
            bool isLoaded = false;
            reader.OnLoaded.Subscribe(doc =>
            {
                Debug.WriteLine(doc.Schema[0].ToString());
                isLoaded = true;
            });
            reader.OnError.Subscribe(e =>
            {
                Assert.Fail(e.Message);
            });
            ((PnCadReader)reader).OnLine.Where(line=>!string.IsNullOrWhiteSpace(line)).Subscribe(line => {
                Debug.WriteLine(line);
            });
           // string filename = @"P:\u\ar\ar0032\c2d\TEST";
            string filename = @"0128";
            Assert.IsTrue(File.Exists(filename));
            reader.Load(filename);
            while (!isLoaded)
                Thread.Sleep(200);
            Debug.WriteLine("PN Cad Document i readed");
        }
    }
}
