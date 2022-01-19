using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBallCollision : MonoBehaviour
{
    public Rigidbody2D rb;
    public CircleCollider2D cc;

    Vector2 lastVelocity;

    // Start is called before the first frame update
    void Start()
    {
        rb.AddForce(new Vector2(2, 4), ForceMode2D.Impulse);
        rb.angularVelocity = 1800;
    }

    // Update is called once per frame
    void Update()
    {
        lastVelocity = rb.velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Box")
        {
            // Reflexionsgesetz
            var speed = lastVelocity.magnitude;
            var direction = Vector2.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
            rb.velocity = direction * speed;
            // Impulserhaltung gilt auch, da Bewegungsenergie (kinetische Energie) im Ball unver‰ndert und sich die Wand nicht bewegt
        }

        else if (collision.collider.tag == "Ball")
        {
            Debug.Log(collision.collider.attachedRigidbody.mass); // Masse des kollidierenden Objekts
            Debug.Log(rb.mass); // Eigene Masse

            // Impulserhaltung (elastische Stoﬂ)
            var speed = lastVelocity.magnitude;
            var direction = Vector2.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
            rb.velocity = direction * speed;
        }

    }
}
