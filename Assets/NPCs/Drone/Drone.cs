using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Targetable))]
public class Drone : MonoBehaviour
{
	private int team;

	public BaseState<Drone> State { get; set; }

	public MuzzleFlash Muzzle;

	public int Team
	{
		get
		{
			return team;
		}
	}

	public Transform Target;

	public AudioSource ShootSound;

	#region State Speeds

	public float AttackCruiseSpeed = 0.5f;

	public float ChaseSpeed = 2f;

	#endregion

	#region Radius Distances

	public float FindTargetRadius = 20f;

	public float LoseTargetRadius = 30f;

	public float AttackRadius = 5f;

	public float AttackStopRadius = 3f;

	#endregion

	#region Radius Distances Squared

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
		get { return Mathf.Pow(FindTargetRadius, 2f); }
	}

	public float SqrLoseTargetRadius
	{
		get { return Mathf.Pow(LoseTargetRadius, 2f); }
	}

	public float SqrAttackRadius
	{
		get { return Mathf.Pow(AttackRadius, 2f); }
	}

	public float SqrAttackStopRadius
	{
		get { return Mathf.Pow(AttackStopRadius, 2); }
	}

	#endregion

	#region Detected Objects

	public List<Vector3> Obstacles { get; set; }

	#endregion

	public DroneSteering Steering;

	#region Private Methods

	private void Awake()
	{
		team = GetComponent<Targetable>().Team;
		TargetingUtility.AddTarget(team, transform);
		Obstacles = new List<Vector3>();
		Steering = new DroneSteering(this);
	}

	private void Start()
	{
		Debug.Log("Drone Start");
		State = new DroneIdle(this);
	}

	private void Update()
	{
		State.Update();
	}

	private void OnDestroy()
	{
		TargetingUtility.RemoveTarget(team, transform);
	}

	#endregion
}