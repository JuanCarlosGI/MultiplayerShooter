using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class RedGun : GunScript
    {
        private readonly GunScript _next;
        private readonly Material _material;

        public override GameObject GetBullet()
        {
            return Resources.Load<GameObject>("RedBullet");
        }

        public override void Transform(GameObject child)
        {
            var mats = child.GetComponent<Renderer>().materials;
            mats[0] = _material;
            child.GetComponent<Renderer>().materials = mats;
        }

        public override GunScript GetNext()
        {
            return _next;
        }

        public RedGun(GameObject gunModel, GunScript next) : base(gunModel)
        {
            _next = next ?? new BlueGun(GunModel, this);
            _material = Resources.Load<Material>("RedGunMat");
        }
    }
}
