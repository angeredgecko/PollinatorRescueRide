using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class BallController : MonoBehaviour {

    public float sensitivity = 10.0f;
    public bool hold = false;
    public Rigidbody2D rb;
    CircleCollider2D cc2d;

    Vector3 beforeDie;
    float timeDie;

    [DllImport("__Internal")]
    private static extern void SyncData();

    // Use this for initialization
    void Start () {
        cc2d = GetComponent<CircleCollider2D>();
        rb.isKinematic = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (GameData.GetState() == GameData.GameState.MENU || GameData.GetState() == GameData.GameState.PAUSED)
        {
            rb.velocity = new Vector2(0, 0); 
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
                GameData.setState(GameData.GameState.DEAD);
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
            GameData.scoreAnim.playBig();
        }
		//test

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
            GameData.scoreAnim.playSmall();
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
        GameData.setState(GameData.GameState.DYING);
        GameData.lastScore = GameData.hitInsects;

        Stats.current.scores.Add(GameData.lastScore);
        Stats.current.gameLengths.Add(GameData.timePlaying);

        Storage.Save(Stats.current);
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            SyncData();
        }
    }

    public void ResetValues()
    {
        GameData.scrollSpeed = 3.0f;
        cc2d.enabled = true;
        rb.isKinematic = false;
        transform.position = new Vector3(-2.5f, 0f);
    }
}
