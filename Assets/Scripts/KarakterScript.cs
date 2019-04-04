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
    public GameControl GameControl;
    public Button restartBtn;
    Animator AlienAnimator;
    public GameObject Destroyer;
    public GameObject KameraIsaret;
    public GameObject MovementGameObject;
    bool DustuMu = false;
    bool animaB = false;
    void Start()
    {
        AlienAnimator = gameObject.GetComponent<Animator>();
        KarakterBody = GetComponentInParent<Rigidbody>();
        firstPositionChar = new Vector3(0, .2f, 3.9f);
        firstPositionCam = Camera.transform.position;
    }
    void Update()
    {
        if (gameObject.transform.position.y <= 2 && DustuMu == false)
        {
            BasladiMi = true;
            DustuMu = true;
        }
        if (DustuMu && BasladiMi)
        {
            #region Standalone
            MovementByTouchWORigidBody(3);
            #endregion
            c.GetComponentInChildren<TextMeshProUGUI>().text = "Distance: " + ((KarakterBody.transform.position.z - distance.transform.position.z) / 4).ToString("0.00") + "m";
        }
    }
    public void Baslat()
    {
        BasladiMi = true;
    }
    public void AgacaCarp(Collider col)
    {
        BasladiMi = false;
        Destroyer.SetActive(false);
        restartBtn.gameObject.SetActive(true);
        gameObject.GetComponent<Animator>().runtimeAnimatorController = null;
    }
    public void ArabayaCarp(Collision collision, Rigidbody RB)
    {
        print("a");
        Destroyer.SetActive(false);
        BasladiMi = false;
        restartBtn.gameObject.SetActive(true);
        RB.AddForce((collision.relativeVelocity + new Vector3(0, 50, 0)));
        gameObject.GetComponent<Animator>().runtimeAnimatorController = null;
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
                b.velocity = new Vector3(siddetVektoru.x / 2, 0, 0);
            }
            else if (Mathf.Abs(siddetVektoru.x) > topSpeed)
            {
                b.velocity = new Vector3(topSpeed * Mathf.Sign(siddetVektoru.x) / 2, 0, 0);
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
    void MovementByTouch(Rigidbody body, float speed)
    {
        if (Input.touchCount > 0)
        {
            Vector3 speedVec = new Vector3(0.1f*speed, 0, 0);
            Quaternion Rot = new Quaternion(0, 3 * speed, 0, 0);
            Quaternion NRot = new Quaternion(0, -3 * speed, 0, 0);
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                if (touch.position.x < Screen.width / 2)
                {
                    body.transform.position -= speedVec;
                    body.transform.rotation = Rot;
                }
                else if(touch.position.x > Screen.width / 2)
                {
                    body.transform.position += speedVec;
                    body.transform.rotation = NRot;
                }
            }
        }
        else
        {
            Vector3 speedVec = new Vector3(0.1f * speed, 0, 0);
            Quaternion Rot = new Quaternion(0, 3 * speed, 0, 0);
            Quaternion NRot = new Quaternion(0, -3 * speed, 0, 0);
            if (Input.GetMouseButton(0))
            {
                if (Input.mousePosition.x < Screen.width / 2)
                {
                    body.transform.position -= speedVec;
                    body.transform.rotation = Rot;
                }
                else if (Input.mousePosition.x > Screen.width / 2)     
                {
                    body.transform.position += speedVec;
                    body.transform.rotation = NRot;
                }
            }
        }
    }
    void MovementByTouchWORigidBody(float speed)
    {
        if (Input.touchCount > 0)
        {
            Vector3 speedVec = new Vector3(0.1f * speed, 0, 0);
            Quaternion Rot = new Quaternion(0, 3 * speed, 0, 0);
            Quaternion NRot = new Quaternion(0, -3 * speed, 0, 0);
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                if (touch.position.x < Screen.width / 2)
                {
                    this.transform.position -= speedVec;
                    this.transform.rotation = Rot;
                }
                else if (touch.position.x > Screen.width / 2)
                {
                    this.transform.position += speedVec;
                    this.transform.rotation = NRot;
                }
            }
        }
        else
        {
            Vector3 speedVec = new Vector3(0.1f * speed, 0, 0);
            Quaternion Rot = Quaternion.Euler(0, 5 * speed, 0);
            Quaternion NRot = Quaternion.Euler(0, -5 * speed, 0);
            if (Input.GetMouseButton(0))
            {
                if (Input.mousePosition.x < Screen.width / 2)
                {
                    this.transform.position -= speedVec;
                    this.transform.rotation = NRot;
                }
                else if (Input.mousePosition.x > Screen.width / 2)
                {
                    this.transform.position += speedVec;
                    this.transform.rotation = Rot;
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                this.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }
    void MovementByTouchWORigidBody(GameObject gameobject,float speed)
    {
        if (Input.touchCount > 0)
        {
            Vector3 speedVec = new Vector3(0.1f * speed, 0, 0);
            Quaternion Rot = new Quaternion(0, 3 * speed, 0, 0);
            Quaternion NRot = new Quaternion(0, -3 * speed, 0, 0);
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                if (touch.position.x < Screen.width / 2)
                {
                    gameobject.transform.position -= speedVec;
                    gameobject.transform.rotation = Rot;
                }
                else if (touch.position.x > Screen.width / 2)
                {
                    gameobject.transform.position += speedVec;
                    gameobject.transform.rotation = NRot;
                }
            }
        }
        else
        {
            Vector3 speedVec = new Vector3(0.1f * speed, 0, 0);
            Quaternion Rot = new Quaternion(0, 3 * speed, 0, 0);
            Quaternion NRot = new Quaternion(0, -3 * speed, 0, 0);
            if (Input.GetMouseButton(0))
            {
                if (Input.mousePosition.x < Screen.width / 2)
                {
                    gameobject.transform.position -= speedVec;
                    gameobject.transform.rotation = Rot;
                }
                else if (Input.mousePosition.x > Screen.width / 2)
                {
                    gameobject.transform.position += speedVec;
                    gameobject.transform.rotation = NRot;
                }
            }
        }
    }

}
