using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPesticide : MonoBehaviour {

    public GameObject pesticidePrefab;
    public float avgTime = 2f;
    public float maxRandTime = 1f;

    public float randTime = 1.0f;

    public float lastTime = 0;

    public List<GameObject> pesticides = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        lastTime = Time.time;
        randTime = Random.Range(-maxRandTime / 2, maxRandTime / 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameData.GetState() == GameData.GameState.PLAYING)
        {
            float currentTime = Time.time;
            if (currentTime - lastTime > avgTime + randTime && GameData.GetState() == GameData.GameState.PLAYING)
            {
                lastTime = currentTime;
                float initalY = Random.Range(-3f, 4f);
                GameObject pesticide = Instantiate(pesticidePrefab, new Vector3(11, initalY, 0.5f), Quaternion.identity);
                randTime = Random.Range(-maxRandTime / 2, maxRandTime / 2);
                pesticides.Add(pesticide);
            }
        }
    }

    public void ResetValues()
    {
        randTime = 1.0f;
        lastTime = 0;
    }
}
