﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour {

    SpriteRenderer sr;
    ParticleSystem ps;

    BallController bc;

    BoxCollider2D bc2d;

    bool playedEffect = false;
    bool hit = false;

    float initialY;
    float time;

    string state = "moving";
    float timeDie;
    Vector3 beforeDie;

    AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        ps = gameObject.GetComponent<ParticleSystem>();
        bc = GameObject.Find("Ball").GetComponent<BallController>();

        initialY = transform.position.y;
        time = 0;

        bc2d = gameObject.GetComponent<BoxCollider2D>();

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameData.GetState() == GameData.GameState.PLAYING)
        {
            time += Time.deltaTime;
            if (state == "moving")
            {
                transform.position = new Vector3(transform.position.x - GameData.scrollSpeed * (7.4f / 3f) * Time.deltaTime, .25f * Mathf.Sin(4f * time) + initialY, transform.position.z);
            }
        }
        else if (GameData.GetState() == GameData.GameState.DYING && hit)
        {
            
        }
        if (transform.position.x < -11 & !hit)
        {
            Destroy(gameObject);
        }

        if (hit)
        {
            //Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Ball")
        {
            Debug.Log("Hit");
            // Stops GameObject2 moving
            //sr.enabled = false;
            ps.Play();
            hit = true;
            bc.Die();
            timeDie = Time.time;
            beforeDie = transform.position;
            state = "dead";
            bc2d.enabled = false;
            audioSource.Play();
        }
    }
}
