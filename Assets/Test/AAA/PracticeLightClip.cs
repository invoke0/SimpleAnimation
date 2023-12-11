using UnityEngine;
using UnityEngine.Playables;

public class PracticeLightClip : PlayableAsset
{
    public PracticeLightBehavior lightBehavior = new PracticeLightBehavior();    //( •̀ ω •́ )✧
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playableAsset = ScriptPlayable<PracticeLightBehavior>.Create(graph, lightBehavior);    //( •̀ ω •́ )✧
        return playableAsset;
    }
}
