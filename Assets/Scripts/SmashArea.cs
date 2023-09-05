using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashArea : MonoBehaviour
{
    public float SmashAttackStrength = 1f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            var diff = other.transform.position - transform.position;
            var direction = diff.normalized;
            var inverseMagnitude = diff.magnitude;
            var enemyRb = other.gameObject.GetComponent<Rigidbody>();
            enemyRb.AddForce(direction * SmashAttackStrength / inverseMagnitude, ForceMode.Impulse);
        }
    }
}
