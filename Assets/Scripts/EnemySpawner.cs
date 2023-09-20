using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    /// <summary>
    /// Prefab del asteroide
    /// </summary>
    public GameObject asteroidPrefab;

    /// <summary>
    /// Variable que contiene la velocidad de spawn de los meteoritos
    /// </summary>
    public float spawnRatePerMinute = 30;

    /// <summary>
    /// Variable usada para aumentar la velocidad de spawn de los meteoritos
    /// </summary>
    public float spawnRateIncrement = 1;

    /// <summary>
    /// Limite de spawn X
    /// </summary>
    public float xBorderLimit = 7.49f;

    /// <summary>
    /// Limite de spawn Y
    /// </summary>
    public float yBorderLimit = 4.0f;

    /// <summary>
    /// Variable para controlar el spawn de los meteoritos
    /// </summary>
    private float spawnNext = 0;

    /// <summary>
    /// Funcion Update encargada de spawnear los enemigos
    /// </summary>
    void Update()
    {
        // instanciamos enemigos s�lo si ha pasado tiempo suficiente desde el �ltimo.
        if (Time.time > spawnNext)
        {
            // indicamos cu�ndo podremos volver a instanciar otro enemigo
            spawnNext = Time.time + 60 / spawnRatePerMinute;
            // con cada spawn hay mas asteroides por minuto para incrementar la dificultad
            spawnRatePerMinute += spawnRateIncrement;
            // guardamos un punto aleatorio entre las esquinas superiores de la pantalla
            float rand = Random.Range(-xBorderLimit, xBorderLimit);

            // Creamos la posicion de spawn, fija en "z = 2", con "y" siempre positiva
            Vector3 spawnPosition = new Vector3(rand, yBorderLimit, 2.0f);

            // Instanciamos el nuevo meteorito
            Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
