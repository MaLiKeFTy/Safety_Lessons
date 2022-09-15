using MonoServices.Core;

namespace MonoServices.GameObjs
{
    public class ParentGameObjectGetter : GameobjectMonoService
    {
        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj) =>
            GetParentGameObjectObjectCommand();

        void GetParentGameObjectObjectCommand() =>
            InvokeCommand(0, gameObject);
    }
}