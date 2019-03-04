using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StartScript : MonoBehaviour
{
    public GameObject karakter;
    public Button StartButton;
    float sayac=0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (StartButton.gameObject.active==false)
        {
            sayac += Time.deltaTime;
            if (sayac>2f)
            {
                SceneManager.LoadScene("StandardMode");
            }
        }
    }
    public void Baslat()
    {
        karakter.GetComponent<Rigidbody>().useGravity = true;
        karakter.GetComponent<Rigidbody>().AddForce(new Vector3(50,400,-50));
        StartButton.gameObject.SetActive(false);
    }
}
