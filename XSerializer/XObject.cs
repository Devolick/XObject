using System;
using System.Text;
using System.Text.RegularExpressions;

namespace XSerializer
{
    public static class XObject
    {
        public static string XSerialize<TObject>(TObject o)
        {
            if (o == null) throw new Exception();
            using(Serialize<TObject> mutation = new Serialize<TObject>())
            {
                return $"&{mutation.Build(o)}";
            }
        }
        public static TObject XDeserialize<TObject>(string x)
        {
            if (x == null) throw new Exception();
            using (Deserialize<TObject> appearance = new Deserialize<TObject>())
            {
                return (TObject)appearance.Build(typeof(TObject), Regex.Match(x,Queries.BODY).Value);
            }
        }
    }
}
