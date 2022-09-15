using MonoServices.Core;
using System.Collections.Generic;
using UnityEngine;

namespace MonoServices.Children
{
    public class ChildrenStackLooping : MonoService
    {
        [SerializeField] float _offset;
        [SerializeField] bool _stackOnStart;
        [SerializeField] bool _stackLoopingOnStart = true;
        [SerializeField] uint _stacksToShowCount = 3;

        float _addedOffset;

        protected override void Start()
        {
            base.Start();

            if (_stackOnStart)
                StackChildrenCommand();
        }

        void StackChildrenCommand()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (i == 0)
                    _addedOffset = transform.position.y;

                var child = transform.GetChild(i);

                child.position = new Vector3(
                    child.position.x,
                    _addedOffset,
                    child.position.z);

                _addedOffset += _offset;
            }

            if (_stackLoopingOnStart)
                StackAndMaskChildrenCommand();
        }

        void PlaceOnTopOfStackCommand()
        {
            transform.GetChild(0).SetAsLastSibling();

            RearrangeChildren();
        }

        void PlaceOnBottomOfStackCommand()
        {
            transform.GetChild(transform.childCount - 1).SetAsFirstSibling();

            RearrangeChildren();
        }

        void RearrangeChildren()
        {
            StackChildrenCommand();

            foreach (var childToShow in ChildrenToShow())
                childToShow.transform.localScale = Vector3.one;

            foreach (var childToHide in ChildrenToHide())
                childToHide.transform.localScale = Vector3.zero;
        }


        void StartStackLoopingCommand()
        {
        }

        void StopStackLoopingCommand()
        {
        }

        void MaskChildrenCommand()
        {
            for (int i = (int)_stacksToShowCount; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                child.transform.localScale = Vector3.zero;
            }
        }

        void StackAndMaskChildrenCommand()
        {
            StackChildrenCommand();
            MaskChildrenCommand();
        }

        Transform[] ChildrenToHide()
        {
            List<Transform> tempChildren = new List<Transform>();


            for (int i = 0; i < transform.childCount; i++)
            {
                if (i >= _stacksToShowCount)
                    tempChildren.Add(transform.GetChild(i));
            }

            return tempChildren.ToArray();
        }

        Transform[] ChildrenToShow()
        {
            List<Transform> tempChildren = new List<Transform>();


            for (int i = 0; i < transform.childCount; i++)
            {
                if (i < _stacksToShowCount)
                    tempChildren.Add(transform.GetChild(i));
            }

            return tempChildren.ToArray();
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) StackChildrenCommand();
            if (methodNumb == 1) PlaceOnTopOfStackCommand();
            if (methodNumb == 2) PlaceOnBottomOfStackCommand();
            if (methodNumb == 3) StartStackLoopingCommand();
            if (methodNumb == 4) StopStackLoopingCommand();
            if (methodNumb == 5) MaskChildrenCommand();
            if (methodNumb == 6) StackAndMaskChildrenCommand();
        }

    }
}