using System.Collections.Generic;
using UnityEngine;
using Controllers;

namespace Objects
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private GameController GameProxy;

        [SerializeField] private GameObject _cubePrefab;
        [SerializeField] private Dictionary<GameObject, Cubes> _cubes;
        [SerializeField] private Queue<GameObject> _cubesToShow;
        [SerializeField] private int _poolCount;

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
                script.CubeClickedEvent += ;
                script.CubeGrownEvent += OnCubeGrown;
                prefab.SetActive(false);
                _cubes.Add(prefab, script);
                _cubesToShow.Enqueue(prefab);
            }
            _startSpeed = _speed;
        }

        private void Update()
        {
            _delta += Time.deltaTime;
        }

        private void FixedUpdate()
        {
            if (_delta > _speed)
            {
                CubeSpawn();
            }
        }

        private void CubeSpawn()
        {
            if (!(_cubesToShow.Count > 0))
            {
                EndGame();
            }

            Vector3 screenPoint = Camera.main.ScreenToWorldPoint(new Vector2(UnityEngine.Random.Range(3f, Camera.main.pixelWidth - 3),
                UnityEngine.Random.Range(3f, Camera.main.pixelHeight - 3)));

            var cube = _cubesToShow.Dequeue();
            cube.transform.position = screenPoint + new Vector3(0, 0, 8);
            cube.SetActive(true);

            _delta = 0;

            if (_temp != GameProxy.GetScore() / 10)
            {
                _temp = GameProxy.GetScore() / 10;
                _flag = true;
            }
            if (_flag)
            {
                _speed -= 0.05f;
                _flag = false;
            }
        }

        private void OnCubeGrown(GameObject cube)
        {
            cube.SetActive(false);
            _cubesToShow.Enqueue(cube);
        }

        private void OnCubeClicked()
        {
            GameProxy.AddScore(1);
        }

        void EndGame()
        {
            foreach (KeyValuePair<GameObject, Cubes> obj in _cubes)
            {
                OnCubeGrown(obj.Key);
            }
            _speed = _startSpeed;
            gameObject.SetActive(false);
        }
    }
}