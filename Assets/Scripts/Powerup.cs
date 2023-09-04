using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public PowerupTypes PowerupType;

    public enum PowerupTypes
    {
        None,
        PowerPush,
        RocketAttack
    }
}
