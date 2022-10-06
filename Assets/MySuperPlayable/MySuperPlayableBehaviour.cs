using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class MySuperPlayableBehaviour : PlayableBehaviour
{
    public GameObject Camera;
    public float Speed;
    PlayableDirector pd;

    public override void OnPlayableCreate (Playable playable)
    {
        
    }

    public override void OnGraphStart(Playable playable)
    {
        pd = (PlayableDirector)playable.GetGraph().GetResolver();      //先从该片段去获取轨道，轨道获取导演
        foreach (var track in pd.playableAsset.outputs)
        {
            if (track.streamName == "AttackScript"|| track.streamName == "VictimScript")
            {
                ActorManager am = (ActorManager)pd.GetGenericBinding(track.sourceObject);
                am.LockAnimation("lock", true);
            }
        }
    }

    public override void OnGraphStop(Playable playable)
    {
        foreach (var track in pd.playableAsset.outputs)
        {
            if (track.streamName == "AttackScript"||track.streamName== "VictimScript")
            {
                ActorManager am = (ActorManager)pd.GetGenericBinding(track.sourceObject);
                am.LockAnimation("lock", false);
            }
        }
    }

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
       
    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        
    }

    public override void PrepareFrame(Playable playable, FrameData info)
    {
        
    }
}
