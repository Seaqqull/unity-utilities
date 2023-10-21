using UnityEditor;


namespace UnityUtilities.Properties
{
    [CustomPropertyDrawer(typeof(IntegerReference))]
    public class IntegerReferenceDrawer : ReferencePropertyDrawer
    {
        protected override SerializedProperty ConstantValue { get; set; }
        protected override SerializedProperty UseConstant { get; set; }
        protected override SerializedProperty Variable { get; set; }


        protected override void InitializeProperties(SerializedProperty property)
        {
            ConstantValue = property.FindPropertyRelative(Base.PropertyReference<int>.EditorExtension.ConstantValue);
            UseConstant = property.FindPropertyRelative(Base.PropertyReference<int>.EditorExtension.UseConstant);
            Variable = property.FindPropertyRelative(Base.PropertyReference<int>.EditorExtension.Variable);
        }
    }
}