using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public PowerupIndicator PowerupIndicator;
    public GameObject RocketPrefab;
    
    private Rigidbody playerRb;
    private GameObject focalPoint;

    // Powerup
    private Powerup.PowerupTypes powerupType = Powerup.PowerupTypes.None;
    private float powerupLength = 7.0f;

    // Powerup: Power Push
    private float powerPushStrength = 15.0f;

    // Powerup: Rocket Attack
    private float rocketAttackDelay = 1f;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = FindObjectOfType<RotateCamera>().gameObject;
    }

    private void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);
        PowerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);

        if (transform.position.y < -2)
        {
            StartCoroutine(Gameover());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup") && powerupType == Powerup.PowerupTypes.None)
        {
            var powerup = other.gameObject.GetComponent<Powerup>();
            switch (powerup.PowerupType)
            {
                case Powerup.PowerupTypes.PowerPush:
                    powerupType = Powerup.PowerupTypes.PowerPush;
                    PowerupIndicator.PowerupType = Powerup.PowerupTypes.PowerPush;
                    break;
                case Powerup.PowerupTypes.RocketAttack:
                    powerupType = Powerup.PowerupTypes.RocketAttack;
                    PowerupIndicator.PowerupType = Powerup.PowerupTypes.RocketAttack;
                    StartCoroutine(RocketAttackRoutine());
                    break;
                default:
                    break;
            }
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && powerupType == Powerup.PowerupTypes.PowerPush)
        {
            var enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            var direction = (collision.gameObject.transform.position - transform.position);

            //Debug.LogFormat("Collided with {0} with powerup set to {1}", collision.gameObject.name, hasPowerup);
            enemyRb.AddForce(direction * powerPushStrength, ForceMode.Impulse);
        }
    }

    private IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(powerupLength);
        powerupType = Powerup.PowerupTypes.None;
        PowerupIndicator.PowerupType = Powerup.PowerupTypes.None;
    }

    private IEnumerator RocketAttackRoutine()
    {
        while (powerupType == Powerup.PowerupTypes.RocketAttack)
        {
            var launchPosition = transform.position + focalPoint.transform.forward;
            Instantiate(RocketPrefab, launchPosition, RocketPrefab.transform.rotation);
            yield return new WaitForSeconds(rocketAttackDelay);
        }
    }

    private IEnumerator Gameover()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(2);
    }
}
