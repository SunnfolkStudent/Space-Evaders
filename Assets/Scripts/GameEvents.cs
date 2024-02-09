using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvents
{
    public delegate void DestroyEvent(GameObject obj);
    public static DestroyEvent destroyAsteroid;
    public static DestroyEvent destroyLoot;
}
