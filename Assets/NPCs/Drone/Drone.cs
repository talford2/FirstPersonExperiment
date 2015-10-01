using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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


	private IEnumerable<IObstacleAvoidance> obs;
	public IEnumerable<IObstacleAvoidance> Obstactles
	{
		get
		{
			if (obs == null)
			{
				var mask = LayerMask.GetMask("Obstacles");
				var r = Physics.OverlapSphere(transform.position, 300f, mask);
				Debug.Log("obstacles = " + r.Length);
				obs = r.Select(o => o.GetComponent<IObstacleAvoidance>());
			}
			return obs;
		}
	}

	#region State Speeds

	public float AttackCruiseSpeed = 0.5f;

	public float ChaseSpeed = 2f;

	public Vector3 Velocity { get; set; }
	public float MaxSpeed { get; set; }

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
		Obstactles.ToList().ForEach(o => o.ApplyForce(transform, 3f, 2.41f));
		State.Update();
	}

	private void OnDestroy()
	{
		TargetingUtility.RemoveTarget(team, transform);
	}

	#endregion
}