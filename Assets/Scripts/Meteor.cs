/**
 * Autor: Sergio Fernández Verdugo
 */

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    /// <summary>
    /// Tiempo maximo de vida para los meteoritos
    /// </summary>
    float maxLifeTime = 7;

    /// <summary>
    /// Define si el meteorito puede dividirse en otros tras su destruccion
    /// </summary>
    public bool canDivide = true;

    /// <summary>
    /// Funcion Start
    /// </summary>
    void Start()
    {
        // Le damos un tiempo maximo de vida para salir de la pantalla
        Destroy(gameObject, maxLifeTime);
    }

    /// <summary>
    /// Update que cambia la trayectoria de los meteoritos divididos
    /// </summary>
    private void Update()
    {
        // Comprobamos que sea un meteorito dividido de otro anterior
        if (canDivide == false)
        {
            // Movemos la bala en la nueva direccion
            transform.Translate(-transform.up * 2 * Time.deltaTime);
        }
    }
}
