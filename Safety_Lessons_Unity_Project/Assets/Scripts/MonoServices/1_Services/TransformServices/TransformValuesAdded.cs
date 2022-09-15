using MonoServices.Core;
using UnityEngine;

namespace MonoServices.Transforms
{
    public class TransformValuesAdded : MonoService
    {

        [SerializeField] float moveSpeed = 10;
        [SerializeField] bool posRelativeToCam;

        Camera _cam;


        protected override void Awake()
        {
            base.Awake();

            _cam = Camera.main;
        }

        void AddToPositionCommand(Vector3 posToAdd)
        {
            transform.position += moveSpeed * Time.deltaTime * RetativeToCamera(posToAdd);
        }

        void AddToEulerAngleCommand(Vector3 eularAngleToAdd)
        {
            transform.eulerAngles += moveSpeed * Time.deltaTime * eularAngleToAdd;
        }

        void AddToScaleCommand(Vector3 scaleToAdd)
        {
            transform.localScale += moveSpeed * Time.deltaTime * scaleToAdd;
        }


        void AddToRightPositionCommand(float valueToAdd)
        {

            var posToAdd = Vector3.right * valueToAdd;


            transform.position += moveSpeed * Time.deltaTime * RetativeToCamera(posToAdd);
        }

        void AddToUpPositionCommand(float valueToAdd)
        {
            var posToAdd = Vector3.up * valueToAdd;

            transform.position += moveSpeed * Time.deltaTime * posToAdd;
        }

        void AddToForwardPositionCommand(float valueToAdd)
        {

            var posToAdd = Vector3.forward * valueToAdd;

            transform.position += moveSpeed * Time.deltaTime * RetativeToCamera(posToAdd);
        }




        void AddToUpEulerAngleCommand(float valueToAdd)
        {

            var eularAngleToAdd = new Vector3(0, -valueToAdd, 0);

            transform.eulerAngles += moveSpeed * Time.deltaTime * eularAngleToAdd;
        }



        Vector3 RetativeToCamera(Vector3 vectorToAdd)
        {
            var forwardCamCord = _cam.transform.forward.normalized;
            var rightCamCord = _cam.transform.right.normalized;

            float horisontalAxis = vectorToAdd.z;
            float verticalAxis = vectorToAdd.x;

            Vector3 vectorToSend = forwardCamCord * horisontalAxis + rightCamCord * verticalAxis;

            vectorToSend.y = vectorToAdd.y;

            return vectorToSend;
        }


        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {

            if (passedObj == null)
                return;

            Vector3 vectorToPass = passedObj is float passedFloat ?
                new Vector3(passedFloat, passedFloat, passedFloat) :
                (Vector3)passedObj;

            if (methodNumb == 0) AddToPositionCommand(vectorToPass);
            if (methodNumb == 1) AddToEulerAngleCommand(vectorToPass);
            if (methodNumb == 2) AddToScaleCommand(vectorToPass);
            if (methodNumb == 3) AddToRightPositionCommand((float)passedObj);
            if (methodNumb == 4) AddToUpPositionCommand((float)passedObj);
            if (methodNumb == 5) AddToForwardPositionCommand((float)passedObj);
            if (methodNumb == 6) AddToUpEulerAngleCommand((float)passedObj);
        }
    }
}