using MonoServices.Core;
using UnityEngine;

namespace MonoServices.GameObjs
{
    public class GameobjectComparer : MonoService
    {

        [SerializeField] bool _compareChildren;
        [SerializeField] bool _callOnceEveryCheck;


        bool _isSameObj;
        bool _isDiffObj;


        void CompareGameobjectCommand(GameObject[] gamesObjToCompare)
        {

            foreach (var gameObjToCompare in gamesObjToCompare)
            {
                bool isChild = _compareChildren && gameObjToCompare.transform.IsChildOf(transform);

                if (gameObject == gameObjToCompare || isChild)
                    OnSameObjectCommand();
                else
                    OnDifferentObjectCommand();
            }

            InvokeCommand(0);
        }

        void OnSameObjectCommand()
        {
            if (!_callOnceEveryCheck)
                InvokeCommand(1);
            else
                OnSameCheckOnce();
        }

        void OnDifferentObjectCommand()
        {
            if (!_callOnceEveryCheck)
                InvokeCommand(2);
            else
                OnDiffrentCheckOnce();
        }

        void OnSameCheckOnce()
        {
            if (_isSameObj)
                return;

            _isSameObj = true;
            _isDiffObj = false;

            InvokeCommand(1);
        }

        void OnDiffrentCheckOnce()
        {
            if (_isDiffObj)
                return;

            _isSameObj = false;
            _isDiffObj = true;

            InvokeCommand(2);
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (passedObj is Collider collider) CompareGameobjectCommand(new GameObject[] { collider.gameObject });
            if (passedObj is GameObject gameObj) CompareGameobjectCommand(new GameObject[] { gameObj });
            if (passedObj is GameObject[] gameObjs) CompareGameobjectCommand(gameObjs);
        }

    }
}