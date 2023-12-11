using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]    //( •̀ ω •́ )✧
public class PracticeLightBehavior : PlayableBehaviour
{
    public ExposedReference<Light> lightObject;    //( •̀ ω •́ )✧
    private Light light;
    public Color color = Color.white;
    public float intensity = 1f;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        var graph = playable.GetGraph();
        light = lightObject.Resolve(graph.GetResolver());
        light.color = color;
        light.intensity = intensity;
    }
}
