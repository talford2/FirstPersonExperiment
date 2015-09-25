using UnityEngine;
using System.Collections;

public abstract class BaseSteering<T>
{
    private readonly T npc;

    public T NPC
    {
        get
        {
            return npc;
        }
    }

    protected BaseSteering(T npc)
    {
        this.npc = npc;
    }
}
