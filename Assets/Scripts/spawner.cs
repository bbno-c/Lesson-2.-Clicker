using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private GameObject _display;
    [SerializeField] private Text _displayText;
    [SerializeField] private int _score;
    [SerializeField] private float _delta;
    [SerializeField] private float _speed;
    private float _startSpeed;
    private int _temp;
    private bool _flag;

    private void Start()
    {
        _startSpeed = _speed;
        _displayText = _display.GetComponent<Text>();
    }

    public int Score
    {
        get => _score;
        set => _score++;
    }

    private void Update()
    {
        
        _delta += Time.deltaTime;
        if (_delta > _speed)
        {
            Vector3 screen_point = Camera.main.ScreenToWorldPoint(
                new Vector2(
                Random.Range(3f, Camera.main.pixelWidth-3),
                Random.Range(3f, Camera.main.pixelHeight-3)
                )
            );

            GameObject instanceObj = Instantiate(_cubePrefab, screen_point + new Vector3(0, 0, 8), Quaternion.identity);
            instanceObj.transform.parent = gameObject.transform;
            instanceObj.GetComponent<Cubes>().SpawnerSciptConnect(this);

            _delta = 0;

            if (_temp != _score / 10)
            {
                _temp = _score / 10;
                _flag = true;
            }
            if (_flag)
            {
                _speed -= 0.05f;
                _flag = false;
            }

            if (transform.childCount > 10)
            {
                foreach (Transform child in transform)
                {
                    Destroy(child.gameObject);
                }
                _score = 0;
                _speed = _startSpeed;
            }
        }

        _displayText.text = "Score: " + _score;
    }
}
