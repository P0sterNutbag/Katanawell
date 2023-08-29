using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static float gameTime = 0;
    public static int kills = 0;
    public static int maxCombo = 0;
    public static bool startTimer = false;
    public AudioSource mainSong;
    public AudioSource introSong;

    public void Start()
    {
        introSong.Play();
        if (mainSong.isPlaying)
        {
            mainSong.Stop();
        }
        startTimer = false;
        kills = 0;
        maxCombo = 0;
        gameTime = 0;
    }

    private void Update()
    {
        if (startTimer)
        {
            gameTime += Time.deltaTime;
            if (introSong.isPlaying)
            {
                introSong.Pause();
                mainSong.Play();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public IEnumerator FreezeTime(float time)
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(time);
        Time.timeScale = 1;
    }

}
