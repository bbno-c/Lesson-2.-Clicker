using System.Collections.Generic;
using UnityEngine;
using Controllers;
using System;

namespace Objects
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private GameController GameProxy;

        [SerializeField] private GameObject _cubePrefab;

        public event Action GameOver;

        [SerializeField] private Dictionary<GameObject, Cubes> _cubes;
        [SerializeField] private Queue<GameObject> _cubesToShow;
        [SerializeField] private int _poolCount;

        [SerializeField] private float _speed;
        private float _delta;
        private float _startSpeed;
        private int _previousScore;
        private bool _gameInStop;

        private void Start()
        {
            _cubes = new Dictionary<GameObject, Cubes>();
            _cubesToShow = new Queue<GameObject>();

            for (int i = 0; i < _poolCount; i++)
            {
                var cubeObject = Instantiate(_cubePrefab);
                var script = cubeObject?.GetComponent<Cubes>();
                script.CubeClickedEvent += OnCubeClicked;
                script.CubeGrownEvent += OnCubeGrown;
                cubeObject.SetActive(false);
                _cubes.Add(cubeObject, script);
                _cubesToShow.Enqueue(cubeObject);
            }
            _startSpeed = _speed;
            _gameInStop = false;
        }

        private void OnEnable()
        {
            _gameInStop = false;
        }

        private void Update()
        {
            _delta += Time.deltaTime;
        }

        private void FixedUpdate()
        {
            if (_delta >= _speed)
            {
                CubeSpawn();
            }
        }

        private void CubeSpawn()
        {
            if(_gameInStop)
            {
                return;
            }

            if (!(_cubesToShow.Count > 0))
            {
                StopSpawner();
                return;
            }

            Vector3 screenPoint = Camera.main.ScreenToWorldPoint(
                new Vector2(UnityEngine.Random.Range(3f, Camera.main.pixelWidth - 3),
                UnityEngine.Random.Range(3f, Camera.main.pixelHeight - 3))
                );

            var cube = _cubesToShow.Dequeue();
            cube.transform.position = screenPoint + new Vector3(0, 0, 8);
            cube.SetActive(true);

            _delta = 0;

            if (_previousScore != GameProxy.GetScore() / 10)
            {
                _previousScore = GameProxy.GetScore() / 10;
                _speed -= 0.05f;
            }
        }

        private void OnCubeGrown(GameObject cube)
        {
            _cubesToShow.Enqueue(cube);
            
            cube.SetActive(false);
        }

        private void OnCubeClicked()
        {
            GameProxy.AddScore(1);
        }

        void StopSpawner()
        {
            _gameInStop = true;
            foreach (KeyValuePair<GameObject, Cubes> obj in _cubes)
            {
                OnCubeGrown(obj.Key);
            }
            _speed = _startSpeed;
            GameOver.Invoke();
        }
    }
}