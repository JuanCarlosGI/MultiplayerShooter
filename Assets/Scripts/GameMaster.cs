using System;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameMaster : NetworkBehaviour
{
    public static GameMaster Instance;
    private static readonly System.Random Random = new System.Random(DateTime.Now.Millisecond);
    public ulong AverageSpan;
    public ulong MaxDeviation;
    public int TargetsToLose;
    public Text TargetsText;
    public Text TimeText;
    public Text GameOverText;

    public GameObject[] Targets;
    public long MinX;
    public long MaxX;
    public long MinY;
    public long MaxY;
    public long MinZ;
    public long MaxZ;

    private long _xSpan;
    private long _ySpan;
    private long _zSpan;
    private double _nextTarget;

    private int _amountTargets;
    private TimeSpan _timeSpan;
    private int _amountDestroyed;

    private ScoreAnalizer _scoreAnalizer = new ScoreAnalizer();
    // Use this for initialization
    void Start ()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _xSpan = MaxX - MinX;
        _ySpan = MaxY - MinY;
        _zSpan = MaxZ - MinZ;

        _nextTarget = CalculateSpan() / 1000.0;
        _amountTargets = 0;
        _timeSpan = new TimeSpan();
        Instance = this;
        _amountDestroyed = 0;
    }
	
	// Update is called once per frame
	void Update ()
	{
        if (!isServer) return;

	    _timeSpan = _timeSpan.Add(TimeSpan.FromSeconds(Time.deltaTime));
        var textScript = TimeText.GetComponent<UpdateText>();
        textScript.SetText(_timeSpan.Minutes + ":" + (_timeSpan.Seconds < 10 ? "0" : "") + _timeSpan.Seconds + ":" + (_timeSpan.Milliseconds < 100 ? "0" : "") + _timeSpan.Milliseconds / 10);

	    _nextTarget -= Time.deltaTime;
	    if (_nextTarget <= 0)
	    {
	        var randX = (float)Random.NextDouble() * _xSpan + MinX;
	        var randY = (float)Random.NextDouble() * _ySpan + MinY;
	        var randZ = (float)Random.NextDouble() * _zSpan + MinZ;

	        var target = Targets[Random.Next(Targets.Length)];
	        var aux = Instantiate(target, new Vector3(randX, randY, randZ),
	            new Quaternion((float)Random.NextDouble() * 360, (float)Random.NextDouble() * 360,
	                (float)Random.NextDouble() * 360, 0));
            NetworkServer.Spawn(aux);

            _nextTarget = CalculateSpan() / 1000.0;
            _amountTargets++;

            var targetsScript = TargetsText.GetComponent<UpdateText>();
            targetsScript.SetText("Targets: " + _amountTargets);    
	        if (_amountTargets >= TargetsToLose)
	        {
	            EndGame();
	        }
        }
    }

    private double CalculateSpan()
    {
        var offset = Random.NextDouble() * 2 * MaxDeviation - MaxDeviation;
        return AverageSpan + offset;
    }

    public void OnTargetDestroyed()
    {
        _amountTargets--;
        _amountDestroyed++;
        TargetsText.text = "Targets: " + _amountTargets;
    }

    private void EndGame()
    {
        GameOver.LatestScore = _timeSpan;
        GameOver.LatestTargetsDestroyed = _amountDestroyed;

        if (_scoreAnalizer.GetHighScore() < _timeSpan) _scoreAnalizer.SaveScore(_timeSpan);
        SceneManager.LoadScene("gameover");
    }
}
