﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PackageDetection.ConfigurationModule.TransmissionDataClass
{
    public abstract class CollisionData
    {
        protected string name;
        protected List<string> ArgsNames;
        protected Dictionary<string, int> args = new Dictionary<string, int>();

        public Dictionary<string, int> Args { get => args; }
        public string Name { get => name; set => name = value; }

        public abstract void SetComponentsByXML(XElement reader);

        
    }

}