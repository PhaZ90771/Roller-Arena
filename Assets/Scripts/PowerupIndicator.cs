using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupIndicator : MonoBehaviour
{
    public GameObject PowerPushIndicator;
    public GameObject RocketAttackIndicator;

    public Powerup.PowerupTypes PowerupType = Powerup.PowerupTypes.None;

    private void Update()
    {
        switch (PowerupType)
        {
            case Powerup.PowerupTypes.PowerPush:
                PowerPushIndicator.gameObject.SetActive(true);
                RocketAttackIndicator.gameObject.SetActive(false);
                break;
            case Powerup.PowerupTypes.RocketAttack:
                PowerPushIndicator.gameObject.SetActive(false);
                RocketAttackIndicator.gameObject.SetActive(true);
                break;
            default: 
                PowerPushIndicator.gameObject.SetActive(false);
                RocketAttackIndicator.gameObject.SetActive(false);
                break;
        }
    }
}