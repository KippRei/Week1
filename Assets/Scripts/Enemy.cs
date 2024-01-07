using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Enemy : MonoBehaviour
{
    public float enemySpeed = 0.7f;
    public static int idNum = 0;

    private GameObject player;
    private int health = 12;
    private GameObject healthBar;
    private float healthBarLength;
    private float healthBarIncrement;


    // Start is called before the first frame update
    void Start()
    {
        healthBar = transform.Find("HealthBar").gameObject;
        healthBarLength = healthBar.transform.localScale.x;
        healthBarIncrement = healthBarLength / health;
        idNum++;
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("Player");
       // transform.Translate(-(transform.position - player.transform.position).normalized * enemySpeed * Time.deltaTime);
        if (health <= 0)
        {
            GameObject.Find("GameLoop").GetComponent<SpawnEnemy>().EnemyDied();
            Destroy(gameObject);
        }
    }

    // TODO: check to ensure player projectile is what collides with enemy
    void OnTriggerEnter2D(Collider2D col)
    {
        /*health -= 1;
        healthBarLength -= healthBarIncrement;
        healthBar.transform.localScale = new Vector3(healthBarLength, healthBar.transform.localScale.y, healthBar.transform.localScale.z);*/
    }
}
