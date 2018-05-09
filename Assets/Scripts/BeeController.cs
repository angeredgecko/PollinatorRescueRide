using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeController : MonoBehaviour {

    SpriteRenderer sr;
    ParticleSystem ps;

    BallController bc;

    CircleCollider2D cc2d;

    bool playedEffect = false;
    bool hit = false;

    float initialY;
    float time;

	// Use this for initialization
	void Start () {
        sr = gameObject.GetComponent<SpriteRenderer>();
        ps = gameObject.GetComponent<ParticleSystem>();
        bc = GameObject.Find("Ball").GetComponent<BallController>();
        cc2d = GetComponent<CircleCollider2D>();

        initialY = transform.position.y;
        time = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (GameData.GetState() == GameData.GameState.PLAYING)
        {
            time += Time.deltaTime;
            transform.position = new Vector3(transform.position.x - GameData.scrollSpeed * (7.4f / 3f) * Time.deltaTime, .25f * Mathf.Sin(4f * time) + initialY, transform.position.z);
        }
        if (transform.position.x < -11 & !hit)
        {
            GameData.missedInsects += 1;
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
            cc2d.enabled = false;
            sr.enabled = false;
            ps.Play();
            hit = true;
        }
    }
}
