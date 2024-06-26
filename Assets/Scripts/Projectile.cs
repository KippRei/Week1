// Player missile

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Projectile : MonoBehaviour
{
    public float bulletVelocity = 25;
    public TrailRenderer trail;

    private Vector2 shootToward;
    private float timeAlive = 0;
    private float maxTimeAlive = 2;
    private Rigidbody2D rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

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
        rb.AddForce(transform.up * bulletVelocity);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == 9 || col.gameObject.layer == 10)
        {
            Destroy(gameObject);
        }
    }

    /*  private void OnDisable()
      {
          trail.transform.parent = null;
          trail.autodestruct = true;
          Destroy(trail.gameObject, 0.6f);
          Destroy(gameObject);
      }*/
}