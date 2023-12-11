using System;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;
[System.Serializable]
public class AnimationBlendPlayable : PlayableBehaviour
{
    [Range(0,1)]
    public float firstClipWeight;
    AnimationMixerPlayable m_mixerPlayable;
    PlayableGraph m_playableGraph;
    Playable m_fatherMixerPlayable;

    public AnimationClip clip1;
    public AnimationClip clip2;
    float m_firstClipLength, m_secondClipLength;

    public int index;

    public void SetIndex(int _index)
    {
        index = _index;
    }

    public override void OnPlayableCreate(Playable playable)
    {
        base.OnPlayableCreate(playable);

        //当该节点被创建的时候生成AnimationMixerPlayable节点，并且和自己连接。
        m_playableGraph = playable.GetGraph();
        m_mixerPlayable = AnimationMixerPlayable.Create(m_playableGraph, 2);

        var clip1Playable = AnimationClipPlayable.Create(m_playableGraph, clip1);
        var clip2Playable = AnimationClipPlayable.Create(m_playableGraph, clip2);

        m_mixerPlayable.ConnectInput(0, clip1Playable, 0);
        m_mixerPlayable.ConnectInput(1, clip2Playable, 0);
        m_firstClipLength = clip1.length;
        m_secondClipLength = clip2.length;
        clip1Playable.SetSpeed(m_firstClipLength);
        clip2Playable.SetSpeed(m_secondClipLength);

        playable.ConnectInput(0, m_mixerPlayable, 0);


    }

    //public override void PrepareFrame(Playable playable, FrameData info)
    //{
    //    base.PrepareFrame(playable, info);
    //    float secondClipWeight = 1.0f - firstClipWeight;
    //    m_mixerPlayable.SetInputWeight(0, firstClipWeight);
    //    m_mixerPlayable.SetInputWeight(1, secondClipWeight);

    //    //Debug.Log(firstClipWeight + ":" + secondClipWeight);
    //    Debug.Log("aa" + index);

    //    //float mixerPlayableSpeed = 1.0f / (firstClipWeight * m_firstClipLength + secondClipWeight * m_secondClipLength);
    //    //m_mixerPlayable.SetSpeed(mixerPlayableSpeed);
    //}


    public override void OnGraphStart(Playable playable)
    {
        base.OnGraphStart(playable);

        m_fatherMixerPlayable = playable.GetOutput(0);
        if (!m_fatherMixerPlayable.IsPlayableOfType<AnimationMixerPlayable>())
            Debug.LogError("Get AnimationMixerPlayable Error");

        //如果是第一个Clip，直接设置权重
        if (playable.Equals(m_fatherMixerPlayable.GetInput(0)))
            SetWeight();
    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        base.OnBehaviourPause(playable, info);

        //播放结束
        if (playable.GetTime() >= playable.GetDuration())
        {
            m_mixerPlayable.SetInputWeight(0, 0);
            m_mixerPlayable.SetInputWeight(1, 0);

            //设置下一个Clip权重
            for (int i = 0, count = m_fatherMixerPlayable.GetInputCount(); i < count - 1; i++)
            {
                if (playable.Equals(m_fatherMixerPlayable.GetInput(i)))
                {
                    ScriptPlayable<AnimationBlendPlayable> sp = (ScriptPlayable<AnimationBlendPlayable>)m_fatherMixerPlayable.GetInput(i + 1);
                    sp.GetBehaviour().SetWeight();
                    break;
                }
            }
        }
    }

    public void SetWeight()
    {
        float secondClipWeight = 1.0f - firstClipWeight;
        m_mixerPlayable.SetInputWeight(0, firstClipWeight);
        m_mixerPlayable.SetInputWeight(1, secondClipWeight);
        float mixerPlayableSpeed = 1.0f / (firstClipWeight * m_firstClipLength + secondClipWeight * m_secondClipLength);
        m_mixerPlayable.SetSpeed(mixerPlayableSpeed);
    }

}