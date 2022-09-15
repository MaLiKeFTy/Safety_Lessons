using MonoServices.Core;
using MonoServices.ObjectsSO;
using UnityEngine;

namespace MonoServices.MeshTexts
{
    public class MeshTextChildrenTextPopulator : MonoService
    {
        [SerializeField] MeshTextInfoHolder[] _meshTextInfoHolders;

        void ChangeTextListCommand(StringsSO textList)
        {
            InvokeCommand(0, textList);
        }

        void PopulateMeshTextInChildrenCommand()
        {
            var meshTextChangers = GetComponentsInChildren<MeshTextChanger>();

            for (int i = 0; i < meshTextChangers.Length; i++)
            {
                var meshTextChanger = meshTextChangers[i];

                foreach (var meshTextInfoHolder in _meshTextInfoHolders)
                {
                    var sameTag = meshTextChanger.MonoServiceParams.MonoServiceTag == meshTextInfoHolder.MeshChangerTag;
                    var indexInRange = meshTextInfoHolder.TextList.Objs.Length > i;

                    if (sameTag && indexInRange)
                        meshTextChanger.TextToChange = meshTextInfoHolder.TextList.Objs[i];
                }
            }

            OnFinishedPopulatingCommand();
        }

        void OnFinishedPopulatingCommand()
        {
            InvokeCommand(2);
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) ChangeTextListCommand((StringsSO)passedObj);
            if (methodNumb == 1) PopulateMeshTextInChildrenCommand();
        }
    }
}
