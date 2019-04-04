using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StartScript : MonoBehaviour
{
    public GameObject karakter;
    public Button StartButton;
    public Avatar BackCrowling;
    public Animator karakterAnimator;
    float X= new float();
    float Y = new float();
    // Start is called before the first frame update
    void Start()
    {

    }
    void Update()
    {
  
    }
    public void Baslat()
    {
        //karakter.GetComponent<Rigidbody>().useGravity = true;
        //karakter.GetComponent<Rigidbody>().AddForce(new Vector3(50,0,0));
        StartButton.gameObject.SetActive(false);
    }
}
