using UnityEngine;
using System.Collections;

public interface IObstacleAvoidence
{
	void ApplyForce(Transform transObj, float maxForce);
}
