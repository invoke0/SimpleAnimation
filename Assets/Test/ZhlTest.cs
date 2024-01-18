using System;
using UnityEngine;
using UnityEngine.Playables;

class ZhlTest:MonoBehaviour
{
    public GameObject obj;
    public Animator anim;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            var go = GameObject.Instantiate(obj);
            PlayableDirector director = go.GetComponent<PlayableDirector>(); 
            Debug.Log("CreateGameObject");
            //director.RebuildGraph();

        }
    }
}

