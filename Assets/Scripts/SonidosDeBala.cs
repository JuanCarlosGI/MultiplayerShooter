using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidosDeBala : MonoBehaviour {
    public static int iSound;

    public AudioClip BulletHitSoundTree;
    public AudioClip BulletHitSoundPuntosCubo;
    public AudioClip BulletHitSoundHitEnemigo;

    private AudioSource _asHitSoundTree;
    private AudioSource _asHitPointsCube;
    private AudioSource _asHitHitEnemigo;

    // Use this for initialization
    void Start () {
        iSound = 0;

        _asHitSoundTree = gameObject.AddComponent<AudioSource>();
        _asHitSoundTree.clip = BulletHitSoundTree;

        _asHitPointsCube = gameObject.AddComponent<AudioSource>();
        _asHitPointsCube.clip = BulletHitSoundPuntosCubo;

        _asHitHitEnemigo = gameObject.AddComponent<AudioSource>();
        _asHitHitEnemigo.clip = BulletHitSoundHitEnemigo;
    }
	
	// Update is called once per frame
	void Update () {
        if (iSound != 0)
        {
            if (iSound == 1)
            {
                _asHitPointsCube.Play();
            }
            else if (iSound == 2)
            {
                _asHitSoundTree.Play();
            }
            iSound = 0;
        }
	}
}
