using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts
{
    class Bullet : NetworkBehaviour
    {
        public AudioClip BulletHitSound;
        public AudioClip BulletHitSoundPuntosCubo;
        public float MaxLifespanSeconds;
        public GameObject Target;

        public uint BulletSpeed;

        private AudioSource _asHit;
        private AudioSource _asHitPointsCube;
        private float _lifeSpan;

        private void Start()
        {
            _asHit = gameObject.AddComponent<AudioSource>();
            _asHit.clip = BulletHitSound;

            _asHitPointsCube = gameObject.AddComponent<AudioSource>();
            _asHitPointsCube.clip = BulletHitSoundPuntosCubo;

            _lifeSpan = 0;

            GetComponent<Rigidbody>().AddForce(transform.forward * BulletSpeed);
        }

        private void Update()
        {
            _lifeSpan += Time.deltaTime;
            if (_lifeSpan >= MaxLifespanSeconds)
                Destroy(gameObject);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == Target.tag)
            {
                SonidosDeBala.iSound = 1;
                var target = collision.gameObject.GetComponent<Target>();
                if (target != null)
                    target.Destroy();

                GameMaster.Instance.OnTargetDestroyed();
            }

            else if (collision.gameObject.tag == "cube")
            {
                SonidosDeBala.iSound = 1;
                var cube = collision.gameObject.GetComponent<Cube>();
                _asHitPointsCube.Play();

                if (cube != null)
                {
                    cube.Delete();
                }
            }

            else if (collision.gameObject.tag == "enemy")
            {
                Enemy.bHitSound = true;
            }
            
            else
            {
                SonidosDeBala.iSound = 2;
            }
            Destroy(gameObject);
        }
    }
}
