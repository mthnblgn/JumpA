using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LroadScript : MonoBehaviour
{
    public GameObject[] CarTypes;
    List<GameObject> Cars = new List<GameObject>();
    float carSpeed = new float();
    float sayac = 0;
    float rndSure = 1;
    float LCP = 0;
    void Start()
    {
        float YolUzunluk = this.GetComponent<BoxCollider>().size.x * gameObject.transform.localScale.x / 2;
        Rigidbody bod = this.GetComponent<Rigidbody>();
        LCP = YolUzunluk;
        float Distance = 0;
        carSpeed = Random.Range(9, 30);
        for (int i = 0; i < 5; i++)
        {
            int rndCar = Random.Range(0, CarTypes.Length);
            GameObject Car = Instantiate(CarTypes[rndCar], new Vector3(LCP - Distance, 2, gameObject.transform.position.z), Quaternion.Euler(0, -90, 0), gameObject.transform);
            Car.GetComponent<Rigidbody>().velocity = new Vector3(-carSpeed, 0, gameObject.GetComponent<Rigidbody>().velocity.z);
            LCP = Car.transform.position.x;
            if (-bod.velocity.z<=85)
            {
                Distance = Random.Range(10, 115+bod.velocity.z);
            }
            else
            {
                Distance = Random.Range(10, 25);
            }
            Cars.Add(Car);
        }
    }
    void Update()
    {
        int rndCar = Random.Range(0, CarTypes.Length);
        float LastCarPosition = gameObject.GetComponent<BoxCollider>().size.x * gameObject.transform.localScale.x / 2;
        sayac += Time.deltaTime;
        if (sayac >= rndSure)
        {
            GameObject Car = Instantiate(CarTypes[rndCar], new Vector3(LastCarPosition, 2, gameObject.transform.position.z), Quaternion.Euler(0, -90, 0), gameObject.transform);
            Car.GetComponent<Rigidbody>().velocity = new Vector3(-carSpeed, 0, gameObject.GetComponent<Rigidbody>().velocity.z);
            sayac = 0;
            Cars.Add(Car);
            rndSure = Random.Range(1, 10);
        }
        foreach (GameObject Car in Cars)
        {
            if (Car != null)
            {
                Car.transform.position = new Vector3(Car.transform.position.x, Car.transform.position.y, gameObject.transform.position.z);
            }
        }
    }
}
