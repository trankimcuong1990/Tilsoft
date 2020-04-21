using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALBase.Resolver
{
    public class ByteArray2StringResolver : ValueResolver<byte[], string>
    {
        protected override string ResolveCore(byte[] source)
        {
            try
            {
                return Convert.ToBase64String(source);
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
