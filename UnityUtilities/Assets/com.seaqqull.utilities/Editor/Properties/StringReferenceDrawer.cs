using UnityEditor;


namespace Utilities.Properties
{
    [CustomPropertyDrawer(typeof(StringReference))]
    public class StringReferenceDrawer : ReferencePropertyDrawer
    {
        protected override SerializedProperty ConstantValue { get; set; }
        protected override SerializedProperty UseConstant { get; set; }
        protected override SerializedProperty Variable { get; set; }


        protected override void InitializeProperties(SerializedProperty property)
        {
            ConstantValue = property.FindPropertyRelative(Base.PropertyReference<string>.EditorExtension.ConstantValue);
            UseConstant = property.FindPropertyRelative(Base.PropertyReference<string>.EditorExtension.UseConstant);
            Variable = property.FindPropertyRelative(Base.PropertyReference<string>.EditorExtension.Variable);
        }
    }
}