using MonoServices.Core;
using UnityEditor;
using UnityEngine;

namespace MonoServiceEditor.Core
{
    [CustomPropertyDrawer(typeof(InvokerCommand))]
    public class InvokerCommandPropertyDrawer : PropertyDrawer
    {
        // know how much space to reserve for drawing this property
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
            /*property.isExpanded ? EditorGUI.GetPropertyHeight(property) - 70 :*/ EditorGUI.GetPropertyHeight(property);

        // Draw the property inside the given rect
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // prefab override logic works on the entire property.
            EditorGUI.BeginProperty(position, label, property);
            {
                // Get the rect for where to draw the label/foldout
                var labelRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
                position.y += EditorGUIUtility.singleLineHeight;

                // Draw the foldout
                property.isExpanded = EditorGUI.Foldout(labelRect, property.isExpanded, property.displayName);

                if (property.isExpanded)
                {
                    // indent children for better readability
                    EditorGUI.indentLevel++;
                    {
                        var serializedInvokerCommand = property.FindPropertyRelative("_invokerCommand");
                        var serializedInvokerCommandRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
                        position.y += EditorGUI.GetPropertyHeight(serializedInvokerCommand);
                        EditorGUI.PropertyField(serializedInvokerCommandRect, serializedInvokerCommand);


                        var serializedInvokerResetter = property.FindPropertyRelative("_invokerResetter");
                        var serializedInvokerResetterRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

                        position.y += EditorGUI.GetPropertyHeight(serializedInvokerResetter);
                        EditorGUI.PropertyField(serializedInvokerResetterRect, serializedInvokerResetter);

                    }
                    EditorGUI.indentLevel--;
                }
            }

            EditorGUI.EndProperty();
        }
    }

}

