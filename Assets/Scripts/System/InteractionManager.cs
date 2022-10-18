using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : IActorManager
{
    CapsuleCollider col;

    public List<EventCasterManager> eventCasterList;

    private void Awake()
    {
        col = GetComponent<CapsuleCollider>();
    }

    private void OnEnable()
    {
        eventCasterList = new List<EventCasterManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        EventCasterManager[] temp = other.GetComponents<EventCasterManager>();
        foreach (var eventData in temp)
        {
            if (!eventCasterList.Contains(eventData))
            {
                eventCasterList.Add(eventData);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        EventCasterManager[] temp = other.GetComponents<EventCasterManager>();
        foreach (var eventData in temp)
        {
            if (eventCasterList.Contains(eventData))
            {
                eventCasterList.Remove(eventData);
            }
        }
    }
}
