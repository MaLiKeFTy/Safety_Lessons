using MonoServices.Core;
using MonoServices.ObjectsSO;
using UnityEngine;

namespace MonoServices.Holders
{
    public class ObjHolderChildrenPopulator : MonoService
    {

        [SerializeField] GameobjsSO _gameobjsSO;


        void ChangeObjSoCommand(GameobjsSO gameobjsSO)
        {
            _gameobjsSO = gameobjsSO;
        }


        void PopulateGameObjsInChildrenCommand()
        {
            var gameObjHolderChildren = GetComponentsInChildren<MultiGameObjHolder>();

            for (int i = 0; i < gameObjHolderChildren.Length; i++)
            {

                var gameObjHolder = gameObjHolderChildren[i];

                if (_gameobjsSO.Objs.Length > i)
                    gameObjHolder.GameObjToHold = _gameobjsSO.Objs[i].GameoObjs;

            }
        }


        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) ChangeObjSoCommand((GameobjsSO)passedObj);
            if (methodNumb == 1) PopulateGameObjsInChildrenCommand();
        }


    }
}