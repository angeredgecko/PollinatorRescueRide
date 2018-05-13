using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour {

    GameObject panel;
    Button[] mainMenuButtons;

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
        mainMenuButtons = panel.GetComponentsInChildren<Button>();
        audioSource = GetComponent<AudioSource>();
        currentState = CanvasState.MAIN;

        buttonContainer = GameObject.Find("MainButtonsContainer");
        statContainer = GameObject.Find("StatsContainer");
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
}
