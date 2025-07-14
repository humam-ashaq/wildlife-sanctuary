using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AnimalWander : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float minMoveDuration = 1f;
    public float maxMoveDuration = 4f;
    public float minWaitDuration = 1f;
    public float maxWaitDuration = 2f;

    private Rigidbody2D rb;
    private Bounds movementBounds;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Ambil collider dari parent (kandang)
        Collider2D parentCollider = transform.parent?.GetComponent<Collider2D>();
        if (parentCollider != null)
        {
            movementBounds = parentCollider.bounds;
        }
        else
        {
            Debug.LogWarning("Parent tidak memiliki Collider2D untuk batas gerakan.");
            movementBounds = new Bounds(transform.position, Vector3.one * 3f); // fallback kecil
        }

        StartCoroutine(WanderRoutine());
    }

    private IEnumerator WanderRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(minWaitDuration, maxWaitDuration);
            // Debug.Log("Menunggu selama: " + waitTime);
            yield return new WaitForSeconds(waitTime);

            float moveTime = Random.Range(minMoveDuration, maxMoveDuration);
            Vector2 direction = Random.insideUnitCircle.normalized;
            // Debug.Log("Mulai bergerak ke: " + direction);

            float elapsed = 0f;
            while (elapsed < moveTime)
            {
                Vector2 newPos = rb.position + direction * moveSpeed * Time.fixedDeltaTime;

                // Pastikan dalam bounds kandang
                if (movementBounds.Contains(newPos))
                {
                    rb.MovePosition(newPos);
                }

                elapsed += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
        }
    }
}
