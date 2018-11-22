using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager _instance;
    public float timeToReplay = 5.0f;
    float timer;
    private void Awake()
    {
        if(_instance!= this)
        {
            _instance = this;
        }
    }
    public bool gameOver = false;
    public GameObject poolingCollector;
	// Update is called once per frame
	void Update () {
        if (gameOver)
        {
            timer += Time.deltaTime;
            if (timer > timeToReplay)
            {
                gameOver = false;
                timer = 0;
                Destroy(poolingCollector);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
	}
    public void GameOver()
    {
        gameOver = true;
        GameObject.FindGameObjectWithTag("BackgroundMusic").SetActive(false);
    }
}
