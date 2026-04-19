using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class WorldItem : MonoBehaviour
{
    public ItemColor color;

    private Vector3 startPosition;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        startPosition = transform.position;
    }

    public void ReturnToStart()
    {
        rb.velocity = Vector2.zero;
        transform.position = startPosition;
    }

    public void SetCorrect(Transform target)
    {
        rb.velocity = Vector2.zero;
        rb.simulated = false; // отключаем физику
        transform.position = target.position;
        enabled = false;
    }
}