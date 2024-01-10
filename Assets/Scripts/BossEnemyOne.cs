using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class BossEnemyOne : MonoBehaviour
{
    public float enemySpeed = 0.7f;
    public static int idNum = 0;
    public int bossHealth = 245;
    public BossOneGun bossGun;

    private GameObject player;
    private GameObject healthBar;
    private float healthBarLength;
    private float healthBarIncrement;
    private int fullHealth;


    // Start is called before the first frame update
    void Start()
    {
        fullHealth = bossHealth;
        healthBar = transform.Find("BossHealthBar").gameObject;
        healthBarLength = healthBar.transform.localScale.x;
        healthBarIncrement = healthBarLength / bossHealth;
        idNum++;
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("Player");
        //transform.Translate(-(transform.position - player.transform.position).normalized * enemySpeed * Time.deltaTime);
        if (bossHealth <= 0)
        {
            //GameObject.Find("GameLoop").GetComponent<SpawnEnemy>().EnemyDied();
            Destroy(gameObject);
        }

        float lifeLeft = (float)bossHealth / (float)fullHealth;
        Debug.Log(lifeLeft);
        if (lifeLeft < 0.25)
        {
            bossGun.numOfSuperProj = 5;
            bossGun.superProjInterval = 0.35f;
        }
        else if (lifeLeft < 0.5)
        {
            bossGun.numOfSuperProj = 4;
            bossGun.superProjInterval = 0.5f;
        }
    }

    // TODO: check to ensure player projectile is what collides with enemy
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "playerProjectile")
        {
            bossHealth -= 1;
            healthBarLength -= healthBarIncrement;
            healthBar.transform.localScale = new Vector3(healthBarLength, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
        }
    }
}
