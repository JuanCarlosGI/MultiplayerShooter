using UnityEngine;
using System.Collections;

public class SpawnTeleporter : MonoBehaviour
{
    public Transform SpawnPoint;

    public void Teleport()
    {
        gameObject.transform.position = SpawnPoint.position;
    }
}
