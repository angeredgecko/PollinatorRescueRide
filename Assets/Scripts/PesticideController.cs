﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PesticideController : MonoBehaviour {

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


    // Use this for initialization
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        ps = gameObject.GetComponent<ParticleSystem>();
        bc = GameObject.Find("Ball").GetComponent<BallController>();

        initialY = transform.position.y;
        time = 0;

		bc2d = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
		if (state == "moving")
		{
			transform.position = new Vector3(transform.position.x - GameData.scrollSpeed * (7.4f / 3f) * Time.deltaTime, .25f * Mathf.Sin(4f * time) + initialY, transform.position.z);
		} else
		{
			Vector3 target = new Vector3(0, 0, 0);
			if (transform.position.Equals(target))
			{

			}
			else
			{
				float currentTime = Time.time;
				float deltaTime = currentTime - timeDie;
				float maxTime = 1.5f;
				Vector3 pos = Vector3.Lerp(beforeDie, target, deltaTime / maxTime);
				transform.position = pos;
			}
		}

        if (transform.position.x < -11 & !hit)
        {
            Destroy(gameObject);
        }

        if (hit && !ps.isPlaying)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Ball")
        {
            // Stops GameObject2 moving
            sr.enabled = false;
            ps.Play();
            hit = true;
            bc.Die();
			timeDie = Time.time;
			beforeDie = transform.position;
			state = "dead";
			bc2d.enabled = false;
        }
    }
}