using System;
using System.Collections.Generic;
using UnityEngine.Animations;

using UnityEngine.Playables;
using UnityEngine.Serialization;

#if UNITY_EDITOR
using UnityEditor;
using UnityEngine.Timeline;
using UnityEngine;
#endif


[Serializable]
[TrackClipType(typeof(TAnimationPlayableAsset), false)]
[TrackBindingType(typeof(Animator))]
//[ExcludeFromPreset]
public class TAnimationTrack : TrackAsset
{
    public override IEnumerable<PlayableBinding> outputs
    {
        get {
            yield return AnimationPlayableBinding.Create(name, this); 
        }
    }

    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        Debug.Log("CreateTrackMixer" + graph.GetHashCode());

        var mixer  = AnimationMixerPlayable.Create(graph, inputCount);
        return mixer;
    }

    //internal Animator GetBinding(PlayableDirector director)
    //{
    //    if (director == null)
    //        return null;

    //    UnityEngine.Object key = this;
    //    if (isSubTrack)
    //        key = parent;

    //    UnityEngine.Object binding = null;
    //    if (director != null)
    //        binding = director.GetGenericBinding(key);

    //    Animator animator = null;
    //    if (binding != null) // the binding can be an animator or game object
    //    {
    //        animator = binding as Animator;
    //        var gameObject = binding as GameObject;
    //        if (animator == null && gameObject != null)
    //            animator = gameObject.GetComponent<Animator>();
    //    }

    //    return animator;
    //}
}
