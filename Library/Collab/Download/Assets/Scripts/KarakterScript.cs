using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class KarakterScript : MonoBehaviour
{
    Rigidbody KarakterBody;
    Vector3 MouseOrigin = new Vector3();
    public Canvas c;
    public GameObject distance;
    bool BasladiMi = false;
    public GameObject Camera;
    Vector3 firstPositionChar;
    Vector3 firstPositionCam;
    public Button restartBtn;
    void Start()
    {
        KarakterBody = GetComponent<Rigidbody>();
        firstPositionChar = gameObject.transform.position;
        firstPositionCam = Camera.transform.position;
    }

    void Update()
    {
        if (BasladiMi)
        {
            #region Standalone
            if (Input.GetMouseButtonDown(0))
            {
                MouseOrigin = Input.mousePosition;
            }
            SingleAxisDragMovement(KarakterBody, 40, MouseOrigin);
            #endregion
            #region Mobile
            #endregion
            if (distance.transform.position.z < -19)
            {
                c.GetComponentInChildren<TextMeshProUGUI>().text = "Distance: " + ((KarakterBody.transform.position.z - distance.transform.position.z) / 4).ToString("0.00") + "m";
            }

        }
        Camera.transform.position = firstPositionCam - firstPositionChar + gameObject.transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "araba")
        {

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Araba")
        {
            BasladiMi = false;
            gameObject.GetComponent<Rigidbody>().AddForce((collision.relativeVelocity + new Vector3(0, 25, 0) * 30));
            gameObject.GetComponent<Rigidbody>().useGravity = true;
            Debug.Log(collision.impulse);
            restartBtn.gameObject.SetActive(true);
        }
    }
    void Oscillation(Rigidbody Body, float Hiz, float AltY, float UstY)
    {

        if (Body.transform.position.y <= AltY)
        {
            Body.velocity = new Vector3(0, Hiz, 0);
        }
        else if (Body.transform.position.y >= UstY)
        {
            Body.velocity = new Vector3(0, -Hiz, 0);

        }
    }
    public void Baslat()
    {
        BasladiMi = true;
    }
    void Surukle45(GameObject Suruklenecek, Vector3 MouseOrigin)
    {
        Vector3 vec = Input.mousePosition - MouseOrigin;
        Suruklenecek.transform.position = new Vector3(transform.position.x + (-vec.y * Mathf.Sin(Mathf.PI / 4) + vec.x * Mathf.Sin(Mathf.PI / 4)) / 100, transform.position.y, transform.position.z + (vec.x * Mathf.Sin(Mathf.PI / 4) + vec.y * Mathf.Sin(Mathf.PI / 4)) / 100);
    }
    void TiklayincaGenisleme2D(GameObject Alan, float hiz)
    {
        if (Input.GetMouseButton(0))
        {
            Alan.transform.localScale += new Vector3(.01f * hiz, .01f * hiz, 0);


        }
        else
        {
            Alan.transform.localScale = new Vector3(.1f, .1f, .1f);
        }
    }
    void SingleAxisDragMovement(Rigidbody b, float topSpeed, Vector3 MouseOrigin)
    {

        Vector3 InstantMousePosition = new Vector3();

        if (Input.GetMouseButton(0))
        {
            InstantMousePosition.x = Input.mousePosition.x;
            Vector3 siddetVektoru = new Vector3();
            siddetVektoru.x = InstantMousePosition.x - MouseOrigin.x;

            if (Mathf.Abs(siddetVektoru.x) <= topSpeed)
            {
                b.velocity = new Vector3(siddetVektoru.x/2, 0, 0);
            }
            else if (Mathf.Abs(siddetVektoru.x) > topSpeed)
            {
                b.velocity = new Vector3(topSpeed * Mathf.Sign(siddetVektoru.x)/2, 0, 0);
            }
        }
        else
        {
            float a = Mathf.Sign(KarakterBody.velocity.x);

            for (float i = Mathf.Abs(KarakterBody.velocity.x); i >= 0; i--)
            {
                Vector3 v = new Vector3(i * a, 0, 0);
                KarakterBody.velocity = v;
            }

        }
    }

}
