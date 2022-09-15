using MonoServices.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonoServices.Shaders
{
    public class DissolveEffectApplier : MonoService
    {

        [SerializeField] Shader _dissolveShader;
        [SerializeField] Shader _dissolveShaderTransparent;
        [SerializeField] bool _applyToChildren = true;
        [SerializeField] AnimationCurve _speedCurve = AnimationCurve.Linear(0, 0, 1, 1);
        [SerializeField, Range(0, 1)] float _moveSpeed = 0.2f;
        [SerializeField] Color _dissolveColor = Color.cyan;
        [SerializeField] bool _affectOtherDissolvers = true;
        [SerializeField] bool _dissolveOnStart;

        int currStartingDissolingValue = 99;
        readonly List<Shader> _previousShaders = new List<Shader>();

        protected override void Start()
        {
            base.Start();

            if (_dissolveOnStart)
                DisappearDissolveCommand();
        }

        void AppearDissolveCommand()
        {
            InvokeCommand(0);
            ApplyDissolveEffect(1);
        }

        void DisappearDissolveCommand()
        {
            StorePreviousShaders();
            ApplyDissolveEffect(0);
            InvokeCommand(1);
        }

        void OnFinishedAppearingCommand()
        {
            RestorePreviousRenders();

            InvokeCommand(2);
        }


        void OnFinishedDisappearingCommand() =>
            InvokeCommand(3);

        void ApplyDissolveEffect(int startingDissovlingValue)
        {
            if (currStartingDissolingValue == startingDissovlingValue)
                return;

            List<Material> childrenMats = new List<Material>();

            foreach (var childRenderer in RenderersToApply())
            {
                ChangeShaderToDissolve(childRenderer.material);

                ApplyTextureMaps(childRenderer.material);
                childRenderer.material.SetColor("_DissolveEdgeColor", _dissolveColor);
                childrenMats.Add(childRenderer.material);
            }

            currStartingDissolingValue = startingDissovlingValue;

            if (childrenMats.Count != 0)
                ActivateCoroutine(Dissolving(startingDissovlingValue, childrenMats.ToArray()));
        }

        void ChangeShaderToDissolve(Material mat)
        {
            if (mat.shader == _dissolveShader || mat.shader == _dissolveShaderTransparent)
                return;

            mat.shader = mat.GetFloat("_Surface") == 0 ? _dissolveShader : _dissolveShaderTransparent;
        }

        void ApplyTextureMaps(Material mat)
        {
            if (mat.GetTexture("_MetallicGlossMap")) mat.SetFloat("_MetallicBoolean", 1);
            if (mat.GetTexture("_BumpMap")) mat.SetFloat("_BumpBoolean", 1);
            if (mat.GetTexture("_EmissionMap")) mat.SetFloat("_EmissionBoolean", 1);
        }

        Renderer[] RenderersToApply()
        {
            List<Renderer> tempRenders = new List<Renderer>();

            List<Renderer> notAffectedRenders = new List<Renderer>();


            var childrenRenders = _applyToChildren ? GetComponentsInChildren<Renderer>() : GetComponents<Renderer>();

            if (_affectOtherDissolvers)
                return childrenRenders;

            foreach (var childRenderer in childrenRenders)
            {
                if (childRenderer.GetComponent<DissolveEffectApplier>())
                    foreach (var rendere in childRenderer.GetComponentsInChildren<Renderer>())
                        notAffectedRenders.Add(rendere);
            }

            foreach (var childRenderer in childrenRenders)
            {
                if (!notAffectedRenders.Contains(childRenderer))
                    tempRenders.Add(childRenderer);
            }

            return tempRenders.ToArray();
        }


        IEnumerator Dissolving(float startingDissovlingValue, Material[] mats)
        {
            float targetDissolveValue = startingDissovlingValue == 0 ? 1 : 0;

            float currentLerpTime = 0.2f;
            float targetLerpTime = 1;

            float currDissolveValue = mats[0].GetFloat("_Dissolve");

            while (currentLerpTime != targetLerpTime)
            {
                currentLerpTime = Mathf.MoveTowards(currentLerpTime, targetLerpTime, _moveSpeed * Time.deltaTime);

                float newDissolveValue = Mathf.Lerp(currDissolveValue, targetDissolveValue, _speedCurve.Evaluate(currentLerpTime));

                foreach (var mat in mats)
                    mat.SetFloat("_Dissolve", newDissolveValue);

                yield return null;
            }

            if (targetDissolveValue == 0)
                OnFinishedAppearingCommand();
            else
                OnFinishedDisappearingCommand();

            yield return null;
        }


        void StorePreviousShaders()
        {
            _previousShaders.Clear();

            foreach (var previousRenderer in GetComponentsInChildren<Renderer>())
                _previousShaders.Add(previousRenderer.material.shader);
        }

        void RestorePreviousRenders()
        {
            var currRenderes = GetComponentsInChildren<Renderer>();

            for (int i = 0; i < _previousShaders.Count; i++)
            {
                if (i >= currRenderes.Length)
                    break;

                var previousShader = _previousShaders[i];
                var currRenderer = currRenderes[i];

                currRenderer.material.shader = previousShader;
            }
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) AppearDissolveCommand();
            if (methodNumb == 1) DisappearDissolveCommand();

        }

    }

}