using UnityEngine;

public class DroneChase : BaseState<Drone>
{
	public DroneChase(Drone npc) : base(npc)
	{
		Debug.Log("Chase");
	}

    private Vector3 GetSteeringForce()
    {
        var steerForce = Vector3.zero;

        steerForce += NPC.Steering.ObstaclesAvoidForce(NPC.Obstacles);

        steerForce += 0.1f*NPC.Steering.SeekForce(NPC.Target.position);

        return steerForce.normalized;
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
			NPC.transform.LookAt(NPC.Target);
		    NPC.transform.position += GetSteeringForce()*NPC.ChaseSpeed*Time.deltaTime;
			//NPC.transform.position += NPC.transform.forward * NPC.ChaseSpeed * Time.deltaTime;

			if (NPC.SqrTargetDistance <= NPC.SqrAttackRadius)
			{
				NPC.State = new DroneAttack(NPC);
			}
		}

		base.Update();
	}
}