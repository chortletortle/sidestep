using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infinite : MonoBehaviour
{
    public GameObject player;
    GameObject floor;

    // Start is called before the first frame update
    void Start()
    {
        for (int p = 1; p < 12; p++)
        {
            float r = Random.Range(0f, 1f);
            //Debug.Log(r.ToString());
            if (r < 1f/3f)
            {
                Instantiate(Resources.Load("Lane1PF"),
          new Vector3(0f, 1f, transform.position.z + p * 20), Quaternion.identity);
            } else if (r > 1f/3f && r < 2f/3f)
            {
                Instantiate(Resources.Load("Lane1PF"),
          new Vector3(2.5f, 1f, transform.position.z + p * 20), Quaternion.identity);
            }
            else
            {
                Instantiate(Resources.Load("Lane1PF"),
          new Vector3(-2.5f, 1f, transform.position.z + p * 20), Quaternion.identity);
            }


        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
          floor = (GameObject)Instantiate(Resources.Load("Floor"), 
          new Vector3(0f, 0f, transform.position.z+350f), Quaternion.identity);
        }
    }

}
