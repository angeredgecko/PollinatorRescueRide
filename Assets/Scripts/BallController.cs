using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

    public float sensitivity = 10.0f;
    public bool hold = false;
    public Rigidbody2D rb;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        bool mouseDown = false;
        if (hold)
        {
            mouseDown = Input.GetMouseButton(0);
        } else
        {
            mouseDown = Input.GetMouseButtonDown(0);
        }
		if (mouseDown)
        {
            rb.velocity = new Vector2(0, sensitivity);
        }
	}

    private void FixedUpdate()
    {

    }
}
