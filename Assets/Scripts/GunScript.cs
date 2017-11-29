using UnityEngine;

namespace Assets.Scripts
{
    public abstract class GunScript
    {
        protected GameObject GunModel;

        protected GunScript(GameObject gunModel)
        {
            GunModel = gunModel;
        }

        public abstract GameObject GetBullet();
        public abstract void Transform();
        public abstract GunScript GetNext();
    }
}
