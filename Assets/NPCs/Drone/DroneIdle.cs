using UnityEngine;
using System.Collections;

public class DroneIdle : BaseState<Drone>
{
	public DroneIdle(Drone npc) : base(npc)
	{
		Debug.Log("Idle");
	}

	public override void Update()
	{
		if (NPC.Target == null)
		{
			var playerDist = (Player.Current.transform.position - NPC.transform.position).sqrMagnitude;
			if (playerDist < NPC.SqrFindTargetRadius)
			{
				NPC.Target = Player.Current.transform;
				NPC.State = new ChaseState(NPC);
			}
		}
		base.Update();
	}
}