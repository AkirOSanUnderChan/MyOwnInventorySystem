using UnityEngine;

public class TopDownPlayerController3D : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // «аборон€Їмо поворот гравц€
    }

    void Update()
    {
        // ќтримуЇмо значенн€ осей горизонтального та вертикального руху
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // —творюЇмо вектор руху на основ≥ отриманих значень осей
        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical).normalized;

        // «м≥щуЇмо позиц≥ю гравц€ зг≥дно з вектором руху
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
    }
}
