using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DTO.Support
{
    public class TestSamplingOption
    {
        public int TestSamplingOptionID { get; set; }
        public string TestSamplingOptionNM { get; set; }
    }
}