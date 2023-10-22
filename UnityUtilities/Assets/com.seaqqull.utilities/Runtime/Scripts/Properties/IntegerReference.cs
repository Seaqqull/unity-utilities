using System;


namespace Utilities.Properties
{
    [Serializable]
    public class IntegerReference : Base.PropertyReference<int>
    {
        public IntegerReference(int value) : base(value) { }
    }
}