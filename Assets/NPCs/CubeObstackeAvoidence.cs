using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public class CubeObstackeAvoidence : MonoBehaviour, IObstacleAvoidance
{
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

	public void ApplyForce(Transform transObj, float maxForce = 1, float radius = 0)
	{
		//radius = 0;
		//Debug.Log("Cube hit!");

		var b = new Bounds(FinalPosition, transform.localScale);
		b.Expand(radius * 2);

		// Why does radius need to be multiplied by 2?
		//var falloffBounds =new Bounds( new Bounds(FinalPosition, transform.localScale + (Vector3.one * Falloff) + (Vector3.one * radius * 2));

		var falloffBounds = new Bounds(FinalPosition, transform.localScale);
		falloffBounds.Expand(radius * 2 + Falloff * 2);
		if (falloffBounds.Contains(transObj.position))
		{
			//Debug.Log("Object in falloff");

			// inside the obstacle!
			if (b.Contains(transObj.position))
			{
				//Debug.Log("Object has enter bounds!");

				transObj.position = GetClosestBorderFromInsidePoint(b, transObj.position, 0.01f);
			}
			// in the fallout area
			else
			{
				var falloutClosePoint = falloffBounds.ClosestPoint(transObj.position);
				Helpers.DrawDebugPoint(falloutClosePoint, 1, Color.cyan);

				var objClosePoint = b.ClosestPoint(transObj.position);
				Helpers.DrawDebugPoint(objClosePoint, 1, Color.magenta);

				Debug.DrawLine(falloutClosePoint, objClosePoint, Color.yellow);

				var mag = (falloutClosePoint - objClosePoint).magnitude;

				var frac = Mathf.Max(1 - mag / Falloff, 0);

				//Debug.Log("mag = " + mag + " (" + frac + ")");

				var forceVector = (falloutClosePoint - objClosePoint).normalized * frac * maxForce;
				transObj.position += forceVector * Time.deltaTime;
			}
		}
	}

	private Vector3 GetClosestBorderFromInsidePoint(Bounds bound, Vector3 point, float outerSkin)
	{
		var edgePoints = new List<Vector3>();

		edgePoints.Add(new Vector3(FinalPosition.x + bound.size.x / 2 + outerSkin, point.y, point.z));
		edgePoints.Add(new Vector3(FinalPosition.x - bound.size.x / 2 - outerSkin, point.y, point.z));

		edgePoints.Add(new Vector3(point.x, FinalPosition.y + bound.size.y / 2 + outerSkin, point.z));
		edgePoints.Add(new Vector3(point.x, FinalPosition.y - bound.size.y / 2 - outerSkin, point.z));

		edgePoints.Add(new Vector3(point.x, point.y, FinalPosition.z + bound.size.z / 2 + outerSkin));
		edgePoints.Add(new Vector3(point.x, point.y, FinalPosition.z - bound.size.z / 2 - outerSkin));

		var edgePoint = Vector3.zero;
		var mag = float.MaxValue;

		foreach (var p in edgePoints)
		{
			var curMag = (p - point).sqrMagnitude;
			if (curMag < mag)
			{
				edgePoint = p;
				//Debug.Log(i + ". iii = " + f);
				mag = curMag;
			}
			//Helpers.DrawDebugPoint(p, 1, Color.green);
		}
		//Helpers.DrawDebugPoint(f, 1, Color.yellow);
		return edgePoint;
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(FinalPosition, transform.localScale);
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = new Color(1, 0, 0, 0.3f);
		Gizmos.DrawCube(FinalPosition, transform.localScale);

		Gizmos.color = new Color(1, 0, 0, 0.8f);
		Gizmos.DrawWireCube(FinalPosition, transform.localScale + (new Vector3(1, 1, 1) * Falloff * 2));
	}
}
