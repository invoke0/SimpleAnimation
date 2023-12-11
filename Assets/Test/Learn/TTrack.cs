using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
[TrackClipType(typeof(TPlayableAsset))]
[TrackBindingType(typeof(Animator))]
public class TTrack : TrackAsset
{
    public static T GetOrCreat<T>(GameObject go) where T : MonoBehaviour
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
        Debug.Log("CreateTrackMixer" + graph.GetHashCode());
        if (Application.isPlaying)
        {
            TManager manager = TTrack.GetOrCreat<TManager>(go);
            return manager.Create(inputCount);
        }
        else
        {
            var mixer = AnimationMixerPlayable.Create(graph, inputCount);
            return mixer;
        }

    }
}