using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    public TMP_Text time;
    public TMP_Text kills;
    public TMP_Text combo;

    private void Start()
    {
        time.text = GameController.gameTime.ToString("#.00");
        kills.text = GameController.kills.ToString();
        combo.text = GameController.maxCombo.ToString();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
