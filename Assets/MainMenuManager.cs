using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour {
    public Text highScore;
	// Use this for initialization
	void Start () {
        highScore.text = PlayerPrefs.GetInt("score", 0).ToString();
	}

}
