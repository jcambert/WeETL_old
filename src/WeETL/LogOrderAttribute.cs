using System;

namespace WeETL
{
    public class LogOrderAttribute:Attribute
    {
        public LogOrderAttribute(int order)
        {
            this.Order = order;
        }

        public int Order { get; }
    }
}
