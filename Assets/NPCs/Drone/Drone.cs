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
		State = new DroneIdle(this);
	}

	void Update()
	{
	}
}