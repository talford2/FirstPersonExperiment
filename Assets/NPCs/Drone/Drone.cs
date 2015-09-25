﻿using UnityEngine;
using System.Collections;

public class Drone : MonoBehaviour
{
	public BaseState<Drone> State { get; set; }

	public Transform Target;

	public float TargetDistanceSqr
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
		Debug.Log("Drone Start");
		State = new DroneIdle(this);
	}

	void Update()
	{
		State.Update();
	}
}