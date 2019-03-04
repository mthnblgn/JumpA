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
    // Start is called before the first frame update
    void Start()
    {
        carSpeed = Random.Range(10, 20);
        //int rndHowManyCars = Random.Range(3, 5);
        //float LastCarPosition = -gameObject.GetComponent<BoxCollider>().size.x * gameObject.transform.localScale.x / 2;
        //for (int a = 0; a <= rndHowManyCars; a++)
        //{
        //    int rndCar = Random.Range(0, CarTypes.Length);
        //    float Distance = Random.Range(30, 45);
        //    float CarScale = CarTypes[rndCar].transform.localScale.x;
        //    GameObject Car = Instantiate(CarTypes[rndCar], new Vector3(LastCarPosition / CarScale + Distance, 2, gameObject.transform.position.z), Quaternion.Euler(0, 0, 0), gameObject.transform);
        //    LastCarPosition = Car.transform.position.x;
        //    Cars.Add(Car);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        int rndCar = Random.Range(0, CarTypes.Length);
        float CarScale = CarTypes[rndCar].transform.localScale.x;
        float LastCarPosition = gameObject.GetComponent<BoxCollider>().size.x * gameObject.transform.localScale.x / 2;
        sayac += Time.deltaTime;
        if (sayac >= rndSure)
        {
            GameObject Car = Instantiate(CarTypes[rndCar], new Vector3(LastCarPosition, 2, gameObject.transform.position.z), Quaternion.Euler(0, 0, 0), gameObject.transform);
            sayac = 0;
            Cars.Add(Car);
            rndSure = Random.Range(1, 5);
        }
        foreach (GameObject Car in Cars)
        {
            if (Car != null)
            {
                Car.GetComponent<Rigidbody>().velocity = new Vector3(-carSpeed, 0, gameObject.GetComponent<Rigidbody>().velocity.z);
            }
        }
    }
}
