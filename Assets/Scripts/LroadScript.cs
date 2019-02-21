using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LroadScript : MonoBehaviour
{
    public GameObject[] CarTypes;
    List<GameObject> Cars = new List<GameObject>();
    float carSpeed = new float();
    // Start is called before the first frame update
    void Start()
    {
        carSpeed = Random.Range(10, 20);
        int rndHowManyCars = Random.Range(2, 5);
        float LastCarPosition = -140;
        for (int a = 0; a <= rndHowManyCars; a++)
        {
            int rndCar = Random.Range(0, CarTypes.Length);
            float Distance = Random.Range(10, 100);
            GameObject Car = Instantiate(CarTypes[rndCar], new Vector3(LastCarPosition + Distance, 2, gameObject.transform.position.z), Quaternion.Euler(0, 0, 0), gameObject.transform);
            LastCarPosition = Car.transform.position.x;
            Cars.Add(Car);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var Car in Cars)
        {
            if (Car != null)
            {
                Car.GetComponent<Rigidbody>().velocity = new Vector3(-carSpeed, 0, gameObject.GetComponent<Rigidbody>().velocity.z);

            }
        }
    }
}
