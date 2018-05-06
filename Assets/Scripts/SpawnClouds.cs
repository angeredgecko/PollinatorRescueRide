using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnClouds : MonoBehaviour {

    public GameObject cloudPreFab;
    public Dictionary<GameObject, float[]> clouds = new Dictionary<GameObject, float[]>();
    public int maxclouds = 5;
    public float cloudSpeed = 1.0f;
    public float minWaitTime = 0.5f;
    public float cloudSize = 5f;
    public float yPos = -2.2f;
    public int depth = 0;

    bool hasSpawned = false;
    float lastcloudTime = 0.0f;
    float nextRandom = 0.0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameData.GetState() == GameData.GameState.PLAYING)
        {
            hasSpawned = false;
            float timeSinceLastcloud = Time.time - lastcloudTime;
            if (clouds.Count < maxclouds && !hasSpawned && timeSinceLastcloud - nextRandom > minWaitTime)
            {
                float randYOffset = Random.Range(-1, 1);
                GameObject newCloud = Instantiate(cloudPreFab, new Vector3(11f, yPos + randYOffset, depth), Quaternion.identity);
                newCloud.transform.localScale = new Vector3(cloudSize * Random.Range(.8f, 1.2f), cloudSize * Random.Range(.8f, 1.2f), cloudSize * Random.Range(.8f, 1.2f));
                clouds.Add(newCloud, new float[] { 0, randYOffset });
                hasSpawned = true;
                lastcloudTime = Time.time;
                nextRandom = Random.value * 2;
            }
            foreach (GameObject cloud in new List<GameObject>(clouds.Keys))
            {
                clouds[cloud][0] += Time.deltaTime;
                if (cloud.transform.position.x < -11f)
                {
                    clouds.Remove(cloud);
                    Destroy(cloud);
                    break;
                }
                cloud.transform.position = new Vector3(cloud.transform.position.x - GameData.scrollSpeed * (Time.deltaTime * (cloudSpeed / 3f)), .15f * Mathf.Sin(1.4f * clouds[cloud][0]) + yPos + clouds[cloud][1], depth);
            }
        }
    }
    public void ResetValues()
    {
        hasSpawned = false;
        lastcloudTime = 0.0f;
        nextRandom = 0.0f;
    }
}
