using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour {

    Canvas c;
    GameObject panel;
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

    private void Awake()
    {
        GameData.setState(GameData.GameState.MENU);
        GameData.UpdateState();
    }

    // Use this for initialization
    void Start () {
        //c = GameObject.Find("Canvas").GetComponent<Canvas>();
        panel = GameObject.Find("Panel");
        GameData.panel = panel;
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
    }
	
	// Update is called once per frame
	void Update () {
        if (GameData.GetState() == GameData.GameState.PLAYING)
        {
            GameData.timePlaying = Time.time - beginTime;
            GameData.scrollSpeed = GameData.maxScrollSpeed / (1f + ((GameData.maxScrollSpeed / GameData.defaultScrollSpeed) - 1) * (Mathf.Exp(-.023f * (GameData.timePlaying))));
        } else
        {
            beginTime = Time.time;
        }

        GameData.UpdateState();

        //GameData.scrollSpeed = GameData.defaultScrollSpeed + (GameData.distTraveled / 250f);
        //Debug.Log(GameData.scrollSpeed);
	}

    public void OnPlay()
    {
        GameData.setState(GameData.GameState.PLAYING);
        panel.SetActive(false);
        ResetGame();
    }

    public void onTutorial()
    {

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
