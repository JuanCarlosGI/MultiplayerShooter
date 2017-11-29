using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Water : MonoBehaviour
{
    public Text Instructions;
    public GameObject Spawn;
    

    // Use this for initialization
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Instructions.text = "Press E to return to the surface";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Instructions.text = "";
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                other.transform.position = Spawn.transform.position;
            }
        }
    }
}
