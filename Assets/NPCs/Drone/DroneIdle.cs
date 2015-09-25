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
		    NPC.Target = TargetingUtility.GetNearest(NPC.Team, NPC.transform.position, NPC.SqrFindTargetRadius);
		    if (NPC.Target != null)
		        NPC.State = new DroneChase(NPC);
		}
		base.Update();
	}
}