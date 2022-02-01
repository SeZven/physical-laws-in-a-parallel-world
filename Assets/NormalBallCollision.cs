using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBallCollision : MonoBehaviour
{
    public Rigidbody2D rb;
    public CircleCollider2D cc;

    public Vector2 lastVelocity;
    public float lastSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb.AddForce(new Vector2(10, 7), ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        lastVelocity = rb.velocity;
        lastSpeed = rb.velocity.magnitude;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Box")
        {
            // Reflexionsgesetz
            var speed = lastVelocity.magnitude;
            var direction = Vector2.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
            rb.velocity = direction * speed;
            // Impulserhaltung gilt auch, da Bewegungsenergie (kinetische Energie) im Ball unverändert und sich die Wand nicht bewegt
            

            //var massCollidingBall = 100000000000000;
            //var velocityCollidingBall = new Vector2(0, 0);
            //rb.velocity = (2 * massCollidingBall * velocityCollidingBall + (rb.mass - massCollidingBall) * lastVelocity) / (rb.mass + massCollidingBall);
            //rb.velocity = -lastVelocity;
        }

        else if (collision.collider.tag == "Ball")
        {
            //Debug.Log(collision.collider.attachedRigidbody.mass); // Masse des kollidierenden Objekts
            //Debug.Log(rb.mass); // Eigene Masse
            //Debug.Log(collision.collider.attachedRigidbody.velocity); // TODO Wahrscheinlich wird lastVelocity gebraucht !!! -> Lösung über relativeVelocity -> nicht die Lösung :(

            var massCollidingBall = collision.collider.attachedRigidbody.mass;
            var velocityCollidingBall = collision.collider.GetComponent<NormalBallCollision>().lastVelocity;
            //kinetische Energie berechnen (Vor der Kollision)
            // Muss nach der Kollision gleich sein

            var keneticEngeryBall = 0.5f * rb.mass * lastVelocity.magnitude;
            var keneticEnergyCollidingBall = 0.5f * massCollidingBall * velocityCollidingBall.magnitude;
            var keneticEnergy = keneticEngeryBall + keneticEnergyCollidingBall;
            keneticEnergy = Mathf.Round(keneticEnergy * 100f) / 100f;

            // Impulserhaltung (elastische Stoß)
            var newVelocity = (2 * massCollidingBall * velocityCollidingBall + (rb.mass - massCollidingBall) * lastVelocity) / (rb.mass + massCollidingBall);
             

            var newVelocityCollidingBall = (2 * rb.mass * lastVelocity + (massCollidingBall - rb.mass) * velocityCollidingBall) / (rb.mass + massCollidingBall);

            // Sonderfall: Kugeln gleicher Masse und gleicher Geschwindigkeit 
            //rb.velocity = -1 * lastVelocity; => funktioniert => Problem lastVeloity für colliding object wird benötigt!!!

            var newKeneticEnergyBall = 0.5f * rb.mass * newVelocity.magnitude;
            var newKeneticEnergyCollidingBall = 0.5f * massCollidingBall * newVelocityCollidingBall.magnitude;
            var newKeneticEnergy = newKeneticEnergyBall + newKeneticEnergyCollidingBall;
            newKeneticEnergy = Mathf.Round(newKeneticEnergy * 100f) / 100f;

            Debug.Log("Kenetic Energy before collision: " + keneticEnergy + "   Kenetic Energy after collision: " + newKeneticEnergy);
            Debug.Log("before == after : " + (keneticEnergy == newKeneticEnergy));

            rb.velocity = newVelocity;
   

        }

    }
}
