using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[RequireComponent(typeof(PlayableDirector))]
public class DirectiorManager : IActorManager
{
    PlayableDirector pd;


    [Header("=========Play======")]
    public TimelineAsset play;
    public TimelineAsset openBox;
    public TimelineAsset leverUp;


    public ActorManager attacker;
    public ActorManager victimer;


    private void Awake()
    {
        pd = GetComponent<PlayableDirector>();

        pd.playOnAwake = false;
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.H) && gameObject.layer == LayerMask.NameToLayer("Player"))
        //{
        //    pd.Play();
        //}
    }

    public bool IsPlaying()
    {
        return pd.state == PlayState.Playing;
    }


    public void PlaySpecialTimeLine(string name, ActorManager attack, ActorManager victim)
    {

        if (name == "FrontStab")
        {
            pd.playableAsset = Instantiate(play);

            TimelineAsset timelineAsset = (TimelineAsset)pd.playableAsset;

            foreach (var track in timelineAsset.GetOutputTracks())
            {
                if (track.name == "Attack")
                {
                    pd.SetGenericBinding(track, attack.ac.Anim);
                }
                else if (track.name == "Victim")
                {
                    pd.SetGenericBinding(track, victimer.ac.Anim);
                }
                else if (track.name == "AttackScript")
                {
                    pd.SetGenericBinding(track, attacker);
                    foreach (var clip in track.GetClips())
                    {
                        MySuperPlayableClip myClip = (MySuperPlayableClip)clip.asset;
                        MySuperPlayableBehaviour myClipBehavior = myClip.template;
                        myClipBehavior.Speed = 520f;
                        myClip.am.exposedName = System.Guid.NewGuid().ToString();
                        pd.SetReferenceValue(myClip.am.exposedName, attacker);   //将某实例化对象的内存地址作为键，设置引用类型

                        //myClipBehavior.Camera = GameObject.Find("A");  //error
                    }
                }
                else if (track.name == "VictimScript")
                {
                    pd.SetGenericBinding(track, victimer);
                    foreach (var clip in track.GetClips())
                    {
                        MySuperPlayableClip myClip = (MySuperPlayableClip)clip.asset;
                        MySuperPlayableBehaviour myClipBehavior = myClip.template;
                        myClipBehavior.Speed = 999f;
                        myClip.am.exposedName = System.Guid.NewGuid().ToString();
                        pd.SetReferenceValue(myClip.am.exposedName, victimer);

                        //myClipBehavior.Camera = GameObject.Find("A");  //error
                    }
                }
            }

            pd.Evaluate();

            pd.Play();

        }

        //if (name == "FrontStab")
        //{
        //    pd.playableAsset = Instantiate(play);

        //    foreach (var track in pd.playableAsset.outputs)
        //    {
        //        if (track.streamName == "Attack")
        //        {
        //            pd.SetGenericBinding(track.sourceObject, attacker.ac.Anim);
        //        }
        //        else if (track.streamName == "Victim")
        //        {
        //            pd.SetGenericBinding(track.sourceObject, victimer.ac.Anim);
        //        }
        //        else if (track.streamName == "AttackScript")
        //        {
        //            pd.SetGenericBinding(track.sourceObject, attacker);
        //        }
        //        else if (track.streamName == "VictimScript")
        //        {
        //            pd.SetGenericBinding(track.sourceObject, victimer);
        //        }
        //    }

        //    pd.Play();
        //}
    }

    public void PlaySpecialTimeLine(string name, ActorManager attack,Animator anim)
    {
        if (pd.state == PlayState.Playing) return;

        if (name == "OpenBox")
        {
            pd.playableAsset = Instantiate(openBox);

            TimelineAsset timelineAsset = (TimelineAsset)pd.playableAsset;

            foreach (var track in timelineAsset.GetOutputTracks())
            {
                if (track.name == "Attack")
                {
                    pd.SetGenericBinding(track, attack.ac.Anim);
                }
                else if (track.name == "Box")
                {
                    pd.SetGenericBinding(track, anim);
                }
                else if (track.name == "AttackScript")
                {
                    pd.SetGenericBinding(track, attacker);
                    foreach (var clip in track.GetClips())
                    {
                        MySuperPlayableClip myClip = (MySuperPlayableClip)clip.asset;
                        MySuperPlayableBehaviour myClipBehavior = myClip.template;

                        myClip.am.exposedName = System.Guid.NewGuid().ToString();
                        pd.SetReferenceValue(myClip.am.exposedName, attacker);   //将某实例化对象的内存地址作为键，设置引用类型

                        //myClipBehavior.Camera = GameObject.Find("A");  //error
                    }
                }
                
            }

            pd.Evaluate();

            pd.Play();

        }
        else if (name == "LeverUp")
        {
            pd.playableAsset = Instantiate(leverUp);

            TimelineAsset timelineAsset = (TimelineAsset)pd.playableAsset;

            foreach (var track in timelineAsset.GetOutputTracks())
            {
                if (track.name == "Attack")
                {
                    pd.SetGenericBinding(track, attack.ac.Anim);
                }
                else if (track.name == "Lever")
                {
                    pd.SetGenericBinding(track, anim);
                }
                else if (track.name == "AttackScript")
                {
                    pd.SetGenericBinding(track, attacker);
                    foreach (var clip in track.GetClips())
                    {
                        MySuperPlayableClip myClip = (MySuperPlayableClip)clip.asset;
                        MySuperPlayableBehaviour myClipBehavior = myClip.template;

                        myClip.am.exposedName = System.Guid.NewGuid().ToString();
                        pd.SetReferenceValue(myClip.am.exposedName, attacker);   //将某实例化对象的内存地址作为键，设置引用类型

                        //myClipBehavior.Camera = GameObject.Find("A");  //error
                    }
                }
                else if (track.name == "LeverScript")
                {
                    pd.SetGenericBinding(track, anim);
                    foreach (var clip in track.GetClips())
                    {
                        MyItemTimelineBehaviorClip myclip = (MyItemTimelineBehaviorClip)clip.asset;
                        MyItemTimelineBehaviorBehaviour myClipBeahavior = myclip.template;

                        myclip.anim.exposedName = System.Guid.NewGuid().ToString();
                        pd.SetReferenceValue(myclip.anim.exposedName, anim);
                    }
                }
            }

            pd.Evaluate();

            pd.Play();
        }

    }
}
