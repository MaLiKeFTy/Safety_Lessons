using UnityEngine;
using System.Collections;
using MonoServices.Core;

namespace MonoServices.MonoPhysics
{
    [RequireComponent(typeof(Joint))]
    public class JointConnectedBodySetter : MonoService
    {
        Joint _joint;
        Rigidbody _bodyToConnect;

        protected override void Awake()
        {
            base.Awake();

            _joint = GetComponent<Joint>();
        }


        void GetBodyToConnectCommand(Rigidbody bodyToConnect)
        {
            _bodyToConnect = bodyToConnect;

            GetConnectedBodyCommand(0);
        }

        void SetBodyToConnectCommand()
        {
            _joint.connectedBody = _bodyToConnect;

            ActivateCoroutine(ConnectingBody());

            GetConnectedBodyCommand(1);

            GetTransConnectedBodyCommand();
        }

        void GetAndSetBodyToConnectCommand(Rigidbody bodyToConnect)
        {
            GetBodyToConnectCommand(bodyToConnect);
            SetBodyToConnectCommand();

            GetConnectedBodyCommand(2);
        }

        void DisconnectBodyCommand()
        {
            _joint.connectedBody = null;

            GetConnectedBodyCommand(3);
        }

        void GetConnectedBodyCommand(int MethodNumb)
        {
            InvokeCommand(MethodNumb, _bodyToConnect);
        }

        void GetTransConnectedBodyCommand()
        {
            InvokeCommand(5, _bodyToConnect.transform);
        }

        void OnConnectedBodyCommand() =>
            InvokeCommand(6, _bodyToConnect.transform);

        IEnumerator ConnectingBody()
        {
            var waitForSeconds = new WaitForSeconds(0.1f);

            while (_joint.connectedBody)
            {
                OnConnectedBodyCommand();

                yield return waitForSeconds;
            }
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            Rigidbody rbToset;
            rbToset = passedObj is Transform passedTrans ?
                passedTrans.GetComponent<Rigidbody>() : (Rigidbody)passedObj;

            if (methodNumb == 0) GetBodyToConnectCommand(rbToset);
            if (methodNumb == 1) SetBodyToConnectCommand();
            if (methodNumb == 2) GetAndSetBodyToConnectCommand(rbToset);
            if (methodNumb == 3) DisconnectBodyCommand();
        }
    }
}
