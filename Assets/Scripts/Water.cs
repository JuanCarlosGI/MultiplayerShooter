using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Water : MonoBehaviour
{
    public AudioClip WaterEnterSound;
    public AudioClip WaterOutSound;
    public Text Instructions;
    public GameObject Spawn;

    private AudioSource _asWaterEnter;
    private AudioSource _asWaterOut; 

    // Use this for initialization
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {
            Instructions.text = "Press E to return to the surface";
            _asWaterEnter = gameObject.AddComponent<AudioSource>();
            _asWaterEnter.clip = WaterEnterSound;
            _asWaterEnter.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "player")
        {
            Instructions.text = "";
            _asWaterOut = gameObject.AddComponent<AudioSource>();
            _asWaterOut.clip = WaterOutSound;
            _asWaterOut.Play();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                other.transform.position = Spawn.transform.position;
            }
        }
    }
}
