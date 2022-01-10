using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallUpCollision : MonoBehaviour
{
    public Rigidbody2D rb;

    Vector2 lastVelocity;

    // Start is called before the first frame update
    void Start()
    {
        rb.AddForce(new Vector2(7, 10), ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        lastVelocity = rb.velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ball")
        {
            Debug.Log("Box collision");
            var speed = lastVelocity.magnitude;
            var direction = new Vector2(lastVelocity[0], 1);
            rb.velocity = direction * Mathf.Min(speed, 10f);

        }
    }
}
