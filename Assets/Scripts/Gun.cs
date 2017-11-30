using System;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.Networking;

public class Gun : NetworkBehaviour
{
    public float ShootCooldown;
    public float ChangeCooldown;
    public uint BulletSpeed;
    public AudioClip ChangeWeaponSound;
    public AudioClip ShotSound;
    public Transform Spawn;
    public GameObject Hull;

    private GunScript _script;
    private float _nextShot;
    private float _nextChange;
    private AudioSource _asShot;
    private AudioSource _asChange;

    [SyncVar(hook = "ChangeGun")]
    public int Change;

	// Use this for initialization
	void Start ()
	{
	    _script = new BlueGun(gameObject, null);
	    _script.Transform(Hull);

	    _asChange = gameObject.AddComponent<AudioSource>();
	    _asChange.clip = ChangeWeaponSound;
	    _asShot = gameObject.AddComponent<AudioSource>();
	    _asShot.clip = ShotSound;
        
	    _nextChange = ChangeCooldown;
	    _nextShot = ShootCooldown;
	}
	
	// Update is called once per frame
	void Update ()
	{
        if (!isLocalPlayer) return;
        if (PauseGame.Instance.IsPaused) return;

	    _nextChange -= Time.deltaTime;
	    _nextShot -= Time.deltaTime;
	    if (Input.GetMouseButtonDown(0) && _nextShot <= 0)
	    {
            CmdFire();
        }
	    if (Input.GetMouseButtonDown(1) && _nextChange <= 0)
	    {
            CmdChangeGun();
        }
	}

    [Command]

    void CmdChangeGun()
    {
        Change++;
    }

    [Command]
    void CmdFire()
    {
        var bullet = Instantiate(_script.GetBullet(), Spawn.transform.position, Spawn.transform.rotation);
        bullet.GetComponent<Bullet>().BulletSpeed = BulletSpeed;
        _asShot.Play();

        _nextShot = ShootCooldown;

        // Spawn the bullet on the Clients
        NetworkServer.Spawn(bullet);
    }

    void ChangeGun(int _)
    {
        _script = _script.GetNext();
        _script.Transform(Hull);
        _asChange.Play();

        _nextChange = ChangeCooldown;
    }
}
