using System.Collections.Generic;
using UnityEngine;

public class DroneSteering : BaseSteering<Drone>
{
    private readonly Drone npc;

    public Vector3 ObstaclesAvoidForce(IEnumerable<Vector3> obstacles)
    {
        var avoidSum = Vector3.zero;
        foreach (var obstacle in obstacles)
        {
            var fromObstacle = npc.transform.position - obstacle;
            avoidSum += fromObstacle.normalized/fromObstacle.magnitude;
        }
        return avoidSum;
    }

    public DroneSteering(Drone npc) : base(npc)
    {
        this.npc = npc;
    }
}
