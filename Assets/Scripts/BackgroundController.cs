using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour {

    Material m_Material;
    public float scrollSpeed = 1.0f;

    // Use this for initialization
    void Start () {
        m_Material = GetComponent<Renderer>().material;
    }
	
	// Update is called once per frame
	void Update () {
        float offset = Time.deltaTime * scrollSpeed;
        m_Material.mainTextureOffset = new Vector2(m_Material.mainTextureOffset.x+offset, 0);
	}
}
