using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miasma : MonoBehaviour
{
    private ParticleSystemRenderer _particle;
    private float duration = 2f;
    void Start()
    {
        _particle = GetComponent<ParticleSystemRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void Dissolve()
    {
        _particle.material.SetFloat("DissolveValue", 0);
    }
    IEnumerator ChangeValueOverTime()
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            //float currentValue = Mathf.Lerp(startValue, targetValue, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        yield return null;
    }
}
