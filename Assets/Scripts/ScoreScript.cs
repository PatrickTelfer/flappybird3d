using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public static int scoreValue = 0;
    static Text score;
    // Start is called before the first frame update
    void Start()
    {
        score = GetComponent<Text>();
    }

    // Update is called once per frame
    public static void IncreaseScore()
    {
        scoreValue++;
        score.text = "" + scoreValue;
    }

    public static void ResetScore()
    {
        scoreValue = 0;
        score.text = "" + scoreValue;
    }
}
