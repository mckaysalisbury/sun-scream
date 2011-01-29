using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using ProtoBuf;

namespace Server
{
    //    [ProtoInclude(1, typeof(DescendedObject))]
    [DataContract]
    class TestObject
    {
        [DataMember(Order = 2, IsRequired = true)]
        public int Id { get; set; }

        [DataMember(Order = 3)]
        public float PositionX { get; set; }

        [DataMember(Order = 4)]
        public float PositionY { get; set; }

        [DataMember(Order = 5)]
        public List<Child> Children { get; set; }

        [DefaultValue(1)]
        [DataMember(Order = 6)]
        public int Defaulted { get; set; }

        [DefaultValue(TestEnum.None)]
        [DataMember(Order = 7)]
        public TestEnum TestEnum { get; set; }

        public TestObject()
        {
            Children = new List<Child>();

            Defaulted = 1;
        }
    }
}