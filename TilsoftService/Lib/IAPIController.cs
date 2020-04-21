using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace TilsoftService.Lib
{
    public interface IAPIController<TDTO>
    {
        string getModuleCode();
        IHttpActionResult Gets(DTO.Search searchInput);
        IHttpActionResult Get(int id);
        IHttpActionResult Update(int id, TDTO dtoItem);
        IHttpActionResult Delete(int id);
        IHttpActionResult Approve(int id, TDTO dtoItem);
        IHttpActionResult Reset(int id, TDTO dtoItem);        
    }
}
