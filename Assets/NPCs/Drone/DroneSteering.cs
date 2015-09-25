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

    public Vector3 SeekForce(Vector3 position)
    {
        var toPosition = position - npc.transform.position;
        return toPosition;
    }

    public DroneSteering(Drone npc) : base(npc)
    {
        this.npc = npc;
    }
}
