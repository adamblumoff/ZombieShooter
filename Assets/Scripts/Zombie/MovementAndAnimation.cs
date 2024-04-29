using UnityEngine;

public class MovementAndAnimation : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private Animator animator;
    private bool right = true;
   

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void Move(Vector2 targetPosition)
    {
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        HandleMovementAnimation(direction);

        // Flip the character to face the target
        if (targetPosition.x > transform.position.x && !right || targetPosition.x < transform.position.x && right)
        {
            Flip();
            right = !right;
        }
    }

    public void StopMovement()
    {

    }
    private void Flip()
    {
        transform.Rotate(0f, 180f, 0f);
    }

    private void HandleMovementAnimation(Vector2 direction)
    {
        bool isHorizontal = Mathf.Abs(direction.x) > Mathf.Abs(direction.y);
        bool isDown = direction.y < 0 && !isHorizontal;

        animator.SetBool("IsDown", isDown);
        animator.SetBool("IsHorizontal", isHorizontal);
    }

    public void TriggerHitAnimation()
    {
        animator.SetBool("IsHit", true);
    }

    public void StopHitAnimation()
    {
        animator.SetBool("IsHit", false);
    }

    public void TriggerDeathAnimation()
    {
        animator.SetBool("IsDead", true);
    }
}
