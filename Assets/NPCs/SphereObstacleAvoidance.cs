﻿using UnityEngine;
using System.Collections;

public class SphereObstacleAvoidance : MonoBehaviour, IObstacleAvoidance
{
	public float Radius = 1f;

	public float Falloff = 1f;

	public Vector3 Offset = Vector3.zero;

	public Vector3 FinalPosition
	{
		get
		{
			if (ControlTransform == null)
			{
				ControlTransform = transform;
			}
			return ControlTransform.position + Offset;
		}
	}

	public Transform ControlTransform;
	
	public void Awake()
	{
		if (ControlTransform == null)
		{
			ControlTransform = transform;
		}
	}

	public void ApplyForce(Transform transObj, float maxForce, float radius = 0)
	{
		var distSqr = (FinalPosition - transObj.position).sqrMagnitude;

		if (distSqr < Mathf.Pow(Radius + Falloff, 2f))
		{
			var dir = (transObj.position - FinalPosition).normalized;

			// object is inside the radius and must be placed on the edge
			if (distSqr < Radius * Radius)
			{
				transObj.position = FinalPosition + dir * Radius;
			}
			else
			{
				var f = Mathf.Pow(Radius + Falloff, 2f);
				var frac = (f - distSqr) / (f - Radius);
				
				//Debug.Log("frac = " + frac);
				transObj.position += dir * frac * maxForce * Time.deltaTime;
			}
		}
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(FinalPosition, Radius);
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = new Color(1, 0, 0, 0.3f);
		Gizmos.DrawSphere(FinalPosition, Radius);

		Gizmos.color = new Color(1, 0, 0, 0.8f);
		Gizmos.DrawWireSphere(FinalPosition, Radius + Falloff);
	}
}
