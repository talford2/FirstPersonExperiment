using UnityEngine;
using System.Collections;

public class Drone : MonoBehaviour
{
	public BaseState<Drone> State { get; set; }

	public Transform Target;

	public float FindTargetRadius = 20f;

	public float ChaseSpeed = 2;

	public float SqrFindTargetRadius
	{
		get
		{
			return Mathf.Pow(FindTargetRadius, 2f);
		}
	}

	void Start()
	{
		State = new IdleState(this);
	}

	void Update()
	{
	}
}

public class ChaseState : BaseState<Drone>
{
	public ChaseState(Drone npc) : base(npc)
	{
		Debug.Log("Chase");
	}

	public override void Update()
	{
		if (NPC.Target != null)
		{
			NPC.transform.LookAt(NPC.Target);
			NPC.transform.position += NPC.transform.forward * NPC.ChaseSpeed * Time.deltaTime;
		}
		base.Update();
	}
}

public class IdleState : BaseState<Drone>
{
	public IdleState(Drone npc) : base(npc)
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

public abstract class BaseState<T>
{
	private T npc;

	public T NPC
	{
		get
		{
			return npc;
		}
	}

	public BaseState(T npc)
	{
		this.npc = npc;
	}

	public virtual void Update() { }
}