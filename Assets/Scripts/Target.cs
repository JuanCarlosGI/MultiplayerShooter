using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Target : NetworkBehaviour
{
    [SyncVar(hook = "OnDestroyed")]
    private bool _destroyed;

    public void Destroy()
    {
        if (!isServer) return;

        _destroyed = true;
    }

    public void OnDestroyed(bool destroyed)
    {
        Destroy(gameObject);
    }
}
