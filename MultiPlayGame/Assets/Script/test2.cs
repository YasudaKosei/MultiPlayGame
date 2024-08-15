using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test2 : MonoBehaviour
{
    // Start is called before the first frame update
    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            GetComponent<Rigidbody>().AddForce(transform.forward * 20, ForceMode.Force);
        }
    }
}
