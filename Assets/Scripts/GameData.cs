using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData {

	public enum GameState
	{
		MENU,
		STARTING,
		PLAYING,
		DYING,
		DEAD
	}

    public static Canvas canvas;

	public static float scrollSpeed = 3.0f;
	public static float distTraveled = 0.0f;
	public static int missedInsects = 0;
	public static int hitInsects = 0;
	private static GameState state;
    private static GameState nextState;

    public static void setState(GameState nextState)
    {
        GameData.nextState = nextState;
    }

    public static GameState GetState()
    {
        return state;
    }

    public static void UpdateState()
    {
        state = nextState;
    }
}
