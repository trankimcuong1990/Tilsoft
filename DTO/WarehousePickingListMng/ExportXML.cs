using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.ComponentModel;

namespace DTO.WarehousePickingListMng
{
    [XmlRootAttribute("shipment", IsNullable = true)]
    public class ExportXML
    {
        [XmlAttribute]
        public string id { get; set; }

        [XmlAttribute]
        public string number { get; set; }
        
        [XmlElement(IsNullable=true)]
        public Header header { get; set; }

        [XmlElement(IsNullable = true)]
        public Sender sender { get; set; }

        [XmlElement(IsNullable = true)]
        public Receiver receiver { get; set; }
        
        [XmlArrayAttribute("items", IsNullable = true)]
        public Item[] items { get; set; }
    }


    public class Header
    {
        [XmlElement(IsNullable = true)]
        public string codamount { get; set; }

        [XmlElement(IsNullable = true)]
        public string codcurrency { get; set; }

        [XmlElement(IsNullable = true)]
        public string delivery { get; set; }

        [XmlElement(IsNullable = true)]
        public References references { get; set; }
    }

    public class References
    {
        [XmlElement(IsNullable = true)]
        public string xxlcare { get; set; }

        [XmlElement(IsNullable = true)]
        public Customer customer { get; set; }
    }

    public class Customer
    {
        [XmlAttribute]
        public string line { get; set; }

        [XmlText]
        public string value { get; set; }
    }

    public class Sender
    {
        [XmlElement(IsNullable = true)]
        public string name { get; set; }

        [XmlElement(IsNullable = true)]
        public string address { get; set; }

        [XmlElement(IsNullable = true)]
        public string postalCode { get; set; }

        [XmlElement(IsNullable = true)]
        public string city { get; set; }

        [XmlElement(IsNullable = true)]
        public string country { get; set;}
    }

    public class Receiver
    {
        [XmlElement(IsNullable = true)]
        public string name { get; set; }

        [XmlElement(IsNullable = true)]
        public string address { get; set; }

        [XmlElement(IsNullable = true)]
        public string postalCode { get; set; }

        [XmlElement(IsNullable = true)]
        public string city { get; set; }

        [XmlElement(IsNullable = true)]
        public string countryCode { get; set; }

        [XmlElement(IsNullable = true)]
        public string phoneNumber { get; set; }

        [XmlElement(IsNullable = true)]
        public string emailAddress { get; set; }
    }


    [XmlType(TypeName = "item")]
    public class Item
    {
        [XmlAttribute]
        public string name { get; set; }

        [XmlElement(IsNullable = true)]
        public string size { get; set; }

        [XmlElement(IsNullable = true)]
        public string quantity { get; set; }

        [XmlElement(IsNullable = true)]
        public Product product { get; set; }

        [XmlArrayAttribute(IsNullable = true)]
        public Dimension[] dimensions { get; set; }
    }

    public class Product
    {
        [XmlAttribute]
        public string sku { get; set; }

        [XmlAttribute]
        public string manufacturerCode { get; set; }

        [XmlAttribute]
        public string ean { get; set; }

        [XmlText]
        public string value { get; set; }
    }

    [XmlType(TypeName = "dimension")] // set this value to change the name of object item in array
    public class Dimension
    {
        [XmlElement(IsNullable=true)]
        public string lenght { get; set; }

        [XmlElement(IsNullable = true)]
        public string width { get; set; }

        [XmlElement(IsNullable = true)]
        public string height { get; set; }

        [XmlElement(IsNullable = true)]
        public string size { get; set; }
    }

}
