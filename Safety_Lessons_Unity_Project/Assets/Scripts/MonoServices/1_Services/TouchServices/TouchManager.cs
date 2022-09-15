using MonoServices.Core;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MonoServices.DeviceTouches
{
    public class TouchManager : MonoService
    {
        [SerializeField] LayerMask _touchRayMask;


        readonly bool[] _touchStates = new bool[3];
        readonly float[] _touchStateValues = new float[3];

        float totalMovedDistance = 0;
        Camera cam;

        protected override void Start()
        {
            base.Start();

            cam = Camera.main;

            ActivateCoroutine(ListeningToTouches());
        }

        IEnumerator ListeningToTouches()
        {
            while (true)
            {
                ProcessTouches();

                yield return null;
            }
        }


        void ProcessTouches()
        {

            if (Input.touchCount == 0)
                return;

            if (TouchIsOnUi())
                return;

            OnFirstTouchPosCommand();
            GetSecondOnTouchDownPosCommand();
            ZoomValueCommand();
            OnVerticalFirstTouchSlideCommand();
            OnHorizontalFirstTouchSlideCommand();
            OnHorizontalSecondTouchSlideCommand();
            OnVerticalSecondTouchSlideCommand();
            EvaluateValues();
            OnFirstTouchUpCommand();
            OnSecondTouchUpCommand();
            OnFirstTouchDownCommand();
        }


        void OnFirstTouchPosCommand()
        {
            if (Input.touchCount != 1)
                return;

            if (Input.GetTouch(0).phase == TouchPhase.Began)
                totalMovedDistance = 0;

            totalMovedDistance += Input.GetTouch(0).deltaPosition.magnitude;

            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                if (totalMovedDistance < 7)
                {
                    InvokeCommand(0, Input.touches[0].position);
                    OnColliderHitCommand();
                }
            }
        }


        void GetSecondOnTouchDownPosCommand()
        {
            if (Input.touchCount != 2)
                return;

            if (Input.GetTouch(1).phase == TouchPhase.Began)
                totalMovedDistance = 0;

            totalMovedDistance += Input.GetTouch(1).deltaPosition.magnitude;

            if (Input.GetTouch(1).phase == TouchPhase.Ended)
            {
                if (totalMovedDistance < 7)
                {
                    InvokeCommand(1, Input.touches[1].position);
                    OnColliderHitCommand();
                }
            }
        }

        void ZoomValueCommand()
        {
            if (Input.touchCount < 2)
                return;

            Touch firstTouch = Input.GetTouch(0);
            Touch secondTouch = Input.GetTouch(1);

            var firstTouchPreviousPos = firstTouch.position - firstTouch.deltaPosition;
            var secondTouchPreviousPos = secondTouch.position - secondTouch.deltaPosition;

            var previousMagnitude = (firstTouchPreviousPos - secondTouchPreviousPos).magnitude;
            var currMagnitude = (firstTouch.position - secondTouch.position).magnitude;

            var zoomValue = currMagnitude - previousMagnitude;

            _touchStateValues[0] = zoomValue;

            if (_touchStates[0])
                InvokeCommand(2, _touchStateValues[0]);
        }

        void OnVerticalFirstTouchSlideCommand()
        {
            if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
                InvokeCommand(3, Input.GetTouch(0).deltaPosition.y);

        }

        void OnHorizontalFirstTouchSlideCommand()
        {
            if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
                InvokeCommand(4, Input.GetTouch(0).deltaPosition.x);

        }

        void OnHorizontalSecondTouchSlideCommand()
        {
            if (Input.touchCount == 2 && Input.GetTouch(1).phase == TouchPhase.Moved)
            {
                _touchStateValues[1] = Input.GetTouch(1).deltaPosition.x;

                if (_touchStates[1])
                    InvokeCommand(5, _touchStateValues[1]);
            }

        }

        void OnVerticalSecondTouchSlideCommand()
        {
            if (Input.touchCount == 2 && Input.GetTouch(1).phase == TouchPhase.Moved)
            {
                _touchStateValues[2] = Input.GetTouch(1).deltaPosition.y;

                if (_touchStates[2])
                    InvokeCommand(6, _touchStateValues[2]);
            }

        }


        void OnColliderHitCommand()
        {
            Ray ray = cam.ScreenPointToRay(Input.GetTouch(0).position);

            if (Physics.Raycast(ray, out RaycastHit raycastHit, _touchRayMask))
                InvokeCommand(7, raycastHit.collider);
        }


        void OnFirstTouchUpCommand()
        {
            if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)
                InvokeCommand(8);

        }

        void OnSecondTouchUpCommand()
        {
            if (Input.touchCount == 2 && Input.GetTouch(1).phase == TouchPhase.Ended)
                InvokeCommand(9);
        }

        void OnFirstTouchDownCommand()
        {
            if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
                OnTouchDownColliderHitCommand();
        }

        void OnTouchDownColliderHitCommand()
        {
            Ray ray = cam.ScreenPointToRay(Input.GetTouch(0).position);

            if (Physics.Raycast(ray, out RaycastHit raycastHit, _touchRayMask))
                InvokeCommand(11, raycastHit.collider);
        }


        void EvaluateValues()
        {
            float largestDeltaPos = 0;
            int largestDeltaIndex = 0;


            for (int i = 0; i < _touchStateValues.Length; i++)
            {
                var touchStateValue = Mathf.Abs(_touchStateValues[i]);

                if (touchStateValue > largestDeltaPos)
                {
                    largestDeltaPos = touchStateValue;
                    largestDeltaIndex = i;

                }
            }

            ChangeTocuhesStates(largestDeltaIndex);
        }

        void ChangeTocuhesStates(int touchIndexToTurnOn)
        {
            for (int i = 0; i < _touchStates.Length; i++)
                _touchStates[i] = i == touchIndexToTurnOn;
        }


        bool TouchIsOnUi()
        {

            bool touchIsOnUi = false;

            if (!EventSystem.current)
                return touchIsOnUi;


            var currEventSys = EventSystem.current;

            foreach (var touch in Input.touches)
            {
                if (currEventSys.IsPointerOverGameObject(touch.fingerId))
                {
                    touchIsOnUi = true;
                    break;
                }
            }

            return touchIsOnUi;

        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
        }

    }
}
