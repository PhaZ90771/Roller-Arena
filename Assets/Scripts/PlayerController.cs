using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public GameObject powerupIndicator;
    
    private Rigidbody playerRb;
    private GameObject focalPoint;

    private bool hasPowerup = false;
    private float powerupStrength = 15.0f;
    private float powerupLength = 7.0f;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = FindObjectOfType<RotateCamera>().gameObject;
    }

    private void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            powerupIndicator.gameObject.SetActive(true);
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            var enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            var direction = (collision.gameObject.transform.position - transform.position);

            //Debug.LogFormat("Collided with {0} with powerup set to {1}", collision.gameObject.name, hasPowerup);
            enemyRb.AddForce(direction * powerupStrength, ForceMode.Impulse);
        }
    }

    private IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(powerupLength);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }
}
