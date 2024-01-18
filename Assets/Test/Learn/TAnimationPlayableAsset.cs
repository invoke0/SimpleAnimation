using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

[System.Serializable]
public class TAnimationPlayableAsset : PlayableAsset
{
    [SerializeField] private AnimationClip m_Clip;
    [SerializeField] private bool m_ApplyFootIK = true;

    public bool applyFootIK
    {
        get { return m_ApplyFootIK; }
        set { m_ApplyFootIK = value; }
    }


    //public override IEnumerable<PlayableBinding> outputs
    //{
    //    get { yield return AnimationPlayableBinding.Create(name, this); }
    //}


    public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
    {
        Debug.Log("CreatePlayable" + graph.GetHashCode());

        Playable root = CreatePlayable(graph, m_Clip, applyFootIK);
        return root;
    }

    internal static Playable CreatePlayable(PlayableGraph graph, AnimationClip clip, bool applyFootIK)
    {
        if (clip == null || clip.legacy)
            return Playable.Null;

        AnimationClipPlayable clipPlayable = AnimationClipPlayable.Create(graph, clip);
        clipPlayable.SetApplyFootIK(applyFootIK);
        Playable root = clipPlayable;
        return root;
    }
}
