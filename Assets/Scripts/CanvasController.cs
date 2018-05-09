using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour {

    public Text text;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        string score = "";
        if (GameData.GetState() == GameData.GameState.MENU)
        {
            score = GameData.lastScore.ToString("D3");
            text.transform.localPosition = new Vector3(text.transform.localPosition.x, 115f, 0);
        } else
        {
            score = GameData.hitInsects.ToString("D3");
            text.transform.localPosition = new Vector3(text.transform.localPosition.x, -195f, 0);
        }
        text.text = score;
	}
}
