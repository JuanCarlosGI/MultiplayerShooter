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
    public Text ScoreText;
    public int ScoreToLose;

    public GameObject[] Targets;
    public GameObject Cube;
    public long MinX;
    public long MaxX;
    public long MinY;
    public long MaxY;
    public long MinZ;
    public long MaxZ;

    public float CubeSpawnProbability;
    public int TargetAdd;
    public int CubeSubtract;
    public int EnemySubtract;

    private long _xSpan;
    private long _ySpan;
    private long _zSpan;
    private double _nextTarget;

    private int _amountTargets;
    private TimeSpan _timeSpan;
    public static int _amountDestroyed;
    public static int _amountHits;
    public static int _amountEnemyHits;

    [SyncVar(hook = "EndGame")]
    private string _sGameOver;

    [SyncVar(hook = "UpdateScore")]
    private int _score;

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
        _amountHits = 0;
        _amountEnemyHits = 0;
    }
	
	// Update is called once per frame
	void Update ()
	{
        if (!isServer) return;
        if (PauseGame.Instance.IsPaused) return;

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

            var r = Random.NextDouble();

            if (r < CubeSpawnProbability)
            {
                var cube = Instantiate(Cube, new Vector3(randX, randY, randZ),
                    new Quaternion((float)Random.NextDouble() * 360, (float)Random.NextDouble() * 360,
                        (float)Random.NextDouble() * 360, 0));
                NetworkServer.Spawn(cube);
            }

            _nextTarget = CalculateSpan() / 1000.0;
            _amountTargets++;

            var targetsScript = TargetsText.GetComponent<UpdateText>();
            targetsScript.SetText("Targets: " + _amountTargets);
            _score = _amountDestroyed * TargetAdd - _amountHits * CubeSubtract - _amountEnemyHits * EnemySubtract;
            if (_amountTargets >= TargetsToLose || _score < ScoreToLose)
	        {
                _sGameOver = _timeSpan.TotalMilliseconds + ">" + _score;
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

    public void AddCubeHit()
    {
        _amountHits++;
    }

    public void AddEnemyHit()
    {
        _amountEnemyHits++;
    }

    private void EndGame(string _timeSpanAndAmount)
    {
        String[] s = _timeSpanAndAmount.Split('>');
        _timeSpan = new TimeSpan(0, 0, 0, 0, int.Parse(s[0]));
        GameOver.LatestScore = _timeSpan;
        var score = int.Parse(s[1]);
        GameOver.LatestTargetsDestroyed = score;

        if (_scoreAnalizer.GetHighScore() < score) _scoreAnalizer.SaveScore(score);
        SceneManager.LoadScene("gameover");
    }

    private void UpdateScore(int score)
    {
        ScoreText.text = "Score: " + score;
    }
}
