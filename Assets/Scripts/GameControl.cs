using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameControl : MonoBehaviour
{
    public GameObject Karakter;
    public GameObject rRoad;
    public GameObject lRoad;
    public GameObject Cimen;
    public GameObject Kaldirim1;
    public GameObject Kaldirim2;
    public GameObject Isaret;
    public GameObject distance;
    public GameObject Camera;
    public Vector3 GameSpeedVec;
    public AudioClip[] GameMusics;
    float SinirNoktaZ = 0;
    float EnUzakNoktaZ = 0;
    bool siraHangisinde = true;
    bool BasladiMi = false;
    bool SeritYonu = true;
    private string connectionString;
    List<GameObject> GameList = new List<GameObject>();
    float YolUzunluk = new float();
    float CimenUzunluk = new float();
    float KaldirimUzunluk = new float();
    GameObject Son;
    void Start()
    {
        PlayMusic();
        YolUzunluk = rRoad.GetComponent<BoxCollider>().size.z * rRoad.transform.localScale.z;
        CimenUzunluk = Cimen.GetComponent<BoxCollider>().size.z * Cimen.transform.localScale.z;
        KaldirimUzunluk = Kaldirim1.GetComponent<BoxCollider>().size.z * Kaldirim1.transform.localScale.z;
        for (int i = 0; i < 18; i++)
        {
            GameObject a = Instantiate(Cimen, new Vector3(0, 0, i * CimenUzunluk), Quaternion.identity);
            GameList.Add(a);
            SinirNoktaZ = a.transform.position.z + CimenUzunluk / 2;
        }
        GameObject K = Instantiate(Kaldirim1, new Vector3(0, 0, KaldirimUzunluk * .5f + SinirNoktaZ), Quaternion.identity);
        GameList.Add(K);
        SinirNoktaZ += KaldirimUzunluk;
        #region Sahne
        while (SinirNoktaZ <= 200)
        {
            if (siraHangisinde)
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
                            siraHangisinde = false;
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
                            siraHangisinde = false;
                            Isaret.transform.position = Vector3.zero;
                        }
                    }
                    SeritYonu = !SeritYonu;
                }
                GameObject Kal = Instantiate(Kaldirim1, new Vector3(0, 0, SinirNoktaZ + KaldirimUzunluk * .5f), Quaternion.identity);
                GameList.Add(Kal);
                SinirNoktaZ += KaldirimUzunluk;

            }
            if (!siraHangisinde)
            {
                int rndCimen = Random.Range(1, 5);
                for (int i = 0; i <= rndCimen; i++)
                {
                    GameObject cimenInstace = Instantiate(Cimen, new Vector3(0, 0, SinirNoktaZ + CimenUzunluk * .5f + i * CimenUzunluk), Quaternion.identity);
                    GameList.Add(cimenInstace);

                    if (i == rndCimen)
                    {
                        SinirNoktaZ += (i + 1) * CimenUzunluk;
                        siraHangisinde = true;
                        Isaret.transform.position = Vector3.zero;
                    }

                }
                //**Kaldirim**
                GameObject Kal = Instantiate(Kaldirim1, new Vector3(0, 0, KaldirimUzunluk * .5f + SinirNoktaZ), Quaternion.identity);
                SinirNoktaZ += KaldirimUzunluk;
                GameList.Add(Kal);

            }
            Isaret.transform.position = new Vector3(0, 0, SinirNoktaZ);
        }
        #endregion

    }
    void Update()
    {
        
        if (Karakter.transform.position.y <= 3f&&BasladiMi==false)
        {
            BasladiMi = true;
        }
        if (gameObject.GetComponent<AudioSource>().volume < 1)
        {
            gameObject.GetComponent<AudioSource>().volume += 0.01f;
        }
        //CameraWatch(Camera, Karakter,new Vector3(0,10.7f,-17));
        if (BasladiMi)
        {
            EnUzakNoktaZ = SinirNoktaZ;           
            //Hız Değişmi
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
            if (!siraHangisinde && Isaret.transform.position.z <= SinirNoktaZ)
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
                        siraHangisinde = true;
                    }
                }
                //**Kaldirim**
                Son = Instantiate(Kaldirim1, new Vector3(0, 0, EnUzakNoktaZ + KaldirimUzunluk * .5f), Quaternion.identity);
                EnUzakNoktaZ = Son.transform.position.z + KaldirimUzunluk / 2;
                Son.GetComponent<Rigidbody>().velocity = GameSpeedVec;
                GameList.Add(Son);
                Isaret.transform.position = new Vector3(0, 0, EnUzakNoktaZ);
            }
            //**Yol**
            if (siraHangisinde && Isaret.transform.position.z <= SinirNoktaZ)
            {
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
                            siraHangisinde = false;
                        }
                    }
                    SeritYonu = !SeritYonu;
                }
                else
                {
                    for (int i = 0; i <= rndYol; i++)
                    {
                        GameObject yolInstace = Instantiate(lRoad, new Vector3(0, 0, EnUzakNoktaZ + i * YolUzunluk + YolUzunluk /2), Quaternion.identity);
                        yolInstace.GetComponent<Rigidbody>().velocity = GameSpeedVec;
                        GameList.Add(yolInstace);
                        if (i == rndYol)
                        {
                            EnUzakNoktaZ = SinirNoktaZ + (i + 1) * YolUzunluk;
                            siraHangisinde = false;
                        }
                    }
                    SeritYonu = !SeritYonu;
                }
                //**Kaldirim**
                GameObject Kaldir = Instantiate(Kaldirim2, new Vector3(0, 0, EnUzakNoktaZ + KaldirimUzunluk /2), Quaternion.identity);
                Kaldir.GetComponent<Rigidbody>().velocity = GameSpeedVec;
                GameList.Add(Kaldir);
                EnUzakNoktaZ = Kaldir.transform.position.z+ KaldirimUzunluk/2;
                Isaret.transform.position = new Vector3(0, 0, EnUzakNoktaZ);
            }
        }
        #endregion
        GameSpeedVec.z -= 0.005f;
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
    private void CameraWatch(GameObject camera,GameObject target,Vector3 differanceVec)
    {
        camera.transform.position = target.transform.position+differanceVec;
    }
    public void YenidenBaslat()
    {
        SceneManager.LoadScene("StandardMode", LoadSceneMode.Single);
    }
    private void PlayMusic()
    {
        AudioSource source = this.GetComponent<AudioSource>();
        int sayi = Random.Range(0, 4);
        source.clip = GameMusics[sayi];
        source.Play();
    }
    public void GoBackToMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

}
