using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public GameProxy GameProxy;

    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private Dictionary<GameObject,Cubes> _cubes;
    [SerializeField] private Queue<GameObject> _cubesToShow;
    [SerializeField] private int _poolCount;

    [SerializeField] private GameObject _display;
    [SerializeField] private Text _displayText;

    [SerializeField] private float _speed;
    private float _delta;
    private float _startSpeed;
    private int _temp;
    private bool _flag;

    private void Start()
    {
        _cubes = new Dictionary<GameObject, Cubes>();
        _cubesToShow = new Queue<GameObject>();

        for (int i = 0; i < _poolCount; i++)
        {
            var prefab = Instantiate(_cubePrefab);
            var script = prefab?.GetComponent<Cubes>();
            prefab.SetActive(false);
            _cubes.Add(prefab, script);
            _cubesToShow.Enqueue(prefab);
        }

        Cubes.CubeGrown += OnCubeGrown;
        _startSpeed = _speed;
    }

    private void OnEnable()
    {
        _display?.SetActive(true);
    }

    private void Update()
    {
        _delta += Time.deltaTime;
        if (_delta > _speed)
        {
            if (_cubesToShow.Count > 0)
            {
                Vector3 screenPoint = Camera.main.ScreenToWorldPoint(new Vector2(UnityEngine.Random.Range(3f, Camera.main.pixelWidth - 3),
                    UnityEngine.Random.Range(3f, Camera.main.pixelHeight - 3)));

                var cube = _cubesToShow.Dequeue();
                cube.transform.position = screenPoint + new Vector3(0, 0, 8);
                cube.SetActive(true);

                _delta = 0;

                if (_temp != GameProxy.Score / 10)
                {
                    _temp = GameProxy.Score / 10;
                    _flag = true;
                }
                if (_flag)
                {
                    _speed -= 0.05f;
                    _flag = false;
                }
            }
            else
            {
                foreach (KeyValuePair< GameObject,Cubes > obj in _cubes)
                {
                    OnCubeGrown(obj.Key);
                }
                _speed = _startSpeed;
                _displayText.text = "Score: 0";
                _display?.SetActive(false);
                GameProxy.EndGame();
                gameObject.SetActive(false);
            }
        }

        _displayText.text = "Score: " + GameProxy.Score;
    }

    private void OnCubeGrown(GameObject cube)
    {
        var script = _cubes[cube];
        cube.transform.localScale = script.StartScale;
        cube.SetActive(false);
        _cubesToShow.Enqueue(cube);
    }
}
