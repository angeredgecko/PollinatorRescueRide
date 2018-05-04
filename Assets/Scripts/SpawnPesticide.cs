﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPesticide : MonoBehaviour {

    public GameObject pesticidePrefab;
    public float avgTime = 2f;
    public float maxRandTime = 1f;

    public float randTime = 1.0f;

    public float lastTime = 0;

    // Use this for initialization
    void Start()
    {
        lastTime = Time.time;
        randTime = Random.Range(-maxRandTime / 2, maxRandTime / 2);
    }

    // Update is called once per frame
    void Update()
    {
        float currentTime = Time.time;
        if (currentTime - lastTime > avgTime + randTime)
        {
            lastTime = currentTime;
            float initalY = Random.Range(-3f, 4f);
            GameObject pesticide = Instantiate(pesticidePrefab, new Vector3(11, initalY, 0.5f), Quaternion.identity);
            randTime = Random.Range(-maxRandTime / 2, maxRandTime / 2);
        }
    }
}
