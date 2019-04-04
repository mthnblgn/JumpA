using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public KarakterScript k;
    public Rigidbody RB;  
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Agac")
        {
            k.AgacaCarp(other);
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "Araba")
        {
            k.AgacaCarp(other);
            Destroy(gameObject);
            gameObject.GetComponentInParent<Rigidbody>().AddForce(new Vector3(0, 100, 0));
        }
    }
}
