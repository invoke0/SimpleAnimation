using System;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;
using static UnityEngine.Timeline.AnimationPlayableAsset;

[Serializable]
public class ManualAnimationPlayableAsset : PlayableAsset
{
    [SerializeField] private AnimationClip m_Clip;
    [SerializeField] private bool m_ApplyFootIK = true;
    public bool applyFootIK
    {
        get { return m_ApplyFootIK; }
        set { m_ApplyFootIK = value; }
    }

    public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
    {
        if (m_Clip == null || m_Clip.legacy)
            return Playable.Null;

        ManualAnimatorPlayable manualAnimatorPlayable = go.GetComponent<ManualAnimatorPlayable>();
        if (Application.isPlaying && null != manualAnimatorPlayable)
        {
            AnimationClipPlayable clipPlayable = manualAnimatorPlayable.CreatePlayable(graph.GetHashCode(), m_Clip);
            clipPlayable.SetApplyFootIK(applyFootIK);
            return clipPlayable;
        }
        else
        {
            AnimationClipPlayable clipPlayable = AnimationClipPlayable.Create(graph, m_Clip);
            clipPlayable.SetApplyFootIK(applyFootIK);
            Playable root = clipPlayable;
            return root;
        }

    }
}

