using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class TargetRotation : MonoBehaviour
    {
        public float RotationAngle;
        private void Update()
        {
            gameObject.transform.Rotate(RotationAngle, 0, 0, Space.Self);
        }
    }
}
