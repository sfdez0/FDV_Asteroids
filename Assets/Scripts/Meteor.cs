using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    /// <summary>
    /// Tiempo maximo de vida para los meteoritos
    /// </summary>
    float maxLifeTime = 6;

    /// <summary>
    /// Varable que define si un meteorito puede dividirse al ser disparado 
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
}
