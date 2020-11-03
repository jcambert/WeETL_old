using System;

namespace WeETL
{
    [AttributeUsage(AttributeTargets.Property,AllowMultiple =false,Inherited =true)]
    public class KeyAttribute:Attribute
    {
    }
}
