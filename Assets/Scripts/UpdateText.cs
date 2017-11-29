using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class UpdateText : NetworkBehaviour {
    public Text Text;
    [SyncVar(hook="ChangeText")]
    private string _string;

    public void SetText(string text)
    {
        if (!isServer) return;
        _string = text;
    }

    private void ChangeText(string text)
    {
        Text.text = text;
    }
}
