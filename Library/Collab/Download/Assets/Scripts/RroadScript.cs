using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RroadScript : MonoBehaviour
{
    public GameObject[] Cars;
    // Start is called before the first frame update
    void Start()
    {
        float carSpeed = Random.Range(10, 20);
        int rndHowManyCars = Random.Range(2, 5);
        float LastCarPosition = 140;
        for (int a = 0; a <= rndHowManyCars; a++)
        {
            int rndCar = Random.Range(0, Cars.Length-1);
            float Distance = Random.Range(10, 100);
            GameObject Car = Instantiate(Cars[rndCar], new Vector3(LastCarPosition - Distance, 2, gameObject.transform.position.z), Quaternion.Euler(0, 180, 0), gameObject.transform);
            Car.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            Car.GetComponent<Rigidbody>().velocity = new Vector3(carSpeed, 0, gameObject.GetComponent<Rigidbody>().velocity.z);
            LastCarPosition = Car.transform.position.x;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
