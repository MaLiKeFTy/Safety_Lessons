using MonoServices.Core;

namespace MonoServices.DataTypes.Changers
{
    public class BoolChanger : MonoService
    {
        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) ChangeToOffCommand();
            if (methodNumb == 1) ChangeToOnCommand();
        }

        void ChangeToOffCommand()
        {
            InvokeCommand(0, false);
            GetChangedBoolCommand(false);
        }

        void ChangeToOnCommand()
        {
            InvokeCommand(1, true);
            GetChangedBoolCommand(true);
        }

        void GetChangedBoolCommand(bool changedBool) =>
            InvokeCommand(2, changedBool);

    }
}
