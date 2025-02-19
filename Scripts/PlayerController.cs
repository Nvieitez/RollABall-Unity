using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement; // Importar el namespace para manejar escenas

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private int count;
    private float MovementX;
    private float MovementY;
    public float speed = 10.0f; // Añadir una variable de velocidad pública
    public TextMeshProUGUI countText;
    public Transform respawnPoint; // Añadir una referencia al punto de respawn

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
    }

    void OnMove(InputValue value)
    {
        Vector2 inputVector = value.Get<Vector2>();
        MovementX = inputVector.x;
        MovementY = inputVector.y;
    }

    void SetCountText()
    {
        if (countText != null)
        {
            countText.text = "Puntuación: " + count.ToString();
            Debug.Log("Puntuación actualizada: " + count.ToString());
        }
        else
        {
            Debug.LogError("Count Text is not assigned in the Inspector.");
        }
    }

    void FixedUpdate()
    {
        if (States.currentState != States.GameState.Playing)
        {
            return;
        }

        Vector3 movement = new Vector3(MovementX, 0.0f, MovementY);
        rb.AddForce(movement * speed);

        // Obtener datos del acelerómetro
        Vector3 acceleration = Input.acceleration;
        Vector3 gyroMovement = new Vector3(acceleration.x, 0.0f, acceleration.y);
        rb.AddForce(gyroMovement * speed);

        // Verificar si el jugador se ha caído del mapa
        if (transform.position.y < -10)
        {
            Respawn();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    }

    public void DecreaseScore(int amount)
    {
        count -= amount;
        SetCountText();
        if (count <= 0)
        {
            Respawn();
        }
    }

    void Respawn()
    {
        // Reposicionar al jugador en el punto de respawn
        transform.position = respawnPoint.position;
        rb.linearVelocity = Vector3.zero; // Detener cualquier movimiento residual
    }
}