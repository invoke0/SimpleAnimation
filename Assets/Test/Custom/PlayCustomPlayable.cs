using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

public class PlayCustomPlayable : MonoBehaviour
{
    PlayableGraph m_graph;
    public AnimationBlendPlayable m_blendPlayableBehaviour = new AnimationBlendPlayable();

    void Start()
    {
        m_graph = PlayableGraph.Create("ChanPlayableGraph");
        var animationOutputPlayable = AnimationPlayableOutput.Create(m_graph, "AnimationOutput", GetComponent<Animator>());
        var blendPlayable = ScriptPlayable<AnimationBlendPlayable>.Create(m_graph, m_blendPlayableBehaviour, 1);
        animationOutputPlayable.SetSourcePlayable(blendPlayable);

        m_blendPlayableBehaviour = blendPlayable.GetBehaviour();
        m_graph.Play();
    }


    void OnDestroy()
    {
        m_graph.Destroy();
    }
}