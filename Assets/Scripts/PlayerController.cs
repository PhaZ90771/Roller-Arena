using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float Speed;
    public PowerupIndicator PowerupIndicator;
    public GameObject RocketPrefab;
    public GameObject SmashArea;
    
    private Rigidbody playerRb;
    private GameObject focalPoint;

    private bool isGrounded = false;
    private bool wasGrounded = false;

    // Powerup
    private Powerup.PowerupTypes powerupType = Powerup.PowerupTypes.None;
    private float powerupLength = 7.0f;

    // Powerup: Power Push
    private float powerPushStrength = 15.0f;

    // Powerup: Rocket Attack
    private float rocketAttackDelay = 1f;

    // Powerup: Smash Attack
    private float smashAttackSpeedUpwards = 3.0f;
    private SmashStates smashState = SmashStates.None;
    private float smashLength = 0.1f;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = FindObjectOfType<RotateCamera>().gameObject;
    }

    private void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * Speed * forwardInput);
        PowerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
        SmashArea.transform.position = transform.position + new Vector3(0, -0.6f, 0);

        if (transform.position.y < -2)
        {
            StartCoroutine(Gameover());
        }

        if (powerupType == Powerup.PowerupTypes.SmashAttack && Input.GetKeyDown(KeyCode.Space))
        {
            playerRb.AddForce(focalPoint.transform.up * smashAttackSpeedUpwards, ForceMode.Impulse);
            smashState = SmashStates.Preparing;
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
                case Powerup.PowerupTypes.SmashAttack:
                    powerupType = Powerup.PowerupTypes.SmashAttack;
                    PowerupIndicator.PowerupType = Powerup.PowerupTypes.SmashAttack;
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
        else if (collision.gameObject.CompareTag("Ground"))
        {
            wasGrounded = isGrounded;
            isGrounded = true;

            if (!wasGrounded && isGrounded && powerupType == Powerup.PowerupTypes.SmashAttack && smashState == SmashStates.Ready)
            {
                SmashArea.SetActive(true);
                StartCoroutine(SmashCountdownRoutine());
                smashState = SmashStates.None;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            wasGrounded = isGrounded;
            isGrounded = false;

            if (wasGrounded && !isGrounded && powerupType == Powerup.PowerupTypes.SmashAttack && smashState == SmashStates.Preparing)
            {
                smashState = SmashStates.Ready;
            }
        }
    }

    private IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(powerupLength);
        powerupType = Powerup.PowerupTypes.None;
        PowerupIndicator.PowerupType = Powerup.PowerupTypes.None;
    }

    private IEnumerator SmashCountdownRoutine()
    {
        yield return new WaitForSeconds(smashLength);
        SmashArea.SetActive(false);
    }

    private IEnumerator RocketAttackRoutine()
    {
        while (powerupType == Powerup.PowerupTypes.RocketAttack)
        {
            var closestEnemy = Enemy.ClosestEnemy(transform.position);
            var direction = (closestEnemy.transform.position - transform.position).normalized;
            var launchPosition = transform.position + direction;
            Instantiate(RocketPrefab, launchPosition, RocketPrefab.transform.rotation);
            yield return new WaitForSeconds(rocketAttackDelay);
        }
    }

    private IEnumerator Gameover()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(2);
    }

    private enum SmashStates
    {
        None,
        Preparing,
        Ready,
    }
}
