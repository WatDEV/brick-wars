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

        for (var i = 0; i < trans.localScale.x; i +=2)
        {
            for (var j = 0; j < trans.localScale.y; j +=2)
            {
                for (var k = 0; k < trans.localScale.z; k +=2)
                {
                    var des = Instantiate(ObjectToSpawn);
                    des.transform.localScale = new Vector3(2, 2, 2);
                    des.transform.position = gameObject.transform.position + new Vector3(i, j, k) - new Vector3(trans.localScale.x/2, trans.localScale.y / 2, trans.localScale.z / 2);
                    des.transform.rotation = gameObject.transform.rotation;
                    des.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-1f,1f),
						Random.Range(-1f, 1f),
						Random.Range(-1f, 1f)) * Random.Range(trans.localScale.x * trans.localScale.y * trans.localScale.z / 2, trans.localScale.x * trans.localScale.y * trans.localScale.z));
                    des.GetComponentInChildren<Renderer>().material = gameObject.GetComponentInChildren<CharacterHighlight>().Material;
                    Destroy(des, 1.5f);
                }
            }
        }
    }
    
    void OnApplicationQuit()
    {
        isQuitting = true;
    }
}
