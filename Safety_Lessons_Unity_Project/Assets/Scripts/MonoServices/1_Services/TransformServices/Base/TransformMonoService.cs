using MonoServices.Core;
using UnityEngine;

public abstract class TransformMonoService : MonoService
{
    protected Transform _ThisTransform;

    protected override void Start()
    {
        base.Start();

        _ThisTransform = transform;
    }
}
