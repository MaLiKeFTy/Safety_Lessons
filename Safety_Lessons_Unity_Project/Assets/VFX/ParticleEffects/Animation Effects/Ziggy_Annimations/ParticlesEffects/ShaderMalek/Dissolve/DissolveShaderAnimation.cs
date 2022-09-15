using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveShaderAnimation : MonoBehaviour
{
    [SerializeField] private Material materialShader;
    [SerializeField] private float speed;
    IEnumerator dissolvingCorotine;

    void Start()
	{
        materialShader.SetFloat("_Dissolve", 1);
    }

    /*
	void Update()
    {
        if (Input.GetMouseButtonDown(0))
            ActivateDissolve(-0.1f);

        else if (Input.GetMouseButtonDown(1))
            ActivateDissolve(1f);
    }
    */
    public void ActivateDissolve(float target)
    {
        if (dissolvingCorotine != null)
            StopCoroutine(dissolvingCorotine);

        dissolvingCorotine = Dissolving(target);
        StartCoroutine(dissolvingCorotine);
    }

    IEnumerator Dissolving(float target)
    {
        while (Mathf.Abs(materialShader.GetFloat("_Dissolve") - target) > 0.03f)
        {
            materialShader.SetFloat("_Dissolve", Mathf.Lerp(materialShader.GetFloat("_Dissolve"), target, speed * Time.deltaTime));
            yield return null;
        }

        yield return null;
        materialShader.SetFloat("_Dissolve", target);

    }
}