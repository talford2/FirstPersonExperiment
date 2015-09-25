﻿using UnityEngine;
using System.Collections;

public class DroneChase : BaseState<Drone>
{
	public DroneChase(Drone npc) : base(npc)
	{
		Debug.Log("Chase");
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
			NPC.transform.position += NPC.transform.forward * NPC.ChaseSpeed * Time.deltaTime;

			if (NPC.SqrTargetDistance <= NPC.SqrAttackRadius)
			{
				NPC.State = new DroneAttack(NPC);
			}
		}

		base.Update();
	}
}