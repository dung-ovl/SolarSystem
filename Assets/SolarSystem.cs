using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SolarSystem : MonoBehaviour
{
    readonly float G = 100f;
    List<GameObject> celestials = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        this.celestials = GameObject.FindGameObjectsWithTag("Celestial").ToList();
        this.InitialVelocity();
    }

    private void InitialVelocity()
    {
        foreach (GameObject a in celestials)
        {
            foreach (GameObject b in celestials)
            {
                if (!a.Equals(b))
                {
                    float m2 = b.GetComponent<Rigidbody>().mass;
                    float r = Vector3.Distance(a.transform.position, b.transform.position);
                    a.transform.LookAt(b.transform);
                    float v = Mathf.Sqrt(G * m2 / r);
                    a.GetComponent<Rigidbody>().velocity += a.transform.right * v;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        this.Gravity();
    }

    private void Gravity()
    {
        foreach (GameObject a in celestials)
        {
            foreach (GameObject b in celestials)
            {
                if (!a.Equals(b))
                {
                    float m1 = a.GetComponent<Rigidbody>().mass;
                    float m2 = b.GetComponent<Rigidbody>().mass;
                    float r = Vector3.Distance(a.transform.position, b.transform.position);
                    Vector3 direction = (b.transform.position - a.transform.position).normalized;
                    float force = G * m1 * m2 / (r * r);
                    Vector3 gravity = force * direction;
                    a.GetComponent<Rigidbody>().AddForce(gravity);
                }
            }
        }
    }
}
