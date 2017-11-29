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
        public float MaxLifespanSeconds;
        public GameObject Target;

        private AudioSource _asHit;
        private float _lifeSpan;

        private void Start()
        {
            _asHit = gameObject.AddComponent<AudioSource>();
            _asHit.clip = BulletHitSound;
            _lifeSpan = 0;
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
                var target = collision.gameObject.GetComponent<Target>();
                if (target != null)
                    target.Destroy();

                GameMaster.Instance.OnTargetDestroyed();
            }

            _asHit.Play();
            Destroy(gameObject);
        }
    }
}
