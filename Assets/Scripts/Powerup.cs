using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public PowerupTypes PowerupType;

    private GameObject focalPoint;


    private void Awake()
    {
        focalPoint = FindObjectOfType<RotateCamera>().gameObject;
    }

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(-focalPoint.transform.forward);
    }
    public enum PowerupTypes
    {
        None,
        PowerPush,
        RocketAttack,
        SmashAttack,
    }
}