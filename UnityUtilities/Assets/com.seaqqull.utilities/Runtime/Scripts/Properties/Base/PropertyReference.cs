using UnityEngine;
using System;


namespace Utilities.Properties.Base
{
    [Serializable]
    public class PropertyReference<T>
    {
        public static class EditorExtension
        {
            public static string ConstantValue = nameof(_constantValue);
            public static string UseConstant = nameof(_useConstant);
            public static string Variable = nameof(_variable);
        }
        
#pragma warning disable 0649
        [SerializeField] private bool _useConstant = true;
        [SerializeField] private T _constantValue;
        [SerializeField] private PropertyVariable<T> _variable;
#pragma warning restore 0649

        public T Value => _useConstant ? _constantValue : _variable.Value;


        public PropertyReference(T value)
        {
            _useConstant = true;
            _constantValue = value;
        }


        public static implicit operator T(PropertyReference<T> reference)
        {
            return reference.Value;
        }
    }
}