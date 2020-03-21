using System;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameProxy GameProxy;
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private Text _displayText;
    [SerializeField] private float _delta;
    [SerializeField] private float _speed;
    private float _startSpeed;
    private int _temp;
    private bool _flag;

    private void Start()
    {
        _startSpeed = _speed;
    }

    private void Update()
    {
        _delta += Time.deltaTime;
        if (_delta > _speed)
        {
            Vector3 screenPoint = Camera.main.ScreenToWorldPoint(new Vector2(UnityEngine.Random.Range(3f, Camera.main.pixelWidth-3),
                UnityEngine.Random.Range(3f, Camera.main.pixelHeight-3)));

            GameObject instanceObj = Instantiate(_cubePrefab, screenPoint + new Vector3(0, 0, 8), Quaternion.identity);
            instanceObj.transform.parent = gameObject.transform;

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

            if (transform.childCount > 10)
            {
                gameObject.SetActive(false);
                _speed = _startSpeed;
            }
        }

        _displayText.text = "Score: " + GameProxy.Score;
    }
}
