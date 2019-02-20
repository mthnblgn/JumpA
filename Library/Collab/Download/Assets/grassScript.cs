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
        for (int x = 0; x < 10; x++)
        
        {
            float rndRotY = Random.Range(0, 360);
            float rndScX = Random.Range(2, 10);
            float rndScY = Random.Range(5, 13);
            float rndScZ = Random.Range(2, 10);

            int rndEnvObj = Random.Range(0, envObjs.Length-1);
            float Distance = Random.Range(20, 30);
            GameObject EnvObj = Instantiate(envObjs[rndEnvObj], new Vector3(LastEnvObjPos - Distance, 2, gameObject.transform.position.z), Quaternion.Euler(0,rndRotY,0), gameObject.transform);
            EnvObj.transform.localScale = new Vector3(rndScX, rndScY, rndScZ);
            LastEnvObjPos = EnvObj.transform.position.x;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
