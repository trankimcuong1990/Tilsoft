using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.LabelingPackaging.DTO
{
    public class LabelingPackagingRejection
    {
        public int LabelingPackagingRejectionID { get; set; }

        public int? LabelingPackagingID { get; set; }

        public string LabelingPackagingRejectionComment { get; set; }

        public string LabelingPackagingRejectionCommentFile { get; set; }

        public string LabelingPackagingRejectionFriendlyName { get; set; }

        public string LabelingPackagingRejectionFileLocation { get; set; }

        public string LabelingPackagingRejectionThumbnailLocation { get; set; }

        public bool LabelingPackagingRejectionFileHasChange { get; set; }

        public string LabelingPackagingRejectionFileNew { get; set; }
    }
}
