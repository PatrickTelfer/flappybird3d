using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PipeSpawner p;
    public static GameObject resetText;
    float initialTime;

    static AudioSource audioSource;

    static int score;
    // Start is called before the first frame update
    void Start()
    {
        initialTime = Time.timeScale;
        resetText = GameObject.FindGameObjectWithTag("text");
        Time.timeScale = 0;
        displayText(true);
        audioSource = GetComponent<AudioSource>();
    }
    
    public static void displayText(bool active)
    {
        resetText.SetActive(active);
    }

    public static void IncreaseScore()
    {
        score++;
        ScoreScript.IncreaseScore();
    }

    public static void PlaySoundEffect()
    {
        audioSource.PlayOneShot(audioSource.clip, 0.7f);
    }

    public static void ResetScore()
    {
        ScoreScript.ResetScore();
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKey(KeyCode.R) || Input.touchCount > 1 )&& Time.timeScale == 0)
        {

            ResetScore();

            displayText(false);
            Time.timeScale = initialTime;
            p.Reset();
        }
    }
}
