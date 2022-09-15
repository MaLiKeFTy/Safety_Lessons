using MonoServices.Core;
using UnityEditor;
using UnityEngine;

namespace MonoServiceEditor.Core
{
    [CustomPropertyDrawer(typeof(InvokerCommandParams))]
    public class InvokerCommandParamsPropertyDrawer : PropertyDrawer
    {
        // know how much space to reserve for drawing this property
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
            property.isExpanded ? EditorGUI.GetPropertyHeight(property) - 80 : EditorGUI.GetPropertyHeight(property);

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
                        var serializedSelectedMonoServiceTag = property.FindPropertyRelative("_selectedMonoServiceTag");
                        var serializedMonoServiceTagNames = property.FindPropertyRelative("_monoSerciveTagNames");
                        var selectedReciverCommandIndexRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
                        position.y += EditorGUI.GetPropertyHeight(serializedSelectedMonoServiceTag);
                        serializedSelectedMonoServiceTag.intValue = EditorGUI.Popup(
                              selectedReciverCommandIndexRect,
                              serializedSelectedMonoServiceTag.displayName,
                              serializedSelectedMonoServiceTag.intValue,
                              DrawerPropUtil.SerialisedArray(serializedMonoServiceTagNames));



                        var serializedMonoServiceIsInSameGameobj = property.FindPropertyRelative("_monoServiceIsAFamRelative");
                        var serializedMonoServiceIsInSameGameobjRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
                        position.y += EditorGUI.GetPropertyHeight(serializedMonoServiceTagNames);
                        EditorGUI.PropertyField(serializedMonoServiceIsInSameGameobjRect, serializedMonoServiceIsInSameGameobj);


                        var serializedParentNumber = property.FindPropertyRelative("_parentNumber");
                        var serializedParentNumberRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

                        if (serializedMonoServiceIsInSameGameobj.boolValue)
                        {
                            position.y += EditorGUI.GetPropertyHeight(serializedParentNumber);
                            EditorGUI.PropertyField(serializedParentNumberRect, serializedParentNumber);
                        }


                        var serializedSelectedInvokerCommandIndex = property.FindPropertyRelative("_selectedInvokerCommandIndex");
                        var serializedInvokerCommandNames = property.FindPropertyRelative("_invokerCommandNames");
                        var serializedSelectedInvokerCommandIndexRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

                        if (serializedSelectedInvokerCommandIndex != null)
                        {
                            position.y += EditorGUI.GetPropertyHeight(serializedSelectedInvokerCommandIndex);
                            serializedSelectedInvokerCommandIndex.intValue = EditorGUI.Popup(
                                  serializedSelectedInvokerCommandIndexRect,
                                  serializedSelectedInvokerCommandIndex.displayName,
                                  serializedSelectedInvokerCommandIndex.intValue,
                                  DrawerPropUtil.SerialisedArray(serializedInvokerCommandNames));
                        }


                        var serializedAlreadyCalled = property.FindPropertyRelative("_alreadyCalled");
                        var serializedAlreadyCalledRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

                        position.y += EditorGUI.GetPropertyHeight(serializedAlreadyCalled);
                        EditorGUI.PropertyField(serializedAlreadyCalledRect, serializedAlreadyCalled);
                    }
                    EditorGUI.indentLevel--;
                }
            }

            EditorGUI.EndProperty();
        }
    }
}