using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

    public float sensitivity = 10.0f;
    public bool hold = false;
    public Rigidbody2D rb;
    CircleCollider2D cc2d;

    Vector3 beforeDie;
    float timeDie;

    Animator animator;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        cc2d = GetComponent<CircleCollider2D>();
        rb.isKinematic = true;

        animator.SetBool("Running?", true);
	}
	
	// Update is called once per frame
	void Update () {
        if (GameData.GetState() == GameData.GameState.MENU)
        {
            rb.isKinematic = true;
        }
        else if (GameData.GetState() == GameData.GameState.PLAYING)
        {
            rb.isKinematic = false;
            bool mouseDown = getClick(hold);
            if (mouseDown)
            {
                rb.velocity = new Vector2(0, sensitivity);
            }
        } else if (GameData.GetState() == GameData.GameState.DYING)
        {
            rb.velocity = new Vector2(0,0);
            Vector3 target = new Vector3(0, 0, 0);
            if (transform.position.Equals(target))
            {
                
            }
            else
            {
                float currentTime = Time.time;
                float deltaTime = currentTime - timeDie;
                float maxTime = .75f;
                Vector3 pos = Vector3.Lerp(beforeDie, target, deltaTime / maxTime);
                transform.position = pos;
            }
        } else if (GameData.GetState() == GameData.GameState.DEAD)
        {
            GameData.setState(GameData.GameState.MENU);
            GameData.panel.SetActive(true);
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
            GameData.hitInsects += 1;
        }
    }

    private void FixedUpdate()
    {

    }

    public void Die()
    {
		GameData.scrollSpeed = 0.0f;
        beforeDie = transform.position;
        timeDie = Time.time;
        cc2d.enabled = false;
        rb.isKinematic = true;
        animator.SetBool("Running?", false);
        GameData.setState(GameData.GameState.DYING);
    }

    public void ResetValues()
    {
        GameData.scrollSpeed = 3.0f;
        cc2d.enabled = true;
        rb.isKinematic = false;
        animator.SetBool("Running?", true);
        transform.position = new Vector3(-2.5f, 0f);
    }
}
