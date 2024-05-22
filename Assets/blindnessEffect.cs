
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class blindnessEffect : MonoBehaviour
{

    private PostProcessVolume fxVolume;
    Vignette vignet;

    [SerializeField, Range(1,10)]
    private int effectDuration;
    [SerializeField, Range(0.2f, 1)]
    private float vignetIntensity;

    private void Start()
    {
        vignet = ScriptableObject.CreateInstance<Vignette>();
        vignet.enabled.Override(true);
        vignet.intensity.Override(0f);

        fxVolume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, vignet);
        //blindEffect();
    }
    private void Update()
    {
        //vignet.intensity.value = Mathf.Sin(Time.realtimeSinceStartup);
    }


    public void blindEffect()
    {
        StartCoroutine(VignetEffectOn());
    }
    private void OnDestroy()
    {
        RuntimeUtilities.DestroyVolume(fxVolume,true, true);
    }


    private IEnumerator VignetEffectOn()
    {
        float elapedTime = 0;
        float duration = 1.5f;
        while (elapedTime < duration)
        {

            
            float t = elapedTime / duration;

            vignet.intensity.value = Mathf.Lerp(0, vignetIntensity, t);

            elapedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        
        yield return new WaitForSeconds(effectDuration);
        VignetEffectOff();
    }

    public IEnumerator VignetEffectOff()
    {
        float elapedTime = 0;
        float duration = 1.5f;

        while (elapedTime < duration)
        {
            float t = elapedTime / duration;

            vignet.intensity.value = Mathf.Lerp(vignetIntensity, 0, t);

            elapedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }
}



