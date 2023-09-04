using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Impact = 1f;
    public float Timer = 1f;
    public float Speed = 1f;
    public float TurnSpeed = 1f;

    private Rigidbody projectRb;

    private void Awake()
    {
        projectRb = GetComponent<Rigidbody>();

        var closestEnemy = Enemy.ClosestEnemy(transform.position);
        if (closestEnemy != null)
        {
            var directionTowards = closestEnemy.transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(directionTowards);
        }
    }

    private void Update()
    {
        Timer -= Time.deltaTime;
        if (Timer <= 0f)
        {
            Destroy(gameObject);
        }

        var closestEnemy = Enemy.ClosestEnemy(transform.position);
        if (closestEnemy != null)
        {
            var directionTowards = closestEnemy.transform.position - transform.position;
            var step = TurnSpeed * Time.deltaTime;
            var newDirection = Vector3.RotateTowards(transform.forward, directionTowards, step, 0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }

        projectRb.AddForce(transform.forward * Speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            var enemyRb = other.gameObject.GetComponent<Rigidbody>();
            var direction = (other.transform.position - transform.position).normalized;
            enemyRb.AddForce(direction * Impact, ForceMode.Impulse);
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
