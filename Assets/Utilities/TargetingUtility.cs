﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class TargetingUtility
{
    private static Dictionary<int, List<Transform>> targets;

    public static void AddTarget(int team, Transform transform)
    {
        if (targets == null)
            targets = new Dictionary<int, List<Transform>>();
        if (!targets.ContainsKey(team))
            targets.Add(team, new List<Transform>());
        targets[team].Add(transform);
    }

    public static void RemoveTarget(int team, Transform transform)
    {
        targets[team].Remove(transform);
    }

    public static int GetOpposingTeam(int team)
    {
        if (team == 1)
            return 2;
        return 1;
    }

    public static Transform GetNearest(int team, Vector3 position, float maxDistance)
    {
        if (!targets.ContainsKey(team) || !targets[team].Any())
            return null;
        var sqrMinDistance = Mathf.Pow(maxDistance, 2f);
        Transform closestTarget = null;
        foreach (var candidate in targets[team])
        {
            var toCandidateSqr = (candidate.position - position).sqrMagnitude;
            if (toCandidateSqr < sqrMinDistance)
            {
                sqrMinDistance = toCandidateSqr;
                closestTarget = candidate;
            }
        }
        return closestTarget;
    }
}
