using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class Bullet : MonoBehaviour
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
                Destroy(collision.gameObject);
                GameMaster.Instance.OnTargetDestroyed();
            }

            _asHit.Play();
            Destroy(gameObject);
        }
    }
}
