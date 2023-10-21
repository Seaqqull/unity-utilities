using System;


namespace UnityUtilities.Properties
{
    [Serializable]
    public class IntegerReference : Base.PropertyReference<int>
    {
        public IntegerReference(int value) : base(value) { }
    }
}