using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashArea : MonoBehaviour
{
    public float SmashAttackStrength = 30.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            var direction = (other.transform.position - transform.position).normalized;
            var enemyRb = other.gameObject.GetComponent<Rigidbody>();
            enemyRb.AddForce(direction * SmashAttackStrength, ForceMode.Impulse);
        }
    }
}
