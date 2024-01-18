using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;


public class ManualAnimator : MonoBehaviour
{
    private PlayableGraph m_graph;
    private AnimationMixerPlayable m_mixRoot;
    private float speed;


    private int index = 0;
    public PlayableGraph GetPlayableGraph()
    {
        if (!m_graph.IsValid())
        {
            m_graph = PlayableGraph.Create("ManualAnimator");
            var animationOutputPlayable = AnimationPlayableOutput.Create(m_graph, "AnimationOutput", GetComponent<Animator>());
            m_mixRoot = AnimationMixerPlayable.Create(m_graph, 2);
            animationOutputPlayable.SetSourcePlayable(m_mixRoot);
            m_graph.Play();

            index = 0;
        }
        return m_graph;
    }

    public void Connect(Playable playable, bool mix, float mixTime)
    {
        Playable oldPlayable = m_mixRoot.GetInput(index);
        if (!oldPlayable.IsNull())
        {
            m_mixRoot.DisconnectInput(index);
        }

        int lastIndex = 0;
        if (0 == index)
        {
            lastIndex = 1;
        }
        Playable lastPlayable = m_mixRoot.GetInput(lastIndex);
        m_mixRoot.ConnectInput(index, playable, 0);
        if (!lastPlayable.IsNull() && mix)
        {
            StopAllCoroutines();
            StartCoroutine(CoroutineFunc(index, lastIndex, mixTime));
        }
        else
        {
            m_mixRoot.SetInputWeight(index, 1);
            m_mixRoot.SetInputWeight(lastIndex, 0);
        }
        index = 0 == index ? 1 : 0;
    }

    private IEnumerator CoroutineFunc(int index, int lastIndex, float duration)
    {
        m_mixRoot.SetInputWeight(index, 0);
        m_mixRoot.SetInputWeight(lastIndex, 1);
        float timer = 0;
        while (timer < duration)
        {
            float t = Mathf.Clamp01(timer / duration);
            timer += Time.deltaTime;
            m_mixRoot.SetInputWeight(index, t);
            m_mixRoot.SetInputWeight(lastIndex, 1 - t);
            yield return null;
        }
        m_mixRoot.SetInputWeight(index, 1);
        m_mixRoot.SetInputWeight(lastIndex, 0);
    }


    private void OnDestroy()
    {
        m_graph.Destroy();
    }
}
