using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public void ClickReiniciar()
    {
        SceneManager.LoadScene(1); //Carga la escena actual
        Time.timeScale = 1; //Restaura el tiempo del juego
    }

    public void ClickMenuPrincipal()
    {
        SceneManager.LoadScene(0); //Carga la escena del menú principal
        Time.timeScale = 1; //Restaura el tiempo del juego
    }
}
