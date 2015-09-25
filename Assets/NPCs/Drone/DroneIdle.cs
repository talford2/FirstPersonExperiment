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
			if (NPC.TargetDistanceSqr <= NPC.SqrFindTargetRadius)
			{
				NPC.Target = Player.Current.transform;
				NPC.State = new DroneChase(NPC);
			}
		}
		base.Update();
	}
}