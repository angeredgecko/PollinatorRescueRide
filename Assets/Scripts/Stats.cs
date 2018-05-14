using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stats {
    public static Stats current;
    public List<int> scores;
    public List<float> gameLengths;

    public Stats()
    {
        scores = new List<int>();
        gameLengths = new List<float>();
        Reset(); 
        current = this;
    }

    public void Reset()
    {
		scores.Clear();
		gameLengths.Clear();
		Debug.Log("called reset");
    }
}
