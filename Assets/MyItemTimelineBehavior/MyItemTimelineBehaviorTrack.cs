using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackColor(0.04150939f, 0.5207547f, 1f)]
[TrackClipType(typeof(MyItemTimelineBehaviorClip))]
[TrackBindingType(typeof(Animator))]
public class MyItemTimelineBehaviorTrack : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        return ScriptPlayable<MyItemTimelineBehaviorMixerBehaviour>.Create (graph, inputCount);
    }
}
