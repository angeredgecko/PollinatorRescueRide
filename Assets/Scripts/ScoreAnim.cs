using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreAnim : MonoBehaviour {

    public float scale = 1.0f;
    Vector3 defaultScale;

    Animator animator;

    Text text;

	// Use this for initialization
	void Start () {
        defaultScale = transform.localScale;
        animator = GetComponent<Animator>();
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.localScale = defaultScale * scale;
        string score = "";
        if (GameData.GetState() == GameData.GameState.MENU)
        {
            score = GameData.lastScore.ToString("D3");
            text.transform.localPosition = new Vector3(text.transform.localPosition.x, 115f, 0);
        }
        else
        {
            score = GameData.hitInsects.ToString("D3");
            text.transform.localPosition = new Vector3(text.transform.localPosition.x, -210f, 0);
        }
        text.text = score;
    }

    public void playBig()
    {
        Debug.Log("played");
        animator.Play("ScoreAnimFrom0");
    }

    public void playSmall()
    {
        animator.Play("ScoreAnimFrom100");
    }
}
