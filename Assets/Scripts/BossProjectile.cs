// Player missile

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossProjectile : MonoBehaviour
{
    public float bulletVelocity = 25;
    public TrailRenderer trail;

    private Vector2 shootToward;
    private float timeAlive = 0;
    private float maxTimeAlive = 2;

    private void Start()
    {
    }

    void Update()
    {
        timeAlive += Time.deltaTime;

        if (timeAlive > maxTimeAlive)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * bulletVelocity);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        if (col.gameObject.CompareTag("playerProjectile"))
        {
            Destroy(gameObject);
        }
        if (col.gameObject.CompareTag("playerShield"))
        {
            Destroy(gameObject);
        }
    }

    /*    private void OnDestroy()
        {
            trail.transform.parent = null;
            trail.autodestruct = true;
        }*/
}