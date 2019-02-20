using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameControl : MonoBehaviour
{
    public GameObject rRoad;
    public GameObject lRoad;
    public GameObject Cimen;
    public GameObject Kaldirim;
    public GameObject Isaret;
    public float oyunHizi;
    GameObject[] Baslangiclar = new GameObject[19];
    float sonuncuYer = 0;
    string siraHangisinde = "Yol";
    bool Baslangic = false;
    bool BasliadiMi = false;
    bool SeritYonu = true;
    public Button startBtn;
    public GameObject distance;


    void Start()
    {
        Vector3 OyunHizVektoru = new Vector3(0, 0, -oyunHizi);
        for (int i = 0; i < 18; i++)
        {
            GameObject a = Instantiate(Cimen, new Vector3(0, 0, -29 + i * 4), Quaternion.identity);
            Baslangiclar[i] = a;
        }
        Baslangiclar[18] = Instantiate(Kaldirim, new Vector3(0, 0, 42), Quaternion.identity);



    }
    void Update()
    {
        Vector3 OyunHizVektoru = new Vector3(0, 0, -oyunHizi);
        if (BasliadiMi)
        {
            if (!Baslangic)
            {
                foreach (var b in Baslangiclar)
                {
                    b.GetComponent<Rigidbody>().velocity = OyunHizVektoru;
                }
                distance.GetComponent<Rigidbody>().velocity = OyunHizVektoru;
                Isaret.GetComponent<Rigidbody>().velocity = OyunHizVektoru;
                Baslangic = true;
            }

            #region YolCimenlikRandomizasyonu
            //**Cimen**
            if (siraHangisinde == "Cimen" && Isaret.transform.position.z <= -30 - sonuncuYer)
            {

                int rndCimen = Random.Range(1, 15);
                for (int i = 0; i <= rndCimen; i++)
                {
                    GameObject cimenInstace = Instantiate(Cimen, new Vector3(0, 0, 45 + i * 4), Quaternion.identity);
                    cimenInstace.GetComponent<Rigidbody>().velocity = OyunHizVektoru;
                    if (i == rndCimen)
                    {
                        sonuncuYer = (i + 1) * 4;
                        siraHangisinde = "Yol";
                        Isaret.transform.position = new Vector3(0, 0, -30);
                    }
                }
                //**Kaldirim**

                Instantiate(Kaldirim, new Vector3(0, 0, 44 + sonuncuYer), Quaternion.identity).GetComponent<Rigidbody>().velocity = OyunHizVektoru;
                sonuncuYer = sonuncuYer + 2;
            }
            //**Yol**
            if (siraHangisinde == "Yol" && Isaret.transform.position.z <= -30 - sonuncuYer)
            {
                int rndYol = Random.Range(1, 15);
                if (SeritYonu)
                {
                    for (int i = 0; i <= rndYol; i++)
                    {

                        GameObject yolInstace = Instantiate(rRoad, new Vector3(0, 0, 45 + i * 4), Quaternion.identity);
                        yolInstace.GetComponent<Rigidbody>().velocity = OyunHizVektoru;
                        if (i == rndYol)
                        {
                            sonuncuYer = (i + 1) * 4;
                            siraHangisinde = "Cimen";
                            Isaret.transform.position = new Vector3(0, 0, -30);
                        }
                    }
                    SeritYonu = !SeritYonu;
                }
                else
                {
                    for (int i = 0; i <= rndYol; i++)
                    {

                        GameObject yolInstace = Instantiate(lRoad, new Vector3(0, 0, 45 + i * 4), Quaternion.identity);
                        yolInstace.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -oyunHizi);
                        if (i == rndYol)
                        {
                            sonuncuYer = (i + 1) * 4;
                            siraHangisinde = "Cimen";
                            Isaret.transform.position = new Vector3(0, 0, -30);
                        }
                    }
                    SeritYonu = !SeritYonu;
                }
                //**Kaldirim**

                Instantiate(Kaldirim, new Vector3(0, 0, 44 + sonuncuYer), Quaternion.identity).GetComponent<Rigidbody>().velocity = OyunHizVektoru;
                sonuncuYer = sonuncuYer + 2;
            }
            #endregion
        }
    }
    public void Baslat()
    {
        BasliadiMi = true;
        if (Input.GetMouseButtonUp(0))
        {
            startBtn.gameObject.SetActive(false);
        }
    }
    float RandomSayidaOlusturVeDiz_Z(GameObject Olusturulacak, Vector3 Nerede, int minAdet, int maxAdet)
    {
        float sonuncuYer = 0;
        int rnd = Random.Range(minAdet, maxAdet);
        float genislikZ = Olusturulacak.GetComponent<MeshCollider>().bounds.size.z;
        for (int i = 0; i <= rnd; i++)
        {
            Instantiate(Olusturulacak, new Vector3(Nerede.x, Nerede.y, Nerede.z + i * genislikZ), Quaternion.identity).GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -oyunHizi);
            if (i == rnd)
            {
                sonuncuYer = i * 4;
            }
        }
        return sonuncuYer;
    }
    public void YenidenBaslat()
    {
        SceneManager.LoadScene("StandardMode", LoadSceneMode.Single);

    }
}
