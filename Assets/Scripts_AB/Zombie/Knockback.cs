using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Knockback : MonoBehaviour
{
    public float strength = 16f;
    public float delay = 0.15f;
    public UnityEvent OnBegin, OnDone;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Ensure the zombie has a Rigidbody2D component.
    }

    public void HitBackwards(Vector3 senderPosition)
    {
        StopAllCoroutines();
        OnBegin?.Invoke();

        Vector2 direction = (transform.position - senderPosition).normalized;
        rb.AddForce(direction * strength, ForceMode2D.Impulse);

        StartCoroutine(Reset());
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(delay);
        rb.velocity = Vector2.zero;
        OnDone?.Invoke();
    }
}
