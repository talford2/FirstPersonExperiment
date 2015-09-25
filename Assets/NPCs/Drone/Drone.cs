using UnityEngine;
using System.Collections;

public class Drone : MonoBehaviour
{
	public BaseState<Drone> State { get; set; }

	public Transform Target;
	
	public float FindTargetRadius = 20f;

	public float LoseTargetRadius = 30f;

	public float AttackRadius = 5f;

	public float ChaseSpeed = 2f;

	public float SqrTargetDistance
	{
		get
		{
			if (Target == null)
			{
				return -1;
			}
			return (Target.transform.position - transform.position).sqrMagnitude;
		}
	}

	public float SqrFindTargetRadius
	{
		get
		{
			return Mathf.Pow(FindTargetRadius, 2f);
		}
	}

	public float SqrLoseTargetRadius
	{
		get
		{
			return Mathf.Pow(LoseTargetRadius, 2f);
		}
	}

	public float SqrAttackRadius
	{
		get
		{
			return Mathf.Pow(AttackRadius, 2f);
		}
	}

	void Start()
	{
		Debug.Log("Drone Start");
		State = new DroneIdle(this);
	}

	void Update()
	{
		State.Update();
	}
}