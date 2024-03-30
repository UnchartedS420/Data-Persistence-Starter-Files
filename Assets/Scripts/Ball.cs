using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    
    public MeshRenderer Renderer;
    private Rigidbody m_Rigidbody;
    private bool hasCollisionHappened = false;
    private float timer1; private float partyColorTime = 0.5f;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }
    void Update() {
        if (hasCollisionHappened && timer1 > 0) {
            timer1 -= Time.deltaTime;
            Material material = Renderer.material;
        material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        }else{
            hasCollisionHappened = false;
            timer1 = partyColorTime;
        }
    }
     void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Brick")) {
            timer1 = partyColorTime;
            hasCollisionHappened = true;
        }   
    }

    private void OnCollisionExit(Collision other)
    {
        var velocity = m_Rigidbody.velocity;
        
        //after a collision we accelerate a bit
        velocity += velocity.normalized * 0.01f;
        
        //check if we are not going totally vertically as this would lead to being stuck, we add a little vertical force
        if (Vector3.Dot(velocity.normalized, Vector3.up) < 0.1f)
        {
            velocity += velocity.y > 0 ? Vector3.up * 0.5f : Vector3.down * 0.5f;
        }

        //max velocity
        if (velocity.magnitude > 3.0f)
        {
            velocity = velocity.normalized * 3.0f;
        }

        m_Rigidbody.velocity = velocity;
    }
}
