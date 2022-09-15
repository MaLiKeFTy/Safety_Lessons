using MonoServices.Core;
using UnityEngine;

namespace MonoServices.MonoUI
{
    public sealed class UiPositionDistanceComparer : UiMonoService
    {
        Vector3 _pointToReturn;

        void CompareClosestDistanceCommand(Vector3[] points)
        {
            float perviousDistanceAmount = float.MaxValue;

            foreach (var point in points)
            {
                if (Vector2.Distance(_ThisRectTransform.anchoredPosition, point) < perviousDistanceAmount)
                {
                    _pointToReturn = point;
                    perviousDistanceAmount = Vector3.Distance(_ThisRectTransform.anchoredPosition, point);
                }
            }

            GetPointCommand();
        }

        void CompareFurtherstDistanceCommand(Vector3[] points)
        {
            float perviousDistanceAmount = 0;

            foreach (var point in points)
            {
                if (Vector3.Distance(_ThisRectTransform.anchoredPosition, point) > perviousDistanceAmount)
                {
                    _pointToReturn = point;
                    perviousDistanceAmount = Vector3.Distance(_ThisRectTransform.anchoredPosition, point);
                }
            }

            GetPointCommand();
        }

        void GetPointCommand() =>
            InvokeCommand(2, _pointToReturn);

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) CompareClosestDistanceCommand((Vector3[])passedObj);
            if (methodNumb == 1) CompareFurtherstDistanceCommand((Vector3[])passedObj);
        }
    }
}
