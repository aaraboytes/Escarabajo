using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CrapBall : MonoBehaviour {
    public int score,maxScore;
    public float increment;
    [Header("Lifes")]
    public GameObject[] lifes;
    public Material offLife;
    int size = 0;
    int life = 3;
    Vector3 initialSize;
    Player player;
    SphereCollider selfCollider;
    AudioSource audio;
    public AudioClip failClip, winClip;
    public GameObject pointsParticle;
    public Pool poopParticles;
    public Animator BeetleAnim;
    [Header("Score")]
    public Animator scoreAnim;
    public Text scoreText;
    public float scoreShowTime;
    bool isShowing = false;
    float scoreTextTimer = 0;

    private void Start()
    {
        selfCollider = GetComponent<SphereCollider>();
        player = FindObjectOfType<Player>();
        initialSize = transform.localScale;
        audio = GetComponent<AudioSource>();
    }
    private void Update()
    {
        //Score show time
        if (isShowing)
        {
            scoreTextTimer += Time.deltaTime;
            if (scoreTextTimer > scoreShowTime)
            {
                isShowing = false;
            }
        }
        //Show Score
        scoreAnim.SetBool("Opened", isShowing);
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
            ShowScore();
            audio.PlayOneShot(winClip);
        }else if (other.CompareTag("Obstacle"))
        {
            poopParticles.Recycle(transform.position);
            life--;
            Renderer currentRenderer = lifes[life].transform.GetChild(0).GetComponent<Renderer>();
            currentRenderer.material = offLife;
            if (life == 0)
            {
                //Game Over//
                ShowScore(false);
                audio.PlayOneShot(failClip);
                GetComponent<SphereCollider>().enabled = false;
                GetComponent<Renderer>().enabled = false;
                player.enabled = false;

                if (PlayerPrefs.GetInt("score", 0) < score)
                {
                    PlayerPrefs.SetInt("score", score);
                }
                BeetleAnim.SetTrigger("Die");
                GameManager._instance.GameOver();
            }
            else
            {
                //Damage//
                score -= score % 10;
                size = 0;
                isShowing = false;
                ShowScore(false);
                transform.localScale = initialSize;
            }
        }
    }
    void ShowScore(bool increase = true, string msg ="")
    {
        if (msg == "")
            msg = score.ToString();
        if (!increase)
        {
            scoreText.color = Color.red;
        }
        else
            scoreText.color = Color.white;
        scoreText.text = msg;
        isShowing = true;
        scoreTextTimer = 0;
    }
}
