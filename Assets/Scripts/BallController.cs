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

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        bool mouseDown = getClick(hold);
		if (mouseDown)
        {
            rb.velocity = new Vector2(0, sensitivity);
        }
        Debug.Log(distTraveled);
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
}
