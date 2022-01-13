using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollisonWithBox : MonoBehaviour
{
    public Rigidbody2D rb;

    Vector2 lastVelocity;

    // Update is called once per frame
    void Update()
    {
        lastVelocity = rb.velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Box")
        {
            var speed = lastVelocity.magnitude;
            var direction = Vector2.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
            rb.velocity = direction * Mathf.Min(speed, 10f);
        }
    }
}
