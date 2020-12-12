using System;
using System.Collections.Generic;
using WeETL.DependencyInjection;
using WeETL.Utilities;

namespace WeETL.Observables.Dxf.Header
{
    public interface IDxfVersion:IComparable<IDxfVersion>,IComparer<IDxfVersion>,INamed
    {
        int Order { get; }
    }
    public class DxfVersion:IDxfVersion
    {

        #region ctor
        public DxfVersion(int order, string name)
        {
            Check.NotNull(name, nameof(name));
            Check.NotEmpty(name, nameof(name));
            this.Order= order;
            this.Name = name;
        }
        #endregion
        #region Properties
        public int Order { get;  }
        public string  Name { get; }
        #endregion
        #region Methods
        public override string ToString() => Name;
        
        #endregion
        #region Equality

        public int CompareTo(IDxfVersion other)
        {
            if (other == null) return 1;
            return other.Order - Order;
        }

        public int Compare(IDxfVersion x, IDxfVersion y)
        => x?.CompareTo(y) ?? 1;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            throw new NotImplementedException();
        }

        public override int GetHashCode()
        => Order;
        #endregion
        #region operators
        public static bool operator >(DxfVersion operand1, DxfVersion operand2)
        =>operand1.CompareTo(operand2) == 1;
        

        // Define the is less than operator.
        public static bool operator <(DxfVersion operand1, DxfVersion operand2)
        => operand1.CompareTo(operand2) == -1;
        

        // Define the is greater than or equal to operator.
        public static bool operator >=(DxfVersion operand1, DxfVersion operand2)
        =>operand1.CompareTo(operand2) >= 0;
        

        // Define the is less than or equal to operator.
        public static bool operator <=(DxfVersion operand1, DxfVersion operand2)
        =>operand1.CompareTo(operand2) <= 0;
        


        // Define the is less than or equal to operator.
        public static bool operator ==(DxfVersion operand1, DxfVersion operand2)
        => operand1.CompareTo(operand2) == 0;
        
        // Define the is less than or equal to operator.
        public static bool operator !=(DxfVersion operand1, DxfVersion operand2)
        => !(operand1 == operand2);
        #endregion
        #region Supported Version by this implementation
        public static DxfVersion Unknown = new DxfVersion(0, "Unknown");
        public static DxfVersion R10 = new DxfVersion(1, "AC1006");
        public static DxfVersion R11 = new DxfVersion(2, "AC1009");
        public static DxfVersion R13 = new DxfVersion(3, "AC1012");
        public static DxfVersion R14 = new DxfVersion(4, "AC1014");
        public static DxfVersion AutoCad2000 = new DxfVersion(5, "AC1015");
        public static DxfVersion AutoCad2004 = new DxfVersion(6, "AC1018");
        public static DxfVersion AutoCad2007 = new DxfVersion(7, "AC1021");
        public static DxfVersion AutoCad2010= new DxfVersion(8, "AC1024");
        public static DxfVersion AutoCad2013 = new DxfVersion(9, "AC1027");
        public static DxfVersion AutoCad2018 = new DxfVersion(10, "AC1032");



        #endregion
       
    }
}
