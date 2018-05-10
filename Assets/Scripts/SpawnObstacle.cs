using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacle : MonoBehaviour {

    public GameObject obstaclePrefab;
    public float avgTime = 2f;
    public float maxRandTime = 1f;

    public float randTime = 1.0f;

    public float lastTime = 0;

    public List<GameObject> obstacles = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        ResetValues();
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
                float initalY = Random.Range(-2f, 3.2f);
                float randomRot = Random.Range(0, 7) * 45;
                Vector3 rot = new Vector3(0,0,randomRot);
                GameObject obstacle = Instantiate(obstaclePrefab, new Vector3(11, initalY, 0.6f), Quaternion.Euler(rot));
                randTime = Random.Range(-maxRandTime / 2, maxRandTime / 2);
                obstacles.Add(obstacle);
            }
        }
    }

    public void ResetValues()
    {
        randTime = Random.Range(-maxRandTime / 2, maxRandTime / 2);
        lastTime = Time.time;
    }
}
