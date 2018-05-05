using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour {

    Canvas c;

    private void Awake()
    {
        GameData.setState(GameData.GameState.MENU);
        GameData.UpdateState();
    }

    // Use this for initialization
    void Start () {
        c = GameObject.Find("Canvas").GetComponent<Canvas>();
	}
	
	// Update is called once per frame
	void Update () {
        GameData.UpdateState();
	}

    public void OnPlay()
    {
        GameData.setState(GameData.GameState.PLAYING);
        c.enabled = false;
    }
}
