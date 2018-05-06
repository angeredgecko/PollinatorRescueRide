using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour {

    Canvas c;
    BallController b;
    BackgroundController backC;
    GameObject worldObject;

    SpawnBees[] bees;
    SpawnClouds[] clouds;
    SpawnPesticide[] pesticides;
    SpawnTrees[] trees;

    private void Awake()
    {
        GameData.setState(GameData.GameState.MENU);
        GameData.UpdateState();
    }

    // Use this for initialization
    void Start () {
        c = GameObject.Find("Canvas").GetComponent<Canvas>();
        GameData.canvas = c;
        b = GameObject.Find("Ball").GetComponent<BallController>();
        backC = GameObject.Find("Plane").GetComponent<BackgroundController>();

        worldObject = GameObject.Find("WorldObject");
        bees = worldObject.GetComponents<SpawnBees>();
        clouds = worldObject.GetComponents<SpawnClouds>();
        pesticides = worldObject.GetComponents<SpawnPesticide>();
        trees = worldObject.GetComponents<SpawnTrees>();
    }
	
	// Update is called once per frame
	void Update () {
        GameData.UpdateState();
	}

    public void OnPlay()
    {
        GameData.setState(GameData.GameState.PLAYING);
        c.enabled = false;
        ResetGame();
    }

    public void ResetGame()
    {
        backC.m_Material.mainTextureOffset = new Vector2(0,0);
        b.ResetValues();
        foreach (SpawnTrees tree in trees)
        {
            foreach (GameObject thing in tree.trees)
            {
                Destroy(thing);
            }
            tree.trees.Clear();
        }
        foreach (SpawnClouds cloud in clouds)
        {
            foreach (GameObject thing in cloud.clouds.Keys)
            {
                Destroy(thing);
            }
            cloud.clouds.Clear();
        }
        foreach (SpawnBees bee in bees)
        {
            foreach (GameObject thing in bee.bees)
            {
                Destroy(thing);
            }
            bee.bees.Clear();
        }
        foreach (SpawnPesticide pesticide in pesticides)
        {
            foreach (GameObject thing in pesticide.pesticides)
            {
                Destroy(thing);
            }
            pesticide.pesticides.Clear();
        }
    }
}
