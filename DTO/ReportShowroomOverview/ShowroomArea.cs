﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ReportShowroomOverview
{
    public class ShowroomArea
    {
        public int ShowroomAreaID { get; set; }
        public string ShowroomAreaUD { get; set; }
        public string ShowroomAreaNM { get; set; }
        public int? DisplayIndex { get; set; }
        public List<ShowroomOverview> ShowroomOverviews { get; set; }
        public List<ShowroomAreaImage> ShowroomAreaImages { get; set; }
    }
}
