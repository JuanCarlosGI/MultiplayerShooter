using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Enemy : NetworkBehaviour {
    public static bool bHitSound;
    public AudioClip CollitionEnemy;
    public AudioClip BulletHitSoundHitEnemigo;
    public float Speed = 0.5f;

    private Rigidbody _rb;
    private AudioSource _asCollitionEnemy;
    private AudioSource _asSoundHitEnemigo;

    // Use this for initialization
    void Start () {
        bHitSound = false;
        _rb = GetComponent<Rigidbody>();

        _asCollitionEnemy = gameObject.AddComponent<AudioSource>();
        _asCollitionEnemy.clip = CollitionEnemy;

        _asSoundHitEnemigo = gameObject.AddComponent<AudioSource>();
        _asSoundHitEnemigo.clip = BulletHitSoundHitEnemigo;
    }
	
	// Update is called once per frame
	void Update () {
        if (!isServer) return;
        if (PauseGame.Instance.IsPaused) return;

        var players = GameObject.FindGameObjectsWithTag("player");
        var closest = players[0];
        var closestDistance = closest.transform.position - transform.position;
        foreach (var player in players)
        {
            var distance = player.transform.position - transform.position;
            if (GetMagnitude(distance) < GetMagnitude(closestDistance))
            {
                closest = player;
                closestDistance = distance;
            }
        }
        var closesetTransform = closest.transform.position;
        closesetTransform.y = transform.position.y;
        transform.LookAt(2 * transform.position - closest.transform.position);
        
        _rb.AddForce(-transform.forward * Speed, ForceMode.Force);

        if (bHitSound)
        {
            bHitSound = false;
            _asSoundHitEnemigo.Play();
        }
    }

    private float GetMagnitude(Vector3 v)
    {
        return Mathf.Sqrt(Mathf.Pow(v.x, 2) + Mathf.Pow(v.y, 2) + Mathf.Pow(v.z, 2));
    }

    
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            _asCollitionEnemy.Play();
            GameMaster._amountDestroyed--;
        }
    }
    
}
