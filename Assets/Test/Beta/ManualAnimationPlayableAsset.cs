using System;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

[Serializable]
public class ManualAnimationPlayableAsset : PlayableAsset
{
    [SerializeField] private AnimationClip m_Clip;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
    {
        if (m_Clip == null || m_Clip.legacy)
            return Playable.Null;

        if (Application.isPlaying)
        {
            ManualAnimatorPlayable manualAnimatorPlayable = ManualAnimatorTrack.GetOrCreate<ManualAnimatorPlayable>(go);
            return manualAnimatorPlayable.CreatePlayable(graph.GetHashCode(), m_Clip);
        }
        else
        {
            AnimationClipPlayable clipPlayable = AnimationClipPlayable.Create(graph, m_Clip);
            Playable root = clipPlayable;
            return root;
        }

    }
}

