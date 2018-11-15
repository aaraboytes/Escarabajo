using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CrapBall : MonoBehaviour {
    public int score,maxScore;
    public float increment;
    public Text scoreText;
    public Image[] lifes;
    int size = 0;
    int life = 3;
    Vector3 initialSize;
    Player player;
    SphereCollider selfCollider;
    AudioSource audio;
    public AudioClip failClip, winClip;
    public GameObject poopParticle, pointsParticle;
    private void Start()
    {
        selfCollider = GetComponent<SphereCollider>();
        player = FindObjectOfType<Player>();
        initialSize = transform.localScale;
        audio = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Poop")){
            other.gameObject.SetActive(false);
            score++;
            size++;
            if (size == maxScore)
            {

                transform.localScale = initialSize;
                size = 0;
                pointsParticle.SetActive(true);
            }
            transform.localScale = new Vector3(transform.localScale.x +  increment, transform.localScale.y + increment, transform.localScale.z + increment);
            scoreText.text = score.ToString();
            audio.PlayOneShot(winClip);
        }else if (other.CompareTag("Obstacle"))
        {
            poopParticle.SetActive(true);
            life--;
            lifes[life].enabled = false;
            if (life == 0)
            {
                audio.PlayOneShot(failClip);
                GetComponent<SphereCollider>().enabled = false;
                GetComponent<Renderer>().enabled = false;
                player.enabled = false;

                if (PlayerPrefs.GetInt("score", 0) < score)
                {
                    PlayerPrefs.SetInt("score", score);
                }
                GameManager._instance.GameOver();
            }
            else
            {
                score -= score % 10;
                size = 0;
                scoreText.text = score.ToString();
                transform.localScale = initialSize;
            }
        }
    }
}
