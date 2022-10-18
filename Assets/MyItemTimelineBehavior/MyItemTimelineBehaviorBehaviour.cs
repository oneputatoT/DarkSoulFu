using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class MyItemTimelineBehaviorBehaviour : PlayableBehaviour
{
    public Animator anim;

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        anim.SetBool("lock", false);
    }

    public override void PrepareFrame(Playable playable, FrameData info)
    {
        anim.SetBool("lock", true);
    }
}
