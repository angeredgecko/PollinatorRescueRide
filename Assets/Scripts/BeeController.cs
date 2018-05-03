using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeController : MonoBehaviour {

    SpriteRenderer sr;
    ParticleSystem ps;

    BallController bc;

    bool playedEffect = false;

    float initialY;
    float time;

	// Use this for initialization
	void Start () {
        sr = gameObject.GetComponent<SpriteRenderer>();
        ps = gameObject.GetComponent<ParticleSystem>();
        bc = GameObject.Find("Ball").GetComponent<BallController>();

        initialY = transform.position.y;
        time = 0;
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        transform.position = new Vector3(transform.position.x - (7.4f) * Time.deltaTime, .25f * Mathf.Sin(4f * time) + initialY, transform.position.z);

        if (transform.position.x < -11)
        {
            bc.missedInsects += 1;
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
            playedEffect = true;
        }
        if (playedEffect == true && !ps.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
