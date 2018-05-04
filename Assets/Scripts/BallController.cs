using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

    public float sensitivity = 10.0f;
    public bool hold = false;
    public Rigidbody2D rb;

    public float distTraveled = 0.0f;

    public float score = 0.0f;
    public float missedInsects = 0.0f;

    bool dead = false;
    Vector3 beforeDie;
    float timeDie;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (!dead)
        {
            bool mouseDown = getClick(hold);
            if (mouseDown)
            {
                rb.velocity = new Vector2(0, sensitivity);
            }
        } else
        {
            Vector3 target = new Vector3(0, 0, 0);
            if (transform.position.Equals(target))
            {

            } else
            {
                float currentTime = Time.time;
                float deltaTime = currentTime - timeDie;
                float maxTime = 1.5f;
                Vector3 pos = Vector3.Lerp(beforeDie, target, deltaTime/maxTime);
                transform.position = pos;
            }
        }
	}

    bool getClick(bool hold)
    {
        if (hold)
        {
            return Input.GetMouseButton(0) || Input.GetButton("Jump");
        }
        else
        {
            return Input.GetMouseButtonDown(0) || Input.GetButtonDown("Jump");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bee")
        {
            score += 1;
        }
    }

    private void FixedUpdate()
    {

    }

    public void Die()
    {
        Debug.Log("died");
        beforeDie = transform.position;
        timeDie = Time.time;
        dead = true;
    }
}
