using System;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

[Serializable]
public class TPlayableAsset : PlayableAsset
{
    //public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    //{
    //    var playable = ScriptPlayable<TBehavior>.Create(graph);
    //    return playable;
    //}

    [SerializeField] private AnimationClip m_Clip;
    [SerializeField] private bool m_ApplyFootIK = true;

    public bool applyFootIK
    {
        get { return m_ApplyFootIK; }
        set { m_ApplyFootIK = value; }
    }


    public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
    {
        Debug.Log("CreatePlayable" + graph.GetHashCode());

        if (m_Clip == null || m_Clip.legacy)
            return Playable.Null;

        if (Application.isPlaying)
        {
            TManager manager = TTrack.GetOrCreat<TManager>(go);
            return manager.CreatePlayable(m_Clip);
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

