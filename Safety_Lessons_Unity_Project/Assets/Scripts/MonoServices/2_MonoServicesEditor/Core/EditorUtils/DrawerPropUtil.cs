using System.Collections.Generic;
using UnityEditor;


namespace MonoServiceEditor.Core
{
    public static class DrawerPropUtil
    {

        public static string[] SerialisedArray(SerializedProperty serialisedArray)
        {
            List<string> tempStringList = new List<string>();

            for (int i = 0; i < serialisedArray.arraySize; i++)
                tempStringList.Add(serialisedArray.GetArrayElementAtIndex(i).stringValue);

            return tempStringList.ToArray();
        }

    }
}