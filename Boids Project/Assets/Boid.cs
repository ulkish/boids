using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    [Header("Set Dynamically")]
    public Rigidbody rigid;
    
    // Use this for initialization
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();

        // Set a random initial position
        pos = Random.insideUnitSphere * Spawner.S.spawnRadius;

        // Set a random initial velocity
        Vector3 vel = Random.onUnitSphere * Spawner.S.velocity;
        rigid.velocity = vel;

        LookAhead();

        // Give the Boid a random color, but make sure it's not too dark
        Color randColor = Color.black;
        while ( randColor.r + randColor.g + randColor.b < 1.0f ) {
            randColor = new Color(Random.value, Random.value, Random.value);
        }
        Renderer[] rends = gameObject.GetComponentsInChildren<Renderer>();
        foreach ( Renderer r in rends ) {
            r.material.color = randColor;
        }
        TrailRenderer tRend = GetComponent<TrailRenderer>();
        tRend.material.SetColor("_TintColor", randColor);
    }

    void LookAhead()
    {
        // Orients the Boid to look at the direction it's flying.
        transform.LookAt(pos + rigid.velocity);
    }

    public Vector3 pos {
        get { return transform.position; }
        set { transform.position = value; }
    }
}
