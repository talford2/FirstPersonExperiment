using UnityEngine;

public class DroneChase : BaseState<Drone>
{
    private Vector3 destination;

	public DroneChase(Drone npc) : base(npc)
	{
		Debug.Log("Chase");
	    npc.MaxSpeed = npc.ChaseSpeed;
	}

    private Vector3 GetSteeringForce()
    {
        var sqrMaxSpeed = NPC.MaxSpeed*NPC.MaxSpeed;
        var steerForce = Vector3.zero;

        // Destination to 5m in front of target
        var toTarget = NPC.Target.position - NPC.transform.position;
        destination = NPC.Target.position - 5f * toTarget.normalized;

        /*
        steerForce += NPC.Steering.ObstaclesAvoidForce(NPC.Obstacles);
        if (steerForce.sqrMagnitude > sqrMaxSpeed)
            return steerForce.normalized*NPC.MaxSpeed;
        */

        steerForce += NPC.Steering.ArriveForce(destination);
        if (steerForce.sqrMagnitude > sqrMaxSpeed)
            return steerForce.normalized * NPC.MaxSpeed;

        steerForce += NPC.Steering.SeekForce(destination);
        if (steerForce.sqrMagnitude > sqrMaxSpeed)
            return steerForce.normalized * NPC.MaxSpeed;

        return steerForce;
    }
    
	public override void Update()
	{
		// Target is too far away lose target
		if (NPC.SqrTargetDistance >= NPC.SqrLoseTargetRadius)
		{
			NPC.Target = null;
			NPC.State = new DroneIdle(NPC);
		}

	    if (NPC.Target != null)
	    {
            NPC.Velocity += GetSteeringForce() * Time.deltaTime;
            var rBody = NPC.GetComponent<Rigidbody>();
            rBody.rotation = Quaternion.LookRotation(NPC.Target.position - NPC.transform.position);
            rBody.position += NPC.Velocity * Time.deltaTime;

	        if (NPC.SqrTargetDistance <= NPC.SqrAttackRadius)
	        {
	            NPC.State = new DroneAttack(NPC);
	        }
	    }

	    base.Update();
	}
}