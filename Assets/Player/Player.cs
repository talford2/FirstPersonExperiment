using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	void Start()
	{
		current = this;
	}

	void Update()
	{

	}

	private static Player current;
	public static Player Current
	{
		get
		{
			return current;
		}
	}
}
