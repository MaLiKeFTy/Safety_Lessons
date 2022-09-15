using UnityEngine;
using System.Collections;
using UnityEditor;
using MonoServices.Core;

namespace MonoServiceEditor.Core
{
    [CustomEditor(typeof(MonoService))]
    public class MonoServiceEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            MonoService myTarget = (MonoService)target;

            if (GUILayout.Button("Build Object"))
            {
            }

        }
    }
}