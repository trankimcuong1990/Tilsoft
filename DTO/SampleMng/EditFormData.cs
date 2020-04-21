using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.SampleMng
{
    public class EditFormData
    {
        public DTO.SampleMng.SampleOrder Data { get; set; }

        public List<Support.Season> Seasons { get; set; }
        public List<Support.Client> Clients { get; set; }
        public List<Support.SamplePurpose> SamplePurposes { get; set; }
        public List<Support.SampleTransportType> SampleTransportTypes { get; set; }
        public List<Support.Model> Models { get; set; }
        public List<Support.Material> Materials { get; set; }
        public List<Support.MaterialTypeOptionRaw> MaterialTypes { get; set; }
        public List<Support.MaterialColorOptionRaw> MaterialColors { get; set; }
        public List<Support.FrameMaterial> FrameMaterials { get; set; }
        public List<Support.FrameMaterialColor> FrameMaterialColors { get; set; }
        public List<Support.SubMaterial> SubMaterials { get; set; }
        public List<Support.SubMaterialColor> SubMaterialColors { get; set; }
        public List<Support.BackCushionOptionRaw> BackCushions { get; set; }
        public List<Support.SeatCushionOptionRaw> SeatCushions { get; set; }
        public List<Support.CushionColorOptionRaw> CushionColors { get; set; }
        public List<Support.User> Users { get; set; }
        public List<Support.Factory> Factories { get; set; }
        public List<Support.SampleRequestType> SampleRequestTypes { get; set; }
    }
}
