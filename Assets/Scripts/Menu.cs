/**
 * Autor: Sergio Fernández Verdugo
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    /// <summary>
    /// Funcion que se ejecuta al tocar el botón de "restart"
    /// </summary>
    public void OnRestartButton()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    /// <summary>
    /// Funcion que se ejecuta al tocar el botón de "quit"
    /// </summary>
    public void OnQuitButton()
    {
        Application.Quit();
    }
}
