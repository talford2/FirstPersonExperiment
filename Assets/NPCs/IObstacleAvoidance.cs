using UnityEngine;
using System.Collections;

public interface IObstacleAvoidance
{
	void ApplyForce(Transform transObj, float maxForce = 1, float radius = 0f);
}
