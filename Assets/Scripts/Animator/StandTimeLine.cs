using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class StandTimeLine : MonoBehaviour
{
    public PlayableDirector pd;

    public Animator attacker;
    public Animator victim;

    private void Awake()
    {
        pd = GetComponent<PlayableDirector>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            foreach (var temp in pd.playableAsset.outputs)
            {
                if (temp.streamName == "Attack")
                {
                    pd.SetGenericBinding(temp.sourceObject, attacker);
                }
                else if(temp.streamName=="Victim")
                {
                    pd.SetGenericBinding(temp.sourceObject, victim);
                }
            }

            pd.time = 0;
            pd.Stop();
            pd.Evaluate();
            pd.Play();
        }
    }
}
