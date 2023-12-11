using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;


public class ManualAnimator : MonoBehaviour
{
    private PlayableGraph m_graph;
    private AnimationMixerPlayable m_mixRoot;

    public PlayableGraph GetPlayableGraph()
    {
        if (!m_graph.IsValid())
        {
            m_graph = PlayableGraph.Create("ManualAnimator");
            var animationOutputPlayable = AnimationPlayableOutput.Create(m_graph, "AnimationOutput", GetComponent<Animator>());
            m_mixRoot = AnimationMixerPlayable.Create(m_graph, 2);
            animationOutputPlayable.SetSourcePlayable(m_mixRoot);
            m_graph.Play();
        }
        return m_graph;
    }

    public void Connect(Playable playable, int test)
    {
        m_mixRoot.DisconnectInput(0);
        m_mixRoot.ConnectInput(test, playable, 0);
        m_mixRoot.SetInputWeight(0, 1);
    }

    private void OnDestroy()
    {
        m_graph.Destroy();
    }
}
