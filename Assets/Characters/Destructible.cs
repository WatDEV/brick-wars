using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour {

    private bool isQuitting = false;
    public GameObject ObjectToSpawn;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnDestroy()
    {
        if (isQuitting)
            return;

        var trans = gameObject.transform.GetChild(0).transform;

        for (var i = 0; i < trans.localScale.x; i += 4)
        {
            for (var j = 0; j < trans.localScale.y; j += 4)
            {
                for (var k = 0; k < trans.localScale.z; k += 4)
                {
                    var des = Instantiate(ObjectToSpawn);
                    des.transform.localScale = new Vector3(4, 4, 4);
                    des.transform.position = gameObject.transform.position + new Vector3(i, j, k);
                    des.transform.rotation = gameObject.transform.rotation;
                    des.GetComponent<Rigidbody>().AddForce(new Vector3(0,
                        0,
                        0) * 50000);
                    des.GetComponentInChildren<Renderer>().material = gameObject.GetComponentInChildren<CharacterHighlight>().Material;
                    Destroy(des, 2);
                }
            }
        }
    }
    
    void OnApplicationQuit()
    {
        isQuitting = true;
    }
}
