﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryTeam.DTO
{
    class EditFormDataDTO
    {
        public FactoryTeamDTO Data { get; set; }
        public List<Support.DTO.FactoryStep> FactorySteps { get; set; }
        public List<Support.DTO.FactoryMaterialGroup> FactoryMaterialGroups { get; set; }
    }
}
