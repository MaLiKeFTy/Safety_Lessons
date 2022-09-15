using MonoServices.Core;
using System.Collections.Generic;
using UnityEngine;

namespace MonoServices.Children
{
    public class MonoserviceChildIndexGetter : MonoService
    {
        [SerializeField] string _childrenMonoserviceTag;
        [SerializeField] List<MonoService> _children = new List<MonoService>();
        [SerializeField] bool _getChildrenOnStart;

        protected override void Start()
        {
            base.Start();

            if (_getChildrenOnStart)
                GetChildrenMonoServicesCommand();
        }

        void GetChildrenMonoServicesCommand()
        {
            _children.Clear();

            foreach (var childMonoservice in GetComponentsInChildren<MonoService>())
            {
                if (childMonoservice.MonoServiceParams.MonoServiceTag == _childrenMonoserviceTag)
                    _children.Add(childMonoservice);
            }

            InvokeCommand(0);
        }

        void GetChildWithIndexCommand(int childIndex)
        {
            GetChildrenMonoServicesCommand();

            MonoService foundChild = null;

            for (int i = 0; i < _children.Count; i++)
            {
                if (i == childIndex)
                {
                    foundChild = _children[i];
                    break;
                }
            }

            if (foundChild)
            {
                GetFoundChildIndexCommand(foundChild);
                GetCurrChildCommand(foundChild.gameObject);
                InvokeCommand(1, foundChild.transform);
            }
        }

        void GetFoundChildIndexCommand(MonoService foundChild)
        {

            int foundChildIndex = 99;

            for (int i = 0; i < _children.Count; i++)
            {
                if (_children[i] == foundChild)
                {
                    foundChildIndex = i;
                    break;
                }
            }

            InvokeCommand(2, foundChildIndex);
        }

        void GetSelectedChildCommand(GameObject foundChild)
        {
            for (int i = 0; i < _children.Count; i++)
            {
                if (_children[i].gameObject == foundChild)
                {
                    GetCurrChildCommand(foundChild);
                    InvokeCommand(3, _children[i].gameObject);
                    break;
                }
            }
        }

        void GetCurrChildCommand(GameObject currChild)
        {
            InvokeCommand(4, currChild);
        }

        void GetChildrenGameObjsCommand()
        {
            List<GameObject> childrenGameObj = new List<GameObject>();

            foreach (var child in _children)
                childrenGameObj.Add(child.gameObject);

            InvokeCommand(5, childrenGameObj.ToArray());

        }


        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) GetChildrenMonoServicesCommand();
            if (methodNumb == 1) GetChildWithIndexCommand((int)passedObj);
            if (methodNumb == 3) GetSelectedChildCommand((GameObject)passedObj);
            if (methodNumb == 5) GetChildrenGameObjsCommand();
        }
    }
}