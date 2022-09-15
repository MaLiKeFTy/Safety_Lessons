using System.Collections.Generic;
using UnityEngine;

namespace MonoServices.Core
{
    public static class GameobjectMonoServiceFinder
    {
        static List<MonoService> eventCallers = new List<MonoService>();

        public static MonoService[] FindMonoServices(string objTag, GameObject gameObject, int parentNumber)
        {
            List<MonoService> tempListenerTags = new List<MonoService>();

            eventCallers.Clear();
            eventCallers = GetAllTags(gameObject, parentNumber);

            foreach (var tag in eventCallers)
            {
                if (Equals(tag.MonoServiceParams.MonoServiceTag, objTag))
                    tempListenerTags.Add(tag);
            }

            return tempListenerTags.ToArray();
        }

        public static string[] GetListenerTags(GameObject gameObject, int parentNumber)
        {
            List<string> tempListenerTags = new List<string>();

            foreach (var tag in GetAllTags(gameObject, parentNumber))
                tempListenerTags.Add(tag.MonoServiceParams.MonoServiceTag);

            return tempListenerTags.ToArray();
        }

        static List<MonoService> GetAllTags(GameObject gameObject, int parentNumber)
        {
            List<MonoService> eventCallers = new List<MonoService>();
            List<MonoService> tempComponents = new List<MonoService>();


            if (parentNumber == 0)
            {
                tempComponents.AddRange(gameObject.GetComponentsInChildren<MonoService>());
            }
            else
            {

                GameObject mainParent = gameObject;

                for (int i = 0; i < parentNumber; i++)
                    mainParent = mainParent.transform.parent.gameObject;

                tempComponents.AddRange(mainParent.GetComponentsInParent<MonoService>());
                tempComponents.AddRange(mainParent.GetComponentsInChildren<MonoService>());
            }


            foreach (var component in tempComponents)
            {
                if (component.MonoServiceParams.MonoServiceTag != "")
                    eventCallers.Add(component);
            }

            return eventCallers;
        }
    }
}

