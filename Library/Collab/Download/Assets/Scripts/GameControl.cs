using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Data;
using System.Data.SqlClient;



public class GameControl : MonoBehaviour
{
    public GameObject rRoad;
    public GameObject lRoad;
    public GameObject Cimen;
    public GameObject Kaldirim;
    public GameObject lYanYol;
    public GameObject rYanYol;
    public GameObject Isaret;
    GameObject[] Baslangiclar = new GameObject[19];
    float SinirNoktaZ = 0;
    float EnUzakNoktaZ = 0;
    string siraHangisinde = "Yol";
    bool Baslangic = false;
    public bool BasliadiMi = false;
    bool SeritYonu = true;
    public Button startBtn;
    public GameObject distance;
    public Vector3 GameSpeedVec;
    private string connectionString;
    List<GameObject> GameList = new List<GameObject>();
    int GameListSayac = 0;
    float YolUzunluk = new float();
    float CimenUzunluk = new float();
    float KaldirimUzunluk = new float();
    void Start()
    {
        YolUzunluk = rRoad.GetComponent<BoxCollider>().size.z * rRoad.transform.localScale.z;
        CimenUzunluk = Cimen.GetComponent<BoxCollider>().size.z * Cimen.transform.localScale.z;
        KaldirimUzunluk = Kaldirim.GetComponent<BoxCollider>().size.z * Kaldirim.transform.localScale.z;
        for (int i = 0; i < 18; i++)
        {
            GameObject a = Instantiate(Cimen, new Vector3(0, 0, i * CimenUzunluk), Quaternion.identity);
            Baslangiclar[i] = a;
            SinirNoktaZ = a.transform.position.z + CimenUzunluk / 2;
        }

        Baslangiclar[18] = Instantiate(Kaldirim, new Vector3(0, 0, KaldirimUzunluk * .5f + SinirNoktaZ), Quaternion.identity);
        SinirNoktaZ += KaldirimUzunluk;
    }
    void Update()
    {
        #region Sahne
        if (SinirNoktaZ <= 200)
        {


            if (siraHangisinde == "Yol")
            {

                int rndYol = Random.Range(1, 5);
                if (SeritYonu)
                {
                    for (int i = 0; i <= rndYol; i++)
                    {

                        GameObject yolInstace = Instantiate(rRoad, new Vector3(0, 0, SinirNoktaZ + i * YolUzunluk + YolUzunluk * .5f), Quaternion.identity);
                        GameList.Add(yolInstace);
                        if (i == rndYol)
                        {
                            SinirNoktaZ += (i + 1) * YolUzunluk;
                            siraHangisinde = "Cimen";
                            Isaret.transform.position = Vector3.zero;
                        }
                    }
                    SeritYonu = !SeritYonu;
                }
                else
                {
                    for (int i = 0; i <= rndYol; i++)
                    {

                        GameObject yolInstace = Instantiate(lRoad, new Vector3(0, 0, SinirNoktaZ + i * YolUzunluk + YolUzunluk * .5f), Quaternion.identity);
                        GameList.Add(yolInstace);

                        if (i == rndYol)
                        {
                            SinirNoktaZ += (i + 1) * YolUzunluk;
                            siraHangisinde = "Cimen";
                            Isaret.transform.position = Vector3.zero;
                        }
                    }
                    SeritYonu = !SeritYonu;
                }
                GameObject Kal = Instantiate(Kaldirim, new Vector3(0, 0, SinirNoktaZ + KaldirimUzunluk * .5f), Quaternion.identity);
                GameList.Add(Kal);
                SinirNoktaZ += KaldirimUzunluk / 2;

            }
            if (siraHangisinde == "Cimen")
            {
                int rndCimen = Random.Range(1, 5);
                for (int i = 0; i <= rndCimen; i++)
                {
                    GameObject cimenInstace = Instantiate(Cimen, new Vector3(0, 0, SinirNoktaZ + CimenUzunluk * .5f + i * CimenUzunluk), Quaternion.identity);
                    GameList.Add(cimenInstace);

                    if (i == rndCimen)
                    {
                        SinirNoktaZ += (i + 1) * CimenUzunluk;
                        siraHangisinde = "Yol";
                        Isaret.transform.position = Vector3.zero;
                    }

                }
                //**Kaldirim**
                GameObject Kal = Instantiate(Kaldirim, new Vector3(0, 0, KaldirimUzunluk * .5f + SinirNoktaZ), Quaternion.identity);
                SinirNoktaZ += KaldirimUzunluk;
                GameList.Add(Kal);

            }
            Isaret.transform.position = new Vector3(0, 0, SinirNoktaZ);
        }
        #endregion
        if (BasliadiMi)
        {
            EnUzakNoktaZ = SinirNoktaZ;
            if (!Baslangic)
            {
                foreach (var b in Baslangiclar)
                {
                    b.GetComponent<Rigidbody>().velocity = GameSpeedVec;

                    GameList.Add(b);

                }
                Baslangic = true;
            }
            //Hız Değişmi
            GameSpeedVec.z -= 0.005f;
            #region Hiz Optimizsyonu
            distance.GetComponent<Rigidbody>().velocity = GameSpeedVec;
            Isaret.GetComponent<Rigidbody>().velocity = GameSpeedVec;
            foreach (GameObject obj in GameList)
            {
                if (obj != null)
                {
                    obj.GetComponent<Rigidbody>().velocity = GameSpeedVec;

                }
            }
            #endregion
            #region YolCimenlikRandomizasyonu
            //**Cimen**
            if (siraHangisinde == "Cimen" && Isaret.transform.position.z <= SinirNoktaZ)
            {
                int rndCimen = Random.Range(1, 5);
                for (int i = 0; i <= rndCimen; i++)
                {
                    GameObject cimenInstace = Instantiate(Cimen, new Vector3(0, 0, EnUzakNoktaZ + CimenUzunluk * .5f + i * CimenUzunluk), Quaternion.identity);

                    cimenInstace.GetComponent<Rigidbody>().velocity = GameSpeedVec;
                    GameList.Add(cimenInstace);
                    if (i == rndCimen)
                    {
                        EnUzakNoktaZ = SinirNoktaZ + (i + 1) * CimenUzunluk;
                        siraHangisinde = "Yol";

                    }

                }
                //**Kaldirim**
                GameObject Kal = Instantiate(Kaldirim, new Vector3(0, 0, EnUzakNoktaZ + KaldirimUzunluk * .5f), Quaternion.identity);
                EnUzakNoktaZ += KaldirimUzunluk;
                Kal.GetComponent<Rigidbody>().velocity = GameSpeedVec;
                GameList.Add(Kal);
                Isaret.transform.position = new Vector3(0, 0, EnUzakNoktaZ);
            }
            //**Yol**
            if (siraHangisinde == "Yol" && Isaret.transform.position.z <= SinirNoktaZ)
            {
                EnUzakNoktaZ = SinirNoktaZ;
                int rndYol = Random.Range(1, 5);
                if (SeritYonu)
                {
                    for (int i = 0; i <= rndYol; i++)
                    {

                        GameObject yolInstace = Instantiate(rRoad, new Vector3(0, 0, EnUzakNoktaZ + i * YolUzunluk + YolUzunluk * .5f), Quaternion.identity);
                        yolInstace.GetComponent<Rigidbody>().velocity = GameSpeedVec;
                        GameList.Add(yolInstace);
                        if (i == rndYol)
                        {
                            EnUzakNoktaZ = SinirNoktaZ + (i + 1) * YolUzunluk;
                            siraHangisinde = "Cimen";

                        }

                    }
                    SeritYonu = !SeritYonu;
                }
                else
                {
                    for (int i = 0; i <= rndYol; i++)
                    {

                        GameObject yolInstace = Instantiate(lRoad, new Vector3(0, 0, EnUzakNoktaZ + i * YolUzunluk + YolUzunluk * .5f), Quaternion.identity);
                        yolInstace.GetComponent<Rigidbody>().velocity = GameSpeedVec;
                        GameList.Add(yolInstace);
                        if (i == rndYol)
                        {
                            EnUzakNoktaZ = SinirNoktaZ + (i + 1) * YolUzunluk;
                            siraHangisinde = "Cimen";

                        }

                    }
                    SeritYonu = !SeritYonu;
                }
                //**Kaldirim**

                GameObject Kal = Instantiate(Kaldirim, new Vector3(0, 0, EnUzakNoktaZ + KaldirimUzunluk * .5f), Quaternion.identity);
                Kal.GetComponent<Rigidbody>().velocity = GameSpeedVec;
                GameList.Add(Kal);
                EnUzakNoktaZ += KaldirimUzunluk / 2;
                Isaret.transform.position = new Vector3(0, 0, EnUzakNoktaZ);
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
            Instantiate(Olusturulacak, new Vector3(Nerede.x, Nerede.y, Nerede.z + i * genislikZ), Quaternion.identity).GetComponent<Rigidbody>().velocity = GameSpeedVec;
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
