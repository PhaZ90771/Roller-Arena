using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static List<Enemy> EnemyList = new List<Enemy>();

    public float Speed;

    private Rigidbody enemyRb;
    private GameObject player;

    private void Awake()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = FindObjectOfType<PlayerController>().gameObject;
        EnemyList.Add(this);
    }

    private void Update()
    {
        var lookDirection = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * Speed);

        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        EnemyList.Remove(this);
    }

    public static Enemy ClosestEnemy(Vector3 position)
    {
        if (!EnemyList.Any()) return null;

        return EnemyList
            .OrderBy(e => Vector3.Distance(e.transform.position, position))
            .First();
    }
}
