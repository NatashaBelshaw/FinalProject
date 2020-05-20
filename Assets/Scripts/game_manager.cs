using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class game_manager : MonoBehaviour
{
    bool gameHasEnded = false;

    public void GameOver()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("GAME OVER");
            YouDied();
        }
    }

    void YouDied()
    {
        SceneManager.LoadScene("GameOver");
    }
}
