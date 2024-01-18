using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
[TrackClipType(typeof(ManualAnimationPlayableAsset))]
[TrackBindingType(typeof(Animator))]
public class ManualAnimatorTrack : TrackAsset
{
    static T GetOrCreate<T>(GameObject go) where T : MonoBehaviour
    {
        T com = go.GetComponent<T>();
        if (com != null)
        {
            return com;
        }
        com = go.AddComponent<T>();
        return com;
    }

    public override IEnumerable<PlayableBinding> outputs
    {
        get { yield return AnimationPlayableBinding.Create(name, this); }
    }

    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        if (Application.isPlaying)
        {
            Animator animator = GetBinding(go != null ? go.GetComponent<PlayableDirector>() : null);
            if (null == animator)
            {
                return AnimationMixerPlayable.Create(graph, inputCount);
            }

            ManualAnimatorPlayable manualAnimatorPlayable = GetOrCreate<ManualAnimatorPlayable>(go);
            ManualAnimator manager = GetOrCreate<ManualAnimator>(animator.gameObject);
            return manualAnimatorPlayable.Init(inputCount, manager);
        }
        else
        {
            return AnimationMixerPlayable.Create(graph, inputCount);
        }

    }

    internal Animator GetBinding(PlayableDirector director)
    {
        if (director == null)
            return null;

        UnityEngine.Object key = this;
        if (isSubTrack)
            key = parent;

        UnityEngine.Object binding = null;
        if (director != null)
            binding = director.GetGenericBinding(key);

        Animator animator = null;
        if (binding != null) // the binding can be an animator or game object
        {
            animator = binding as Animator;
            var gameObject = binding as GameObject;
            if (animator == null && gameObject != null)
                animator = gameObject.GetComponent<Animator>();
        }

        return animator;
    }
}