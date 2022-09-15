using MonoServices.Core;
using UnityEditor;
using UnityEngine;

namespace MonoServiceEditor.Core
{
    [CustomPropertyDrawer(typeof(MonoServiceCommand))]
    public class MonoServiceCommandPropertyDrawer : PropertyDrawer
    {

        // know how much space to reserve for drawing this property
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
            property.isExpanded ? EditorGUI.GetPropertyHeight(property) + 20 : EditorGUI.GetPropertyHeight(property);



        // Draw the property inside the given rect
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {

            // Using BeginProperty / EndProperty on the parent property means that    
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

                        var serialisedInvokerCommands = property.FindPropertyRelative("_invokerCommands");
                        var serialisedInvokerCommandsRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

                        if (serialisedInvokerCommands != null)
                        {
                            position.y += EditorGUI.GetPropertyHeight(serialisedInvokerCommands);
                            EditorGUI.PropertyField(serialisedInvokerCommandsRect, serialisedInvokerCommands);
                        }


                        var serialisedRecieverCommand = property.FindPropertyRelative("_recieverCommand");
                        var serialisedRecieverCommandRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

                        if (serialisedRecieverCommand != null)
                        {
                            position.y += EditorGUI.GetPropertyHeight(serialisedRecieverCommand);
                            EditorGUI.PropertyField(serialisedRecieverCommandRect, serialisedRecieverCommand);
                        }

                        var serialisedresetAllAlreadyCalledCommands = property.FindPropertyRelative("_resetAllAlreadyCalledCommands");
                        var serialisedresetAllAlreadyCalledCommandsRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

                        if (serialisedresetAllAlreadyCalledCommands != null)
                        {
                            position.y += EditorGUI.GetPropertyHeight(serialisedresetAllAlreadyCalledCommands);
                            EditorGUI.PropertyField(serialisedresetAllAlreadyCalledCommandsRect, serialisedresetAllAlreadyCalledCommands);
                        }



                        var refreshButtonRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
                        if (GUI.Button(refreshButtonRect, "Refresh Commmands"))
                        {
                            RefreshCommandNames(property.displayName, property);
                            Debug.Log("called");
                        }
                           
                            


                    }
                    EditorGUI.indentLevel--;
                }
            }

            EditorGUI.EndProperty();
        }



        void RefreshCommandNames(string holderIndex, SerializedProperty property)
        {
            var monoService = (property.serializedObject.targetObject as MonoService);

            for (int i = 0; i < monoService.MonoServiceParams.MonoServiceCommands.Length; i++)
            {
                var monoServiceCommand = monoService.MonoServiceParams.MonoServiceCommands[i];

                if (holderIndex.Contains(i.ToString()))
                {
                    monoServiceCommand.RefreshCommandNames(monoService);
                    continue;
                }
            }

            SceneMonoServicesFinder.RefreshCommandsReferences();
        }
    }
}