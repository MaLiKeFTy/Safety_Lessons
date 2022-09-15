using UnityEngine;

namespace MonoServices.Holders
{
    public class AnimationHolder : ObjectHolder<AnimationClip>
    {
        protected override void SetHolderText()
        {
            base.SetHolderText();

            _holderText.text = _heldObject.name;
        }
    }
}