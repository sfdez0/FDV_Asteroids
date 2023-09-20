using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    /// <summary>
    /// Velocidad de la bala
    /// </summary>
    public int speed = 10;

    /// <summary>
    /// Tiempo de vida maximo de la bala
    /// </summary>
    public float maxLifeTime = 3;

    /// <summary>
    /// Direccion de la bala
    /// </summary>
    private Vector3 targetVector;

    /// <summary>
    /// Spawner de balas
    /// </summary>
    GameObject bulletSpawner;

    /// <summary>
    /// Vector que contiene el cambio de tamaño de los meteoritos
    /// </summary>
    public Vector3 meteorScaleChange = new Vector3(-0.1f, -0.1f, -0.1f);

    /// <summary>
    /// Funcion Start
    /// </summary>
    void Start()
    {
        // Le damos un tiempo maximo de vida para salir de la pantalla
        Destroy(gameObject, maxLifeTime);

        // Obtenemos el objeto BulletSpawner
        bulletSpawner = GameObject.Find("BulletSpawner");

        // Obtenemos su direccion (rotacion eje Z), sera la misma para la bala
        targetVector = bulletSpawner.transform.right;
    }

    /// <summary>
    /// Funcion Update encargada de aplicar el movimiento a la bala
    /// </summary>
    private void Update()
    {
        // Movemos la bala en la direcion obtenida del BulletSpawner
        transform.Translate(targetVector * speed * Time.deltaTime);
    }

    /// <summary>
    /// Funcion para manejar las colisiones de la bala
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Si colisiona con un enemigo
        /*if (other.gameObject.tag == "Enemy")
        {
            // Destruimos enemigo y la propia bala
            Destroy(other.gameObject);
            Destroy(gameObject);

            // Incrementamos la puntuacion
            IncreaseScore();
        }*/

        if (other.gameObject.tag == "Enemy")
        {
            // Destruimos la bala
            Destroy(gameObject);

            // Incrementamos la puntuacion
            IncreaseScore();

            Meteor meteor = other.gameObject.GetComponent<Meteor>();
            if (meteor.canDivide)
            {
                Debug.Log("DIVIDE");
                // Instanciamos dos nuevos meteoritos m�s peque�os en diferentes rotaciones
                GameObject m1 = Instantiate(other.gameObject, transform.position, Quaternion.identity);
                Meteor meteor_1 = m1.GetComponent<Meteor>();
                meteor_1.canDivide = false;

                GameObject m2 = Instantiate(other.gameObject, transform.position, Quaternion.identity);
                Meteor meteor_2 = m2.GetComponent<Meteor>();
                meteor_2.canDivide = false;

                // Rotamos el primero 50�
                m1.transform.Rotate(new Vector3(0, 0, 1), 50f);
                m1.transform.localScale += meteorScaleChange;

                // Rotamos el segundo 310�
                m2.transform.Rotate(new Vector3(0, 0, 1), 310f);
                m1.transform.localScale += meteorScaleChange;

                // Destruimos el meteorito
                Destroy(other.gameObject);
            }
            else
            {
                // Destruimos el meteorito
                Destroy(other.gameObject);
            }
        }
    }

    /// <summary>
    /// Funcion que incrementa y actualiza los puntos del jugador
    /// </summary>
    private void IncreaseScore()
    {
        // Incrementamos los puntos
        Player.SCORE++;

        // Actualizamos el texto de los puntos
        UpdateScoreText();
    }

    /// <summary>
    /// Actualiza el texto en pantalla con la nueva puntuacion
    /// </summary>
    private void UpdateScoreText()
    {
        // Obtenemos el objeto del texto y modificamos su componente de texto
        GameObject UItext = GameObject.FindGameObjectWithTag("UI");
        UItext.GetComponent<Text>().text = "Puntos: " + Player.SCORE;
    }
}
