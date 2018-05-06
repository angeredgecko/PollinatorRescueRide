using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour {

    public Material m_Material;
    public float scrollSpeed = 1.0f;
    GameObject ball;
    BallController bControl;

	// Use this for initialization

	private void Awake()
	{
		GameData.defaultScrollSpeed = scrollSpeed;
	}

	void Start () {
        m_Material = GetComponent<Renderer>().material;
        ball = GameObject.Find("Ball");
        bControl = ball.GetComponent<BallController>();
    }
	
	// Update is called once per frame
	void Update () {
        if (GameData.GetState() == GameData.GameState.PLAYING)
        {
            float offset = Time.deltaTime * GameData.scrollSpeed;
            GameData.distTraveled += offset * (7.4f / 3f);
            m_Material.mainTextureOffset = new Vector2(m_Material.mainTextureOffset.x + offset, 0);
        }
	}
}
