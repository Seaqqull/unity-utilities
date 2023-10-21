using System;


namespace UnityUtilities.Properties
{
    [Serializable]
    public class StringReference : Base.PropertyReference<string>
    {
        public StringReference(string value) : base(value) { }
    }
}