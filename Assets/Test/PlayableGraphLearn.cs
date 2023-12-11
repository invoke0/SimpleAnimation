using System;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

class PlayableGraphLearn:MonoBehaviour
{
    public Animator animator
    {
        get
        {
            if (m_Animator == null)
            {
                m_Animator = GetComponent<Animator>();
            }
            return m_Animator;
        }
    }

    private Animator m_Animator;
    public AnimationClip walkClip;
    public AnimationClip runClip;

    public PlayableGraph graph;
    public AnimationMixerPlayable mixerPlayable;
    public AnimationClipPlayable walkClipPlayable;
    public AnimationClipPlayable runClipPlayable;


    private void Start()
    {
        graph = PlayableGraph.Create("PlayableGraphLearn");

        // out put
        AnimationPlayableOutput animationOutputPlayable = AnimationPlayableOutput.Create(graph, "AnimationOutput", animator);

        // clip
        walkClipPlayable = AnimationClipPlayable.Create(graph, walkClip);
        runClipPlayable = AnimationClipPlayable.Create(graph, runClip);


        mixerPlayable = AnimationMixerPlayable.Create(graph, 2);

        graph.Connect(runClipPlayable, 0, mixerPlayable, 0);
        //graph.Connect(walkClipPlayable, 0, mixerPlayable, 1);

        animationOutputPlayable.SetSourcePlayable(mixerPlayable);

        graph.Play();
    }
    [Range(0, 1)] public float speed;

    private void Update()
    {
        mixerPlayable.SetInputWeight(0, 1.0f - speed);
        mixerPlayable.SetInputWeight(1, speed);

        if (Input.GetKeyDown(KeyCode.A))
        {
            walkClipPlayable.Pause();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            walkClipPlayable.Play();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            graph.Disconnect(mixerPlayable, 1);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            graph.Connect(walkClipPlayable, 0, mixerPlayable, 1);
        }
    }

    private void OnDestroy()
    {
        graph.Destroy();
    }
}

