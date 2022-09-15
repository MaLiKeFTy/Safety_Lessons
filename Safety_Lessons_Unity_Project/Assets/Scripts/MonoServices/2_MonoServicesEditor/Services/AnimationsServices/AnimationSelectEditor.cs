using UnityEngine;
using UnityEditor;
using MonoServices.Animations;

namespace MonoServiceEditor.Animations
{
    [CustomEditor(typeof(AnimationPlayer))]
    public class AnimationSelectEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (target is AnimationPlayer playScript)
            {
                var arrayLabel = new GUIContent("Play Animation");
                playScript.PlayIndex = EditorGUILayout.Popup(arrayLabel, playScript.PlayIndex, playScript.AnimeNames);
            }

        }
    }
}

