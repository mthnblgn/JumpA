using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kaldirimScript : MonoBehaviour
{
    public GameObject Duba;
    List<GameObject> Dubalar = new List<GameObject>();
    void Start()
    {
        float genislik = gameObject.GetComponent<BoxCollider>().size.x;
        int DubaAdet = Random.Range(1, 5);
        for (int i = 0; i < DubaAdet; i++)
        {
            GameObject D = Instantiate(Duba, new Vector3(Random.Range(-genislik / 2, genislik / 2) * 10, 1, gameObject.transform.position.z), Quaternion.identity, gameObject.transform);
            D.GetComponent<Rigidbody>().velocity = gameObject.GetComponent<Rigidbody>().velocity;
            Dubalar.Add(D);
        }
    }
    void Update()
    {
        foreach (var d in Dubalar)
        {
            if (d != null)
            {
                d.GetComponent<Rigidbody>().velocity = gameObject.GetComponent<Rigidbody>().velocity;
            }
        }
    }
}
