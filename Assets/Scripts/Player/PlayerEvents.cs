using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerEvents
{
    public delegate void PlayerEvent();
    public static PlayerEvent playerDeath;
    public static PlayerEvent hitAsteroid;
    public static PlayerEvent hitLaserCharge;
    public static PlayerEvent hitShield;
}
