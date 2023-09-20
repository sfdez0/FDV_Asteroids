using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    /// <summary>
    /// Velocidad
    /// </summary>
    public float thrustForce = 5f;

    /// <summary>
    /// Velocidad de rotacion de la nave
    /// </summary>
    public float rotationSpeed = 120f;

    /// <summary>
    /// Direccion de empuje de la nave
    /// </summary>
    private Vector2 thrustDirection;

    /// <summary>
    /// Spawner de balas
    /// </summary>
    public GameObject bulletSpawner;

    /// <summary>
    /// Prefab de bala
    /// </summary>
    public GameObject bulletPrefab;

    /// <summary>
    /// Rigidbody de la nave
    /// </summary>
    private Rigidbody2D _rigidbody;

    /// <summary>
    /// Puntuacion del jugador
    /// </summary>
    public static int SCORE = 0;

    /// <summary>
    /// Limite del mundo X
    /// </summary>
    public float xBorderLimit = 7.49f;

    /// <summary>
    /// Limite del mundo Y
    /// </summary>
    public float yBorderLimit = 4.0f;

    /// <summary>
    /// Funcion Start
    /// </summary>
    void Start()
    {
        // rigidbody nos permite aplicar fuerzas en el jugador
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// FixedUpdate encargado del movimiento de la nave, es independiente de los FPS
    /// </summary>
    private void FixedUpdate()
    {
        // Obtenemos las pulsaciones de teclado
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        float thrust = Input.GetAxis("Vertical") * thrustForce;

        // La dirección de empuje es .right (el eje X positivo)
        thrustDirection = transform.right;

        // Rotamos con el eje "Rotate" negativo para que la dirección sea correcta
        transform.Rotate(Vector3.forward, -rotation);

        // Añadimos la fuerza capturada arriba a la nave del jugador
        _rigidbody.AddForce(thrust * thrustDirection);
    }

    /// <summary>
    /// Update encargado del control del disparo
    /// </summary>
    private void Update()
    {
        // Comprobamos si supera los limites del mundo
        CheckBorderLimits();

        // Comprobamos si presiona el espacio para disparar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    /// <summary>
    /// Si el jugador colisiona con un meteorito reiniciamos la escena
    /// </summary>
    /// <param name="other"></param> Valor de la colision
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Si colisiona con un enemigo
        if (other.gameObject.tag == "Enemy")
        {
            // Reseteamos puntuacion
            SCORE = 0;

            // Reseteamos la escena
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    /// <summary>
    /// Funcion que comprueba si el jugador supera un limite del mundo y 
    /// lo teleporta al limite contrario
    /// </summary>
    private void CheckBorderLimits()
    {
        bool hasChanged = false;
        Vector3 actualPos = transform.position;

        if (actualPos.x > xBorderLimit)
        {
            actualPos.x = -xBorderLimit + 1;
            hasChanged = true;
        }
        else if (actualPos.x < -xBorderLimit)
        {
            actualPos.x = xBorderLimit - 1;
            hasChanged = true;
        }
        else if (actualPos.y > yBorderLimit)
        {
            actualPos.y = -yBorderLimit + 1;
            hasChanged = true;
        }
        else if (actualPos.y < -yBorderLimit)
        {
            actualPos.y = yBorderLimit - 1;
            hasChanged = true;
        }

        // Aplicamos la nueva posicion en caso de realizarse un teleport
        if (hasChanged)
        {
            transform.position = actualPos;
        }
    }

    /// <summary>
    /// Funcion de disparo
    /// </summary>
    private void Shoot()
    {
        // Instanciamos la bala en la posicion del BulletSpawner
        Instantiate(bulletPrefab, bulletSpawner.transform.position, Quaternion.identity);
    }
}
