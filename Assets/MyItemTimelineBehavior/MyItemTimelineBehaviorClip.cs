using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class MyItemTimelineBehaviorClip : PlayableAsset, ITimelineClipAsset
{
    public MyItemTimelineBehaviorBehaviour template = new MyItemTimelineBehaviorBehaviour ();
    public ExposedReference<Animator> anim;

    public ClipCaps clipCaps
    {
        get { return ClipCaps.None; }
    }

    public override Playable CreatePlayable (PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<MyItemTimelineBehaviorBehaviour>.Create (graph, template);
        MyItemTimelineBehaviorBehaviour clone = playable.GetBehaviour ();
        clone.anim = anim.Resolve (graph.GetResolver ());
        return playable;
    }
}
