using UnityEngine.Playables;
using UnityEngine;

public class TBehavior: PlayableBehaviour
{
    public override void OnGraphStart(Playable playable)
    {
        Debug.Log("OnGraphStart");
        base.OnGraphStart(playable);
    }

    public override void OnGraphStop(Playable playable)
    {
        Debug.Log("OnGraphStop");
        base.OnGraphStop(playable);
    }

    public override void OnPlayableCreate(Playable playable)
    {
        base.OnPlayableCreate(playable);
        Debug.Log("Create");
    }

    public override void OnPlayableDestroy(Playable playable)
    {
        base.OnPlayableDestroy(playable);
        Debug.Log("Destroy");
    }

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        base.OnBehaviourPlay(playable, info);
        Debug.Log("Play");
    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        Debug.Log("Pause");
        base.OnBehaviourPause(playable, info);

    }
    public override void PrepareFrame(Playable playable, FrameData info)
    {
        base.PrepareFrame(playable, info);
        Debug.Log("Update");

    }
}

