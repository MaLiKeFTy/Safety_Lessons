using MonoServices.Core;

namespace MonoServices.Holders
{
    public class BoolHolder : MonoService
    {
        void ReceiveBoolCommand(bool boolToReceive)
        {
            if (boolToReceive)
                TrueCommand();
            else
                FalseCommand();
        }

        void TrueCommand()
        {
            InvokeCommand(1, true);
        }

        void FalseCommand()
        {
            InvokeCommand(2, false);
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) ReceiveBoolCommand((bool)passedObj);
        }
    }
}
