using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour {

    Material m_Material;
    public float scrollSpeed = 1.0f;
    GameObject ball;
    BallController bControl;

    // Use this for initialization
    void Start () {
        m_Material = GetComponent<Renderer>().material;
        ball = GameObject.Find("Ball");
        bControl = ball.GetComponent<BallController>();
    }
	
	// Update is called once per frame
	void Update () {
        float offset = Time.deltaTime * scrollSpeed;
        bControl.distTraveled += offset * (7.4f/3f);
        m_Material.mainTextureOffset = new Vector2(m_Material.mainTextureOffset.x+offset, 0);
	}
}
