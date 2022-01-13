using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBallCollision : MonoBehaviour
{
    public Rigidbody2D rb;
    public CircleCollider2D cc;

    Vector2 lastVelocity;
    float lastAngularVelocity;

    // Start is called before the first frame update
    void Start()
    {
        rb.AddForce(new Vector2(2, 4), ForceMode2D.Impulse);
        rb.angularVelocity = 13021;
    }

    // Update is called once per frame
    void Update()
    {
        lastVelocity = rb.velocity;
        lastAngularVelocity = rb.angularVelocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Box" || collision.collider.tag == "Ball")
        {
            // Reflexionsgesetz
            var speed = lastVelocity.magnitude;
            var direction = Vector2.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
            rb.velocity = direction * speed;
            // Impulserhaltung gilt auch, da Bewegungsenergie (kinetische Energie) im Ball unverändert und sich die Wand nicht bewegt

            // Erhaltung des Drehimpuls
            var drehzahl = lastAngularVelocity / 360; // ? Drehzahl = Drehgeschwindikeit / 360, da eine Umdrehung = 360
            // winkelgewschindikeit = 2*pi * n => n = 2*pi / winkelgeschwindigkeit
            var umlaufzeit = 1 / drehzahl; // Umlaufzeit = 1 / Drehzahl
            var traegheitsmoment = rb.mass * cc.radius * cc.radius; // Trägheitsmoment = Masse * Radius^2
            var winkelgeschwindikeit = 2 * Mathf.PI / umlaufzeit; // Winkelgeschwindigkeit = 2*pi / Umlaufszeit
            rb.angularVelocity = traegheitsmoment * winkelgeschwindikeit;

            // Geschwindigkeit und Drehimpuls aufeinander abstimmen

        }
    }
}
