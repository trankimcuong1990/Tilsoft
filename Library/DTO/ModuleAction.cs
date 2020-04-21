using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DTO
{
    public enum ModuleAction
    {
        CanCreate,
        CanRead,
        CanUpdate,
        CanDelete,
        CanPrint,
        CanApprove,
        CanReset
    }
}
