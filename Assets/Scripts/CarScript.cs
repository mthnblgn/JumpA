using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarScript : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 SpeedZ=new Vector3(0,0,0.005f);
    public GameControl GC;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //gameObject.GetComponent<Rigidbody>().velocity = gameObject.GetComponent<Rigidbody>().velocity - SpeedZ;
    }
}
