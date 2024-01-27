using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableTerrain : MonoBehaviour
{
    public int terrainLife = 1;
    private GameObject terrainBreakAudioGameObj;
    private AudioSource terrainBreakAudioSource;
    [SerializeField] private AudioClip terrainBreakSoundFX;

    // Start is called before the first frame update
    void Start()
    {
        terrainBreakAudioGameObj = GameObject.Find("TerrainAudioSource");
        terrainBreakAudioSource = terrainBreakAudioGameObj.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (terrainLife <= 0)
        {
            terrainBreakAudioSource.PlayOneShot(terrainBreakSoundFX);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "playerProjectile")
        {
            
            terrainLife -= 1;
        }
    }
}
