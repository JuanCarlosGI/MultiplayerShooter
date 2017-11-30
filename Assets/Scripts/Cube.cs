using Assets.Scripts;
using UnityEngine;
using UnityEngine.Networking;

public class Cube : NetworkBehaviour {
    public float Speed;
    [SyncVar(hook = "DeleteCube")]
    public bool _destroy;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!isServer) return;
        if (PauseGame.Instance.IsPaused) return;

        var players = GameObject.FindGameObjectsWithTag("player");
        var closest = players[0];
        var closestDistance = closest.transform.position - transform.position;
        foreach (var player in players)
        {
            var distance = player.transform.position - transform.position;
            if (GetMagnitude(distance) < GetMagnitude(closestDistance))
            {
                closest = player;
                closestDistance = distance;
            }
        }
        closestDistance.Normalize();
        transform.Translate(closestDistance * Speed, Space.World);
    }

    private float GetMagnitude(Vector3 v)
    {
        return Mathf.Sqrt(Mathf.Pow(v.x, 2) + Mathf.Pow(v.y, 2) + Mathf.Pow(v.z, 2));
    }
    
    public void Delete()
    {
        if (!isServer) return;
        _destroy = true;
    }

    private void DeleteCube(bool _)
    {
        Destroy(gameObject);
    }
}
