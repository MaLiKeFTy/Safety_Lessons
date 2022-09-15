using MonoServices.Core;
using UnityEngine;

namespace MonoServices.Children
{
    public class GameObjChildEnabler : MonoService
    {
        void EnableChildWithIndexCommand(int index)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(index == i);
            }
        }

        void EnbleChildWithGameObjCommand(GameObject gameObject)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);

                child.gameObject.SetActive(child.gameObject == gameObject);
            }
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) EnableChildWithIndexCommand((int)passedObj);
            if (methodNumb == 1) EnbleChildWithGameObjCommand((GameObject)passedObj);
        }

    }

}

