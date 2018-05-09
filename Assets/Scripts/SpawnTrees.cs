using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrees : MonoBehaviour {

    public List<GameObject> treePreFabs = new List<GameObject>();
    public List<GameObject> trees = new List<GameObject>();
    public int maxTrees = 5;
    public float treeSpeed = 1.0f;
    public float minWaitTime = 0.5f;
    public float treeSize = 5f;
    public float yPos = -2.2f;
    public int depth = 0;

    bool hasSpawned = false;
    float lastTreeTime = 0.0f;
    float nextRandom = 0.0f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (GameData.GetState() == GameData.GameState.PLAYING)
        {
            hasSpawned = false;
            float timeSinceLastTree = Time.time - lastTreeTime;
            if (trees.Count < maxTrees && !hasSpawned && timeSinceLastTree - nextRandom > minWaitTime)
            {
                int index = Random.Range(0, treePreFabs.Count);
                //Debug.Log(index + " : " + treePreFabs.Count);
                GameObject treePreFab = treePreFabs[index];
                treePreFab.transform.localScale = new Vector3(treeSize, treeSize, treeSize);
                if (index == 1)
                {
                    treePreFab.transform.localScale *= 1.2f;
                }
                trees.Add(Instantiate(treePreFab, new Vector3(12f, yPos, depth), Quaternion.identity));
                hasSpawned = true;
                lastTreeTime = Time.time;
                nextRandom = Random.value;
            }
            foreach (GameObject tree in trees.ToArray())
            {
                if (tree.transform.position.x < -12f)
                {
                    trees.Remove(tree);
                    Destroy(tree);
                }
                //tree.transform.localScale = new Vector3(treeSize, treeSize, treeSize);
                tree.transform.position = new Vector3(tree.transform.position.x - GameData.scrollSpeed * (Time.deltaTime * (treeSpeed / 3f)), tree.transform.position.y, depth);
            }
        }
	}
    public void ResetValues()
    {
        hasSpawned = false;
        lastTreeTime = 0.0f;
        nextRandom = Random.value;
    }
}
