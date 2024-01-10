// Player missile

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossSuperProjectile : MonoBehaviour
{
    public float bossSuperSpeed = 15;

    private float timeAlive = 0;
    private float maxTimeAlive = 4;

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
        GetComponent<Rigidbody2D>().AddForce(transform.up * bossSuperSpeed);
        transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(2.5f, transform.localScale.y, 0), .1f);
    }
}