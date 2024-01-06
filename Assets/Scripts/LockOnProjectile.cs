// Player missile

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LockOnProjectile : MonoBehaviour
{
    public float bulletVelocity = 25;
    public float maxBulletVelocity = 500;
    public float delayToSeek = 0f;
    public float lockDistance = 1f;

    private Vector2 shootToward;
    private float timeAlive = 0;
    private float maxTimeAlive = 4;
    private GameObject lockedOnEnemy;
    private GameObject cross;

    private void Start()
    {

    }

    void Update()
    {
        timeAlive += Time.deltaTime;

        if (timeAlive > maxTimeAlive)
        {
            Destroy(cross);
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (timeAlive >= delayToSeek)
        {
            SeekEnemy();
        }

        GetComponent<Rigidbody2D>().AddForce(transform.up * bulletVelocity);
    }

    void SeekEnemy()
    {
        Vector2 targetPosition = lockedOnEnemy.transform.position;
        Vector2 missilePosition = transform.position;
        float yCoord = targetPosition.y - missilePosition.y;
        float xCoord = targetPosition.x - missilePosition.x;
        float rotateToRad = Mathf.Atan(yCoord / xCoord);
        float toAdd = -90;
        if (targetPosition.x < missilePosition.x)
        {
            toAdd = 90;
        }
        float rotateToDeg = rotateToRad * (180 / Mathf.PI) + toAdd;
        transform.rotation = Quaternion.Euler(0, 0, rotateToDeg);
    }

    public void SetEnemy(GameObject enemy)
    {
        lockedOnEnemy = enemy;
    }

    public void SetCrosshairs(GameObject crosshairs)
    {
        cross = crosshairs;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("enemy"))
        {
            Destroy(cross);
            Destroy(gameObject);
        }
    }
}