using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;


public class ManualAnimatorPlayable : MonoBehaviour
{
    private ManualAnimator m_manualAnimator;
    private AnimationMixerPlayable m_mixer;
    private int index = 0;

    public int test = 0;

    public Playable Init(int inputCount, ManualAnimator manualAnimator)
    {
        m_manualAnimator = manualAnimator;
        if (!m_mixer.IsNull())
        {
            m_mixer.Destroy();
        }
        PlayableGraph graph = m_manualAnimator.GetPlayableGraph();
        m_mixer = AnimationMixerPlayable.Create(graph, inputCount);
        m_manualAnimator.Connect(m_mixer, test);
        index = 0;
        return m_mixer;
    }

    public Playable CreatePlayable(int graphHashCode, AnimationClip clip)
    {
        if (clip == null || clip.legacy)
        {
            return Playable.Null;
        }
        PlayableGraph graph = m_manualAnimator.GetPlayableGraph();
        AnimationClipPlayable clipPlayable = AnimationClipPlayable.Create(graph, clip);
        m_mixer.ConnectInput(index, clipPlayable, 0);
        index++; 
        return clipPlayable;
    }

    private void OnDestroy()
    {
        m_mixer.Destroy();
    }
}