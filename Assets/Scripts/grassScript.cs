using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grassScript : MonoBehaviour
{
    public GameObject[] envObjs;
    void Start()
    {
        int HowManyEnvObjs = Random.Range(2, 5);
        float LastEnvObjPos = 140;
        for (int x = 0; x < HowManyEnvObjs; x++)

        {
            float rndRotY = Random.Range(0, 360);


            int rndEnvObj = Random.Range(0, envObjs.Length - 1);
            float Distance = Random.Range(30, 70);
            GameObject EnvObj = Instantiate(envObjs[rndEnvObj], new Vector3(LastEnvObjPos - Distance, envObjs[rndEnvObj].transform.position.y, gameObject.transform.position.z), Quaternion.Euler(0, rndRotY, 0), gameObject.transform);

            LastEnvObjPos = EnvObj.transform.position.x;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
