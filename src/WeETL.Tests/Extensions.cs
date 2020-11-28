using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WeETL.Core;

namespace WeETL.Tests
{
    public static class Extensions
    {
        internal static void RegisterComponentForEvents(this ETLCoreComponent c, IScheduler scheduler)
        {
            c.OnStart.SubscribeOn(scheduler).Subscribe(OnComponentStart);
            c.OnError.SubscribeOn(scheduler).Subscribe(OnError);
            c.OnCompleted.SubscribeOn(scheduler).Subscribe(OnComponentCompleted);
        }

        private static void OnComponentStart((ETLCoreComponent comp, DateTime dt) c)
        {
            Debug.WriteLine($"{c.comp.Name} start at".PadLeft(50) + $" -> {c.dt.ToShortTimeString()}");

        }
        private static void OnComponentCompleted((ETLCoreComponent comp, DateTime dt) c)
        {
            Debug.WriteLine($"{c.comp.Name} Completed in".PadLeft(50) + $" -> {c.comp.ElapsedTime}");
        }


        private static void OnError(ConnectorException e)
        {
            Debug.Write("** AN ERROR HAPPENED ** => ");
            Debug.WriteLine(e.Message + "\n" + e.InnerException.Message);
            Assert.Fail(e.InnerException.Message);
        }

        private static string BaseLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        internal static string GetLocalPath(this string filename)
        => Path.Combine(BaseLocation, filename);

    }
}
