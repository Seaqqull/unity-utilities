using UnityEngine;


namespace Utilities.Properties.Base
{
    public class PropertyVariable<T> : ScriptableObject
    {
#if UNITY_EDITOR
#pragma warning disable 0414
        [Multiline] [SerializeField] private string _description = "";
#pragma warning restore 0414
#endif
        [SerializeField] private T _value;

        public T Value
        {
            get => _value;
            private set => _value = value;
        }


        public void SetValue(T value)
        {
            Value = value;
        }

        public void SetValue(PropertyVariable<T> value)
        {
            Value = value.Value;
        }
    }
}