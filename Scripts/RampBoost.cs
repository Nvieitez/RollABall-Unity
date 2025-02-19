using UnityEngine;

public class RampBoost : MonoBehaviour
{
    // Fuerza del empujón
    public float boostForce = 10.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        // Comprobar si el objeto que ha colisionado es el jugador
        if (collision.gameObject.CompareTag("Player"))
        {
            // Obtener el componente Rigidbody del jugador
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();

            if (playerRb != null)
            {
                // Aplicar un empujón al jugador en la dirección de la rampa
                Vector3 boostDirection = transform.up; // Puedes ajustar la dirección según la orientación de la rampa
                playerRb.AddForce(boostDirection * boostForce, ForceMode.Impulse);
            }
        }
    }
}