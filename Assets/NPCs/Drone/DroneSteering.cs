using System.Collections.Generic;
using UnityEngine;

public class DroneSteering : BaseSteering<Drone>
{
    private readonly Drone npc;

    public Vector3 ObstaclesAvoidForce(IEnumerable<Vector3> obstacles)
    {
        var avoidSum = Vector3.zero;
        /*
        foreach (var obstacle in obstacles)
        {
            var fromObstacle = npc.transform.position - obstacle;
            avoidSum += fromObstacle.normalized/fromObstacle.magnitude;
        }
        */
        return avoidSum;
    }

    public Vector3 SeekForce(Vector3 position)
    {
        var desiredVelocity = (position - npc.transform.position).normalized*npc.MaxSpeed;
        return desiredVelocity - npc.Velocity;
    }

    public Vector3 ArriveForce(Vector3 position)
    {
        var toPosition = npc.transform.position - position;
        var distance = toPosition.magnitude;
        var deceleration = 0.6f;
        if (distance > 0f)
        {
            var speed = distance/deceleration;
            speed = Mathf.Min(speed, npc.MaxSpeed);
            var desiredVelocity = toPosition*speed/distance;
            return desiredVelocity- npc.Velocity;
        }
        return Vector3.zero;
    }

    public DroneSteering(Drone npc) : base(npc)
    {
        this.npc = npc;
    }
}
