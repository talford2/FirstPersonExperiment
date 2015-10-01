using UnityEngine;
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
			return transform.position + Offset;
		}
	}

	public void ApplyForce(Transform transObj, float maxForce)
	{
		var d = (FinalPosition - transObj.position).magnitude;

		if (d < (Radius + Falloff))
		{
			var dir = (transObj.position - FinalPosition).normalized;

			// object is inside the radius and must be placed on the edge
			if (d < Radius)
			{
				transObj.position = dir * Radius;
			}
			else
			{
				var f = Radius + Falloff;
				var frac = 1 - (f - d) / (f - Radius);
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
