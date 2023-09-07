using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : MonoBehaviour
{
    public static int Score = 0;

    [SerializeField] private BallLauncher ballLauncher;
    [SerializeField] private TextMeshProUGUI ballsText, scoreText;


    void Update()
    {
        if (!ballsText || !scoreText || !ballLauncher) return;

        ballsText.text = ballLauncher.NumberOfBalls.ToString();
        scoreText.text = Score.ToString();
    }
}
