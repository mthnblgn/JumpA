using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteAlicanNERO : MonoBehaviour
{
    float sayac = 0;
    public Animator anim;
    void Start()
    {

    }
    void Update()
    {

    }
    public void bb()
    {
        while (true)
        {
            sayac += Time.deltaTime;
            for (int i = 1; i <= 5; i++)
            {
                if (sayac>=1)
                {
                    anim.SetFloat("Blend", i * 0.1f);
                    sayac = 0;
                }
                
            }
        }

    }
}
