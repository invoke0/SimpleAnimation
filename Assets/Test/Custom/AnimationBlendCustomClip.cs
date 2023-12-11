using UnityEngine;
using UnityEngine.Playables;
public class AnimationBlendCustomClip : PlayableAsset
{
    public AnimationBlendPlayable m_blendPlayableBehaviour = new AnimationBlendPlayable();
    public int index = 1;
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var blendPlayable = ScriptPlayable<AnimationBlendPlayable>.Create(graph, m_blendPlayableBehaviour, 1);
        var behaivor = blendPlayable.GetBehaviour();
        behaivor.SetIndex(index);
        return blendPlayable;
    }
}