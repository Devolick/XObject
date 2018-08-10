using System;
using System.Collections.Generic;
using System.Text;

namespace XObjectSerializer.Tests
{
    public class ColletionHelper
    {
        public T ForSameKey<T>(int next)
        {
            Type type = typeof(T);
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Char:
                    return (T)Convert.ChangeType(next + 97, Type.GetTypeCode(type));
                case TypeCode.Boolean:
                case TypeCode.Byte:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.SByte:
                case TypeCode.Single:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    return (T)Convert.ChangeType(next,Type.GetTypeCode(type));
                case TypeCode.DateTime:
                    var d = DateTime.Now;
                    return (T)Convert.ChangeType(new DateTime(d.Ticks - (next * 10)), Type.GetTypeCode(type));
                case TypeCode.DBNull:
                    break;
                case TypeCode.Empty:
                    break;
                case TypeCode.Object:
                    return (T)new object();
                case TypeCode.String:
                    return (T)Convert.ChangeType(next.ToString(), Type.GetTypeCode(type));
            }
            return default(T);
        }
        public int CutSize<T>(int next)
        {
            Type type = typeof(T);
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Boolean:
                    return 2;
                case TypeCode.Byte:
                    return next > byte.MaxValue ? byte.MaxValue : next;
                case TypeCode.Char:
                    return next > 24 ? 24 : next;
                case TypeCode.Decimal:
                    return (int)(next > Decimal.MaxValue ? Decimal.MaxValue : next);
                case TypeCode.Double:
                    return (int)(next > Double.MaxValue ? Double.MaxValue : next);
                case TypeCode.Int16:
                    return (int)(next > Int16.MaxValue ? Int16.MaxValue : next);
                case TypeCode.Int32:
                    return (int)(next > Int32.MaxValue ? Int32.MaxValue : next);
                case TypeCode.Int64:
                    return next;
                case TypeCode.SByte:
                    return (int)(next > SByte.MaxValue ? SByte.MaxValue : next);
                case TypeCode.Single:
                    return (int)(next > Single.MaxValue ? Single.MaxValue : next);
                case TypeCode.UInt16:
                    return (int)(next > UInt16.MaxValue ? UInt16.MaxValue : next);
                case TypeCode.UInt32:
                    return next;
                case TypeCode.UInt64:
                    return next;
                case TypeCode.DateTime:
                    var d = DateTime.Now;
                    return (int)(d.Ticks);
                case TypeCode.DBNull:
                    return 1;
                case TypeCode.Empty:
                    return 1;
                case TypeCode.Object:
                    return 1;
                case TypeCode.String:
                    return next;
            }
            return -1;
        }

        public ColletionHelper() { }

    }
}
