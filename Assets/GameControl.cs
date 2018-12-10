using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
