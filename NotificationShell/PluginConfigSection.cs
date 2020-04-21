using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationShell
{
    public class PluginTaskSection : ConfigurationSection 
    {
        [ConfigurationProperty("", IsRequired = true, IsDefaultCollection = true)]
        public PluginTaskCollection Instances
        {
            get { return (PluginTaskCollection)this[""]; }
            set { this[""] = value; }
        }
    }

    public class PluginTaskCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new PluginTaskElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            //set to whatever Element Property you want to use for a key
            return ((PluginTaskElement)element).Name;
        }
    }

    public class PluginTaskElement : ConfigurationElement
    {
        //Make sure to set IsKey=true for property exposed as the GetElementKey above
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get { return (string)base["name"]; }
            set { base["name"] = value; }
        }

        [ConfigurationProperty("code", IsRequired = true)]
        public string Code
        {
            get { return (string)base["code"]; }
            set { base["code"] = value; }
        }
    }

}
