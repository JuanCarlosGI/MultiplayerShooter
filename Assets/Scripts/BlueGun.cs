using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class BlueGun : GunScript
    {
        private readonly GunScript _next;
        private readonly Material _material;

        public override GameObject GetBullet()
        {
            return Resources.Load<GameObject>("BlueBullet");
        }

        public override void Transform()
        {
            var child = GunModel.transform.Find("Hull");
            var mats = child.GetComponent<Renderer>().materials;
            mats[0] = _material;
            child.GetComponent<Renderer>().materials = mats;
        }

        public override GunScript GetNext()
        {
            return _next;
        }

        public BlueGun(GameObject gunModel, GunScript next) : base(gunModel)
        {
            _next = next ?? new RedGun(GunModel, this);
            _material = Resources.Load<Material>("BlueGunMat");
        }
    }
}
