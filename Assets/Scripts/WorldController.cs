using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour {

    public static WorldController wc;

    BallController b;
    BackgroundController backC;
    GameObject worldObject;

    SpawnBees[] bees;
    SpawnClouds[] clouds;
    SpawnPesticide[] pesticides;
    SpawnTrees[] trees;
    SpawnObstacle[] obstacles;

    float beginTime;

    ScoreAnim scoreAnim;

    AudioSource audio;

    private void Awake()
    {
        Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
        GameData.setState(GameData.GameState.MENU);
        GameData.UpdateState();

        Debug.Log(Storage.Load(out Stats.current));
        Storage.ListIndexedDBFiles();
        Debug.Log(Application.persistentDataPath);
        Debug.Log("Gamelengths: " + Stats.current.gameLengths.Count);
        Debug.Log("Scores: " + Stats.current.scores.Count);
    }

    // Use this for initialization
    void Start () {
        wc = this;
        //c = GameObject.Find("Canvas").GetComponent<Canvas>();
        b = GameObject.Find("Ball").GetComponent<BallController>();
        backC = GameObject.Find("Plane").GetComponent<BackgroundController>();

        worldObject = GameObject.Find("WorldObject");
        bees = worldObject.GetComponents<SpawnBees>();
        clouds = worldObject.GetComponents<SpawnClouds>();
        pesticides = worldObject.GetComponents<SpawnPesticide>();
        trees = worldObject.GetComponents<SpawnTrees>();
        obstacles = worldObject.GetComponents<SpawnObstacle>();

        scoreAnim = GameObject.Find("LastScoreText").GetComponent<ScoreAnim>();
        GameData.scoreAnim = scoreAnim;

        GameData.scrollSpeed = GameData.defaultScrollSpeed;

        beginTime = Time.time;

        audio = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (GameData.GetState() == GameData.GameState.PLAYING)
        {
            GameData.timePlaying = Time.time - beginTime;
            GameData.scrollSpeed = GameData.maxScrollSpeed / (1f + ((GameData.maxScrollSpeed / GameData.defaultScrollSpeed) - 1) * (Mathf.Exp(-.023f * (GameData.timePlaying))));

            if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
            {
                GameData.panel.GetComponent<PanelAnim>().playPopup();
                GameData.scoreAnim.playBig();
                GameData.setState(GameData.GameState.PAUSED);
            }
        } else if (GameData.GetState() == GameData.GameState.PAUSED)
        {
            if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
            {
                GameData.setState(GameData.GameState.PLAYING);
            }
        }
        else
        {
            beginTime = Time.time;
        }

        GameData.UpdateState();

        //GameData.scrollSpeed = GameData.defaultScrollSpeed + (GameData.distTraveled / 250f);
        //Debug.Log(GameData.scrollSpeed);
	}

    public void ResetGame()
    {
        backC.m_Material.mainTextureOffset = new Vector2(0,0);
        b.ResetValues();
        GameData.ResetValues();
        foreach (SpawnTrees tree in trees)
        {
            foreach (GameObject thing in tree.trees)
            {
                Destroy(thing);
            }
            tree.trees.Clear();
            tree.ResetValues();
        }
        foreach (SpawnClouds cloud in clouds)
        {
            foreach (GameObject thing in cloud.clouds.Keys)
            {
                Destroy(thing);
            }
            cloud.clouds.Clear();
            cloud.ResetValues();
        }
        foreach (SpawnBees bee in bees)
        {
            foreach (GameObject thing in bee.bees)
            {
                Destroy(thing);
            }
            bee.bees.Clear();
            bee.ResetValues();
        }
        foreach (SpawnPesticide pesticide in pesticides)
        {
            foreach (GameObject thing in pesticide.pesticides)
            {
                Destroy(thing);
            }
            pesticide.pesticides.Clear();
            pesticide.ResetValues();
        }
        foreach (SpawnObstacle obstacle in obstacles)
        {
            foreach (GameObject thing in obstacle.obstacles)
			{
				Destroy(thing);
            }
            obstacle.obstacles.Clear();
            obstacle.ResetValues();
        }
    }
}
