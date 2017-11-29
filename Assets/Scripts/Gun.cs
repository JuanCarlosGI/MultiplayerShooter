using System;
using Assets.Scripts;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float ShootCooldown;
    public float ChangeCooldown;
    public uint BulletSpeed;
    public AudioClip ChangeWeaponSound;
    public AudioClip ShotSound;

    private GunScript _script;
    private float _nextShot;
    private float _nextChange;
    private Transform _spawn;
    private AudioSource _asShot;
    private AudioSource _asChange;

	// Use this for initialization
	void Start ()
	{
	    _script = new BlueGun(gameObject, null);
	    _script.Transform();

	    _asChange = gameObject.AddComponent<AudioSource>();
	    _asChange.clip = ChangeWeaponSound;
	    _asShot = gameObject.AddComponent<AudioSource>();
	    _asShot.clip = ShotSound;

	    _spawn = gameObject.transform.Find("BulletSpawn");
	    _nextChange = ChangeCooldown;
	    _nextShot = ShootCooldown;
	}
	
	// Update is called once per frame
	void Update ()
	{
        if (PauseGame.Instance.IsPaused) return;

	    _nextChange -= Time.deltaTime;
	    _nextShot -= Time.deltaTime;
	    if (Input.GetMouseButtonDown(0) && _nextShot <= 0)
	    {
            var bullet = Instantiate(_script.GetBullet(), _spawn.transform.position, _spawn.transform.rotation);
	        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * BulletSpeed);
	        _asShot.Play();

            _nextShot = ShootCooldown;
            
	    }
	    if (Input.GetMouseButtonDown(1) && _nextChange <= 0)
	    {
            _script = _script.GetNext();
	        _script.Transform();
	        _asChange.Play();

	        _nextChange = ChangeCooldown;
        }
	}
}
