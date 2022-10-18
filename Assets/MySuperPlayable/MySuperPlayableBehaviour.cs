using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class MySuperPlayableBehaviour : PlayableBehaviour
{
    public ActorManager am;
    public float Speed;


    public override void OnPlayableCreate (Playable playable)
    {
        
    }

    public override void OnGraphStart(Playable playable)
    {
        //pd = (PlayableDirector)playable.GetGraph().GetResolver();      //�ȴӸ�Ƭ��ȥ��ȡ����������ȡ����
        //foreach (var track in pd.playableAsset.outputs)
        //{
        //    if (track.streamName == "AttackScript"|| track.streamName == "VictimScript")
        //    {
        //        ActorManager am = (ActorManager)pd.GetGenericBinding(track.sourceObject);
        //        am.LockAnimation(true);
        //    }
        //}
    }

    public override void OnGraphStop(Playable playable)
    {
        //foreach (var track in pd.playableAsset.outputs)
        //{
        //    if (track.streamName == "AttackScript"||track.streamName== "VictimScript")
        //    {
        //        ActorManager am = (ActorManager)pd.GetGenericBinding(track.sourceObject);
        //        am.LockAnimation(false);
        //    }
        //}
    }

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        
    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
            am.LockAnimation(false);
    }

    public override void PrepareFrame(Playable playable, FrameData info)
    { 
            am.LockAnimation(true);
    }
}
