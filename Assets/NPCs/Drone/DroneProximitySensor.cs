using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (SphereCollider))]
public class DroneProximitySensor : MonoBehaviour
{
    private Drone npc;
    private List<Collider> detectedColliders;

    private void Awake()
    {
        npc = GetComponentInParent<Drone>();
        detectedColliders = new List<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!detectedColliders.Contains(other))
        {
            // Avoid detecting self
            var otherNpc = other.GetComponentInParent<Drone>();
            if (otherNpc != null)
            {
                if (otherNpc == npc)
                    return;
            }
            detectedColliders.Add(other);
            npc.Obstacles.Add(other.transform.position);
            Debug.Log("DETECTED COUNT: " + detectedColliders.Count);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        detectedColliders.Remove(other);
        npc.Obstacles.Remove(other.transform.position);
        Debug.Log("DETECTED COUNT: " + detectedColliders.Count);
    }
}
