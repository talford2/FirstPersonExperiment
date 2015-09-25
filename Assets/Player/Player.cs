using UnityEngine;

public class Player : MonoBehaviour
{
    public int Team;

    private void Awake()
    {
        TargetingUtility.AddTarget(Team, transform);
    }

    private void Start()
    {
        current = this;
    }

    private void Update()
    {

    }

    private static Player current;

    public static Player Current
    {
        get { return current; }
    }

    private void OnDestroy()
    {
        TargetingUtility.RemoveTarget(Team, transform);
    }
}
