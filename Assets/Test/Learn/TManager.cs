using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

class TManager:MonoBehaviour
{
    PlayableGraph m_graph;
    AnimationMixerPlayable m_mix;
    int index = 0;
    public PlayableGraph GetGraph()
    {
        if (!m_graph.IsValid())
        {
            m_graph = PlayableGraph.Create("TManager");
        }
        m_graph.Play();
        return m_graph;
    }

    public Playable Create(int inputCount)
    {
        if (m_mix.IsValid())
        {
            m_mix.Destroy();
        }
        var graph = GetGraph();
        var animationOutputPlayable = AnimationPlayableOutput.Create(graph, "AnimationOutput", GetComponent<Animator>());
        m_mix = AnimationMixerPlayable.Create(graph, inputCount);
        animationOutputPlayable.SetSourcePlayable(m_mix);
        return m_mix;
    }

    public Playable CreatePlayable(AnimationClip clip)
    {
        if (clip == null || clip.legacy)
            return Playable.Null;
        var graph = GetGraph();
        AnimationClipPlayable clipPlayable = AnimationClipPlayable.Create(graph, clip);
        m_mix.ConnectInput(index, clipPlayable, 0);
        index++;
        return clipPlayable;
    }

    public void DestroyGraph()
    {
        m_graph.Destroy();
    }

    private void OnDestroy()
    {
        m_graph.Destroy();
    }
}
