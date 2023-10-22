using System;


namespace Utilities.Properties
{
    [Serializable]
    public class StringReference : Base.PropertyReference<string>
    {
        public StringReference(string value) : base(value) { }
    }
}