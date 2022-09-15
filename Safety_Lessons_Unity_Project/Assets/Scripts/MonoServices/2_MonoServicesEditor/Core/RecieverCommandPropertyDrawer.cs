using MonoServices.Core;
using UnityEditor;
using UnityEngine;


namespace MonoServiceEditor.Core
{
    [CustomPropertyDrawer(typeof(RecieverCommand))]
    public class RecieverCommandPropertyDrawer : PropertyDrawer
    {
        // know how much space to reserve for drawing this property
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
            EditorGUI.GetPropertyHeight(property);

        // Draw the property inside the given rect
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {

            var serializedSelectedReciverCommandIndex = property.FindPropertyRelative("_selectedReciverCommandIndex");
            var serializedRecieverCommands = property.FindPropertyRelative("_recieverCommandsNames");

            Rect propRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

            var selectedReciverCommandIndexRect = propRect;

            serializedSelectedReciverCommandIndex.intValue = EditorGUI.Popup(
                  selectedReciverCommandIndexRect,
                  serializedSelectedReciverCommandIndex.displayName,
                  serializedSelectedReciverCommandIndex.intValue,
                  DrawerPropUtil.SerialisedArray(serializedRecieverCommands));
        }

    }
}