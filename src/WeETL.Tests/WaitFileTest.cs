using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WeETL.Observables;

namespace WeETL.Tests
{
    [TestClass]
    public class WaitFileTest
    {
        [TestMethod]
        public void TestWaitfile()
        {
            bool completed = false;
            Debug.WriteLine("Start wait file Test");
            WaitFile wf = new WaitFile(new WaitFileOptions()
            {
                Path = @"e:\",
                Filter = "*.txt"
            });
            wf.StopOnFirst = true;
            var disp = wf.Output.Subscribe(file =>
            {
                Debug.WriteLine($"{ file.EventArgs.Name} has {file.EventArgs.ChangeType.ToString()}");
            }, () => {
                //System.Environment.Exit(1);
                Debug.WriteLine("Stop listening");
                completed = true;
            });
            var cpt = 0;
            while (!completed)
            {
                Thread.Sleep(200);
                Debug.Write(".");
                if (++cpt > 20)
                {
                    Debug.WriteLine("");
                    cpt = 0;
                }
            }
        }
    }
}
