using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALBase.Resolver
{
    public class DateTime2JsonResolver : ValueResolver<DateTime, string>
    {
        protected override string ResolveCore(DateTime source)
        {
            try
            {
                if (source.Year == 1) return string.Empty;

                return source.ToString("dd/MM/yyyy");
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
