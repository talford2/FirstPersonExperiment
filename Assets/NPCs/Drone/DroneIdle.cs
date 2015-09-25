using UnityEngine;
using System.Collections;

public class DroneIdle : BaseState<Drone>
{
	private bool IsSearchingForTarget;

	public DroneIdle(Drone npc) : base(npc)
	{
		Debug.Log("Idle");
	}

	public override void Update()
	{
		if (NPC.Target == null && !IsSearchingForTarget)
		{
			NPC.StartCoroutine(GetNewTarget(1f));
		}
		base.Update();
	}

	private IEnumerator GetNewTarget(float delay)
	{
		Debug.Log("Starting looking for dude");
		IsSearchingForTarget = true;
		yield return new WaitForSeconds(delay);
		
		TargetingUtility.GetNearestCoroutine(TargetingUtility.GetOpposingTeam(NPC.Team), NPC.transform.position, NPC.FindTargetRadius, t =>
		{
			NPC.Target = t;
			if (t == null)
			{
				Debug.Log("No target found");
			}
			else
			{
				Debug.Log(NPC.name + " => " + NPC.Target.name);
				NPC.State = new DroneChase(NPC);
			}
		});

		IsSearchingForTarget = false;
	}
}