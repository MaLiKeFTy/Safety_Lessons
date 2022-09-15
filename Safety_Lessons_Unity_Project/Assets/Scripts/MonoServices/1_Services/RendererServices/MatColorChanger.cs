using MonoServices.Core;
using MonoServices.DataTypes.Converter;
using MonoServices.Holders;
using System.Collections;
using UnityEngine;

namespace MonoServices.Rendering
{
    public sealed class MatColorChanger : MatColorParametersHolder
    {
        [Tooltip("How fast can the colour change to the other colour")]
        [SerializeField, Range(1, 10)] float _moveSpeed = 8;

        [SerializeField] bool _changeOnStart;
        [SerializeField] bool _applyToChildren;

        protected override void Start()
        {
            base.Start();

            if (_changeOnStart)
                GetMatColorParam(_MatColorParams[0]);
        }

        protected override void GetMatColorParam(MatColorParamHolder matColorParam)
        {
            Renderer[] renderers = _applyToChildren ?
               GetComponentsInChildren<Renderer>() : GetComponents<Renderer>();

            foreach (var renderer in renderers)
                foreach (var shaderParam in matColorParam.ShaderParams)
                {
                    if (renderer is SpriteRenderer)
                        continue;

                    StartCoroutine(ColorChanging(renderer, matColorParam.Color, shaderParam));
                }
        }

        void ChangeColorWithMatCommand(Material matColorToChange)
        {
            Renderer[] renderers = _applyToChildren ?
              GetComponentsInChildren<Renderer>() : GetComponents<Renderer>();

            foreach (var renderer in renderers)
                StartCoroutine(MainColorChanging(renderer, matColorToChange));
        }

        IEnumerator ColorChanging(Renderer renderer, Color colorToChange, string materialColorParamater)
        {
            Vector4 colorToChangeQ = DataTypeConverter.FromColorToVec4(colorToChange, true);

            if(!renderer.material.HasColor(materialColorParamater))
                yield break;

            Color currColor = renderer.material.GetColor(materialColorParamater);

            Vector4 currColorQ = DataTypeConverter.FromColorToVec4(currColor, true);

            while (Vector4.Distance(currColorQ, colorToChangeQ) > 0.01f)
            {
                currColor = Color.Lerp(currColor, colorToChange, _moveSpeed * Time.deltaTime);
                currColorQ = DataTypeConverter.FromColorToVec4(currColor, true);

                renderer.material.SetColor(materialColorParamater, currColor);

                yield return null;
            }

            renderer.material.SetColor(materialColorParamater, currColor);
            yield return null;

        }

        IEnumerator MainColorChanging(Renderer renderer, Material matColorToChange)
        {
            Vector4 colorToChangeQ = DataTypeConverter.FromColorToVec4(matColorToChange.color, true);
            Color currColor = renderer.material.color;

            Vector4 currColorQ = DataTypeConverter.FromColorToVec4(currColor, true);

            while (Vector4.Distance(currColorQ, colorToChangeQ) > 0.01f)
            {
                currColor = Color.Lerp(currColor, matColorToChange.color, _moveSpeed * Time.deltaTime);
                currColorQ = DataTypeConverter.FromColorToVec4(currColor, true);

                renderer.material.color = currColor;

                yield return null;
            }

            renderer.material.color = currColor;
            yield return null;

        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            base.ReceiveCommands(invokedMonoService, methodNumb, passedObj);

            if (methodNumb == _MatColorParams.Length) ChangeColorWithMatCommand((Material)passedObj);

        }
    }
}