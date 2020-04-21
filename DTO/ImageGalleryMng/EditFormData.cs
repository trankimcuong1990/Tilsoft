using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ImageGalleryMng
{
    public class EditFormData
    {
        public ImageGallery Data { get; set; }
        public List<Support.Material> Materials { get; set; }
        public List<Support.MaterialTypeOptionRaw> MaterialTypes { get; set; }
        public List<Support.MaterialColorOptionRaw> MaterialColors { get; set; }
        public List<Support.BackCushionOptionRaw> BackCushionOptions { get; set; }
        public List<Support.SeatCushionOptionRaw> SeatCushionOption { get; set; }
        public List<Support.CushionColorOptionRaw> CushionColors { get; set; }
        public List<Support.SeasonType> SeasonTypes { get; set; }
        public List<Support.GalleryItemType> GalleryItemTypes { get; set; }
        public List<Support.Model> Models { get; set; }
        public List<Support.Client> Clients { get; set; }
    }
}
