using UnityEngine;

public class DroneSteering : BaseSteering<Drone>
{
    private readonly Drone npc;

    public Vector3 ObstaclesAvoidForce()
    {
        var checkRay = new Ray(npc.transform.position, npc.Velocity);
        RaycastHit obstacleHit;
        if (Physics.SphereCast(checkRay, 0.8f, out obstacleHit, npc.MaxSpeed, LayerMask.GetMask("Detect")))
        {
            var desiredVelocity = npc.MaxSpeed*obstacleHit.normal*(1f + 1.6f)/obstacleHit.distance;
            return desiredVelocity - npc.Velocity;
        }
        return Vector3.zero;
    }

    public Vector3 SeekForce(Vector3 position)
    {
        var desiredVelocity = (position - npc.transform.position).normalized*npc.MaxSpeed;
        return desiredVelocity - npc.Velocity;
    }

    public Vector3 ArriveForce(Vector3 position)
    {
        var toPosition = position - npc.transform.position;
        var distance = toPosition.magnitude;
        var deceleration = 0.6f;
        if (distance > 0f)
        {
            var speed = distance/deceleration;
            speed = Mathf.Min(speed, npc.MaxSpeed);
            var desiredVelocity = toPosition*speed/distance;
            return desiredVelocity - npc.Velocity;
        }
        return Vector3.zero;
    }

    public DroneSteering(Drone npc) : base(npc)
    {
        this.npc = npc;
    }
}
