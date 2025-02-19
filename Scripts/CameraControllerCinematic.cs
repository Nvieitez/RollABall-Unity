// filepath: /C:/Users/nicoX/Rollaball/Assets/Scripts/CameraControllerCinematic.cs
using UnityEngine;

public class CameraControllerCinematic : MonoBehaviour
{
    // Punto central alrededor del cual la cámara girará
    public Transform centerPoint;
    // Radio de la órbita
    public float orbitRadius = 10.0f;
    // Velocidad angular de la cámara
    public float orbitSpeed = 10.0f;
    // Altura de la cámara
    public float height = 5.0f;

    private float angle = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (centerPoint == null)
        {
            Debug.LogError("Center point is not assigned in the CameraControllerCinematic script.");
            return;
        }

        // Colocar la cámara en la posición inicial de la órbita
        UpdateCameraPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (centerPoint == null)
        {
            return;
        }

        // Incrementar el ángulo para mover la cámara
        angle += orbitSpeed * Time.deltaTime;
        if (angle >= 360.0f)
        {
            angle -= 360.0f;
        }

        // Actualizar la posición de la cámara
        UpdateCameraPosition();
    }

    void UpdateCameraPosition()
    {
        // Calcular la nueva posición de la cámara en la órbita
        float x = centerPoint.position.x + orbitRadius * Mathf.Cos(angle * Mathf.Deg2Rad);
        float z = centerPoint.position.z + orbitRadius * Mathf.Sin(angle * Mathf.Deg2Rad);
        float y = centerPoint.position.y + height;

        // Establecer la nueva posición y rotación de la cámara
        transform.position = new Vector3(x, y, z);
        transform.LookAt(centerPoint);
    }
}