using UnityEngine;
using System.Collections;

public static class Helpers
{
	public static void DrawDebugPoint(Vector3 position, float size, Color color)
	{
		Debug.DrawLine(position + Vector3.up * size, position + Vector3.down * size, color);
		Debug.DrawLine(position + Vector3.left * size, position + Vector3.right * size, color);
		Debug.DrawLine(position + Vector3.forward * size, position + Vector3.back * size, color);
	}
}
