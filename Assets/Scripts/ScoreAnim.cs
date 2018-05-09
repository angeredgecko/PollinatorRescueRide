using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreAnim : MonoBehaviour {

    public float scale = 1.0f;
    Vector3 defaultScale;

    Animator animator;

	// Use this for initialization
	void Start () {
        defaultScale = transform.localScale;
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.localScale = defaultScale * scale;
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
