using System;
using System.Collections.Generic;
using System.Text;

namespace WeETL
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class LogHeaderAttribute:Attribute
    {
        public LogHeaderAttribute(string title)
        {
            this.Title = title ?? "NOT DEFINED";
        }
        public LogHeaderAttribute(int length=15)
        {

        }
        public string Title { get; }
    }
}
