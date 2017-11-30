using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerColliderListener : NetworkBehaviour {
    private void OnCollisionEnter(Collision collision)
    {
        if (!isServer) return;

        if (collision.gameObject.tag == "enemy")
        {
            GameMaster.Instance.AddEnemyHit();
            var teleporter = collision.gameObject.GetComponent<SpawnTeleporter>();
            if (teleporter == null)
            {
                Destroy(collision.gameObject);
            } else
            {
                teleporter.Teleport();
            }
        } else if (collision.gameObject.tag == "cube")
        {
            GameMaster.Instance.AddCubeHit();
            var cube = collision.gameObject.GetComponent<Cube>();
            if (cube != null) cube.Delete();
        }
    }
}
