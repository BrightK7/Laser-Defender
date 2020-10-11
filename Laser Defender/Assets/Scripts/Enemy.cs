using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 100f;
    [SerializeField] float shootCounter;
    [SerializeField] float maxTimeBetweenShoot = 2f;
    [SerializeField] float minTimeBetweenShoot = 0.2f;
    [SerializeField] GameObject laser;
    [SerializeField] float LaserSpeed = 8f;
    [SerializeField] GameObject explosionVFX;
    [SerializeField] float durationOfExplosion = 1f;
    [SerializeField] AudioClip explosionSound;
    [SerializeField] AudioClip shootSound;
    // Start is called before the first frame update
    void Start()
    {
        shootCounter = Random.Range(minTimeBetweenShoot, maxTimeBetweenShoot);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownShoot();
    }

    private void CountDownShoot()
    {
        shootCounter -= Time.deltaTime;
        if(shootCounter <= 0)
        {
            Fire();
            shootCounter = Random.Range(minTimeBetweenShoot, maxTimeBetweenShoot);
        }

    }

    private void Fire()
    {
        GameObject Laser = Instantiate(laser, transform.position, Quaternion.identity) as GameObject;
        Laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -LaserSpeed);
        AudioSource.PlayClipAtPoint(shootSound, transform.position);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DemageDealer demageDealer = other.GetComponent<DemageDealer>();
        if (!demageDealer) { return; }
        HandleHit(demageDealer);
       
    }

    private void HandleHit(DemageDealer demageDealer)
    {
        health -= demageDealer.getDemage();
        demageDealer.Hit();
        if (health <= 0)
        {
            GameObject explosion = Instantiate(explosionVFX, transform.position,transform.rotation);
            Destroy(explosion,durationOfExplosion);
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);
        }
    }
}
