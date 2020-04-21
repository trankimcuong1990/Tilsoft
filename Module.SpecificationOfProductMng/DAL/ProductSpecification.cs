//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.SpecificationOfProductMng.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductSpecification
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductSpecification()
        {
            this.ProductSpecificationCushionImage = new HashSet<ProductSpecificationCushionImage>();
            this.ProductSpecificationImage = new HashSet<ProductSpecificationImage>();
            this.ProductSpecificationPacking = new HashSet<ProductSpecificationPacking>();
            this.ProductSpecificationWeavingFile = new HashSet<ProductSpecificationWeavingFile>();
            this.ProductSpecificationWoodenPart = new HashSet<ProductSpecificationWoodenPart>();
        }
    
        public int ProductSpecificationID { get; set; }
        public string ProductOverallDimL { get; set; }
        public string ProductOverallDimW { get; set; }
        public string ProductOverallDimH { get; set; }
        public string ProductOverallWeight { get; set; }
        public string FrameTDCode { get; set; }
        public string FrameDimensionL { get; set; }
        public string FrameDimensionW { get; set; }
        public string FrameDimensionH { get; set; }
        public string FrameMaterial { get; set; }
        public string FrameColor { get; set; }
        public string FrameColorCode { get; set; }
        public string FrameWelding { get; set; }
        public string FrameWeight { get; set; }
        public string WoodenPartTDCode { get; set; }
        public string WoodenPartDimensionL { get; set; }
        public string WoodenPartDimensionW { get; set; }
        public string WoodenPartDimensionH { get; set; }
        public string WoodenPartSpecies { get; set; }
        public string WoodenPartColor { get; set; }
        public string WoodenPartSnail { get; set; }
        public string WickerColorCode { get; set; }
        public string WickerType { get; set; }
        public string WickerColor { get; set; }
        public string WickerWeight { get; set; }
        public string WeavingMethod { get; set; }
        public string TextilenDimensionL { get; set; }
        public string TextilenDimensionW { get; set; }
        public string TextilenColor { get; set; }
        public string CushionColor { get; set; }
        public string CushionWeight { get; set; }
        public string CushionDimensionL { get; set; }
        public string CushionDimensionW { get; set; }
        public string CushionDimensionH { get; set; }
        public string CushionLine { get; set; }
        public string CushionWashingLabelDimL { get; set; }
        public string CushionWashingLabelDimW { get; set; }
        public string CartonBoxTDCode { get; set; }
        public string CartonBoxType { get; set; }
        public string CartonBoxDimensionL { get; set; }
        public string CartonBoxDimensionW { get; set; }
        public string CartonBoxDimensionH { get; set; }
        public string HardwareRing { get; set; }
        public string HardwareSpring { get; set; }
        public string HardwareConnector { get; set; }
        public string HardwareLeveller { get; set; }
        public string AssemblyInstructionCode { get; set; }
        public string OtherDescription { get; set; }
        public string Remark { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<bool> IsConfirmed { get; set; }
        public Nullable<int> ConfirmedBy { get; set; }
        public Nullable<System.DateTime> ConfirmedDate { get; set; }
        public Nullable<int> FactoryID { get; set; }
        public Nullable<int> SampleProductID { get; set; }
        public string HardwareBoltQuantity { get; set; }
        public string HardwareBoltDimension { get; set; }
        public string HardwareKeyDimensionL { get; set; }
        public string HardwareKeyDimensionW { get; set; }
        public string HardwareKeyThickness { get; set; }
        public string FrameCoatingColor { get; set; }
        public Nullable<int> ProductID { get; set; }
        public Nullable<int> ClientID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductSpecificationCushionImage> ProductSpecificationCushionImage { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductSpecificationImage> ProductSpecificationImage { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductSpecificationPacking> ProductSpecificationPacking { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductSpecificationWeavingFile> ProductSpecificationWeavingFile { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductSpecificationWoodenPart> ProductSpecificationWoodenPart { get; set; }
    }
}
