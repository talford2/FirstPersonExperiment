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

			if (NPC.Target != null)
				NPC.State = new DroneChase(NPC);
		}
		base.Update();
	}

	private IEnumerator GetNewTarget(float delay)
	{
		Debug.Log("Starting looking for dude");
		IsSearchingForTarget = true;
		yield return new WaitForSeconds(delay);
		NPC.Target = TargetingUtility.GetNearest(TargetingUtility.GetOpposingTeam(NPC.Team), NPC.transform.position, NPC.FindTargetRadius);
		IsSearchingForTarget = false;
		if (NPC.Target != null)
		{
			Debug.Log(NPC.name + " => " + NPC.Target.name);
		}
		else
		{
			Debug.Log("No target found");
		}
	}
}