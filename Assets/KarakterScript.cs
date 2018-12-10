using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarakterScript : MonoBehaviour
{
    public GameObject Alan;
    public GameObject Nereye;
    Rigidbody KarakterBody;
    Rigidbody NereyeBody;
    bool CalistiMi = false;
    Vector3 MouseOrigin = new Vector3();
    public GameObject gameControl;

    void Start()
    {
        KarakterBody = GetComponent<Rigidbody>();
        NereyeBody = Nereye.GetComponent<Rigidbody>();
        gameControl.GetComponent<GameControl>().;

        
    }

    void Update()
    {

        YukariAsagiSalinim(KarakterBody, .3f, 1.2f, 1.6f);
        if (Input.touchSupported)
        {

        }
        else
        {

            if (Input.GetMouseButton(0))
            {
                Alan.transform.localScale += new Vector3(.1f, .1f, 0);

                
            }
            else
            {
                Alan.transform.localScale = new Vector3(.1f, .1f, .1f);
            }
        }


        if (Input.GetMouseButtonDown(0) && CalistiMi == false)
        {
            MouseOrigin = Input.mousePosition;
            CalistiMi = true;
        }
        else if (Input.GetMouseButtonUp(0) && CalistiMi == true)
        {
            CalistiMi = false;
        }

    }
    void OnMouseDrag()
    {
        Vector3 vec = Input.mousePosition - MouseOrigin;
        Nereye.transform.position = new Vector3(transform.position.x+(-vec.y * Mathf.Sin(Mathf.PI / 4)+vec.x * Mathf.Sin(Mathf.PI / 4))/100, transform.position.y, transform.position.z + (vec.x * Mathf.Sin(Mathf.PI / 4) + vec.y*Mathf.Sin(Mathf.PI/4))/100);
        Debug.Log("drag");
    }

    void YukariAsagiSalinim(Rigidbody Body, float Hiz, float AltY, float UstY)
    {
        if (this.transform.position.y <= AltY)
        {
            Body.velocity = new Vector3(0, Hiz, 0);
        }
        else if (this.transform.position.y >= UstY)
        {
            Body.velocity = new Vector3(0, -Hiz, 0);

        }


    }
}
