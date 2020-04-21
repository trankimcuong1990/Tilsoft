using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.UserGroupMng
{
    public class UserGroupSearchResult
    {
        public int UserGroupID { get; set; }

        public string UserGroupNM { get; set; }

        public string Description { get; set; }

    }
}