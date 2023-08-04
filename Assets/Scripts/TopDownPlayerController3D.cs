using UnityEngine;

public class TopDownPlayerController3D : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // ����������� ������� ������
    }

    void Update()
    {
        // �������� �������� ���� ��������������� �� ������������� ����
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // ��������� ������ ���� �� ����� ��������� ������� ����
        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical).normalized;

        // ������ ������� ������ ����� � �������� ����
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
    }
}
