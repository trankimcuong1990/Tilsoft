﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryTeam.DTO
{
    public class SearchFormDataDTO
    {
        public List<FactoryTeamDTO> Data { get; set; }
        public List<Support.DTO.FactoryStep> FactorySteps { get; set; }
    }
}
