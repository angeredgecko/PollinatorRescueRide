using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour {

    GameObject panel;
    Button[] mainMenuButtons;
	public Text[] textStats;

    GameObject buttonContainer;
    GameObject statContainer;

    AudioSource audioSource;

    public enum CanvasState
    {
        MAIN,
        TUTORIAL,
        STATS
    }

    private static CanvasState currentState;
    private static CanvasState nextState;

    // Use this for initialization
    void Start () {
        panel = GameObject.Find("Panel");
        GameData.panel = panel;
		buttonContainer = GameObject.Find("MainButtonsContainer");
		statContainer = GameObject.Find("StatsContainer");
		mainMenuButtons = buttonContainer.GetComponentsInChildren<Button>();
        audioSource = GetComponent<AudioSource>();
		currentState = CanvasState.MAIN;
    }
	
	// Update is called once per frame
	void Update () {
        currentState = nextState;
        if (GameData.GetState() == GameData.GameState.MENU || GameData.GetState() == GameData.GameState.PAUSED)
        {
            panel.SetActive(true);

            if (getState() == CanvasState.MAIN)
            {
                buttonContainer.SetActive(true);
                statContainer.SetActive(false);
                foreach (Button b in mainMenuButtons)
                {
                    b.enabled = true;
                    if (b.gameObject.name.Equals("Continue"))
                    {
                        b.interactable = GameData.GetState() == GameData.GameState.PAUSED;
                    }
                    if (b.gameObject.name.Equals("PlayerButton"))
                    {
                        b.GetComponentInChildren<Text>().text = GameData.GetState() == GameData.GameState.MENU ? "Play" : "Restart";
                    }
                }
            } else if (getState() == CanvasState.STATS)
            {
                buttonContainer.SetActive(false);
                statContainer.SetActive(true);

				if (Stats.current.scores.Count > 0)
				{
					textStats[0].text = Stats.current.scores.ToArray().Max().ToString("D3");
					textStats[1].text = (Stats.current.scores.ToArray().Sum() / Stats.current.scores.Count).ToString("D3");
					int maxTimeSec = ((int)(Stats.current.gameLengths.ToArray().Max()));
					int maxTimeMin = maxTimeSec / 60;
					textStats[2].text = maxTimeMin.ToString("D2") + ":" + (maxTimeSec%60).ToString("D2");
					int avgTimeSec = ((int)(Stats.current.gameLengths.ToArray().Sum() / Stats.current.gameLengths.Count));
					int avgTimeMin = avgTimeSec / 60;
					textStats[3].text = avgTimeMin.ToString("D2") + ":" + (avgTimeSec % 60).ToString("D2");
				} else
				{
					textStats[0].text = "000";
					textStats[1].text = "000";
					textStats[2].text = "00:00";
					textStats[3].text = "00:00";
				}
			}
        }
        else
        {
            setState(CanvasState.MAIN);
            panel.SetActive(false);
        }

        if (GameData.GetState() == GameData.GameState.DEAD)
        {
            panel.GetComponent<PanelAnim>().playPopup();
        }

    }

    public void onContinue()
    {
        GameData.setState(GameData.GameState.PLAYING);
    }

    public void OnPlay()
    {
        GameData.setState(GameData.GameState.PLAYING);
        panel.SetActive(false);
        WorldController.wc.ResetGame();
    }

    public void onTutorial()
    {
        setState(CanvasState.TUTORIAL);
    }

    public void onStats()
    {
        setState(CanvasState.STATS);
    }

    public void playSound()
    {
        audioSource.Play();
    }

    public static void setState(CanvasState cs)
    {
        nextState = cs;
    }
    public static CanvasState getState()
    {
        return currentState;
    }

	public void onBack()
	{
		setState(CanvasState.MAIN);
	}

	[DllImport("__Internal")]
	private static extern void SyncData();

	public void OnResetStats()
	{
		Stats.current.Reset();
		Storage.Save(Stats.current);
		if (Application.platform == RuntimePlatform.WebGLPlayer)
		{
			SyncData();
		}
	}
}
