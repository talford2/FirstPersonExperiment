using UnityEngine;
using System.Collections;

public class DroneAttack : BaseState<Drone>
{
	public DroneAttack(Drone npc) : base(npc)
	{
		Debug.Log("Attack");
	}

	public override void Update()
	{
		NPC.transform.LookAt(NPC.Target);
		
		base.Update();
	}
}