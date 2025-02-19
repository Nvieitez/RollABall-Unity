using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player; // Referencia al jugador
    public float speed = 5.0f; // Velocidad de movimiento del enemigo
    public PlayerController playerScript; // Referencia al script del jugador

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component is missing from the enemy.");
        }

        if (playerScript == null)
        {
            Debug.LogError("Player script reference is missing.");
        }
    }

    void Update()
    {
        if (States.currentState != States.GameState.Playing)
        {
            return;
        }

        // Perseguir al jugador
        Vector3 direction = player.position - transform.position;
        direction.Normalize();
        rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Comprobar si el enemigo ha colisionado con el jugador
        if (collision.gameObject.CompareTag("Player"))
        {
            // Restar un punto al jugador
            playerScript.DecreaseScore(1);
        }
    }
}