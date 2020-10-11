using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //config parameters

    [Header("Payer")]
    [SerializeField] float gameSpeed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] int health = 200;

    [Header("Projectile")]
    [SerializeField] GameObject laser;
    [SerializeField] float LaserSpeed = 10f;
    [SerializeField] float ShootPauseTime = 0.1f;
    [SerializeField] AudioClip ShootSound;
    [SerializeField] AudioClip DeathSound;

    //State variables
    float minX, minY, maxX, maxY;
    Coroutine FireContine;
    // Start is called before the first frame update
    void Start()
    {
        setUpBoundry();
    }

    private void setUpBoundry()
    {
        Camera gameCamera =  Camera.main;
        minX = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        minY = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        maxX = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        maxY = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
    }


    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            FireContine = StartCoroutine(FireContinuously());   
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(FireContine);
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject Laser = Instantiate(laser, transform.position, Quaternion.identity) as GameObject;
            Laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, LaserSpeed);
            AudioSource.PlayClipAtPoint(ShootSound, transform.position);
            yield return new WaitForSeconds(ShootPauseTime);
        }
        
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * gameSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * gameSpeed;
        var newXpos = Mathf.Clamp(transform.position.x + deltaX, minX, maxX);
        var newYpos = Mathf.Clamp(transform.position.y + deltaY,minY,maxY);
        transform.position = new Vector2(newXpos, newYpos);


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DemageDealer demageDealer = collision.GetComponent<DemageDealer>();
        if (!demageDealer) { return; }
        ProcessHit(demageDealer);
 
    }

    private void ProcessHit(DemageDealer demageDealer)
    {
        health -= demageDealer.getDemage();
        demageDealer.Hit();
        if (health <= 0)
        {
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(DeathSound, transform.position);
        }
    }
}
