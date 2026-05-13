using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;

    [Header("Field Limit")]
    public Vector2 xLimit = new Vector2(-20, 20);
    public Vector2 zLimit = new Vector2(-10, 10);

    private CharacterController controller;
    private Animator animator;

    private Vector3 moveDir;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        moveDir = new Vector3(h, 0, v).normalized;

        controller.Move(moveDir * moveSpeed * Time.deltaTime);

        if (moveDir != Vector3.zero)
        {
            transform.forward = moveDir;
        }

        Vector3 pos = transform.position;

        pos.x = Mathf.Clamp(pos.x, xLimit.x, xLimit.y);
        pos.z = Mathf.Clamp(pos.z, zLimit.x, zLimit.y);

        transform.position = pos;

        animator.SetFloat("Speed", moveDir.magnitude);
    }
}