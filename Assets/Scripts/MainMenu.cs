using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pressButtonText;
    [SerializeField] private float _startDelay;
    [SerializeField] private float _repeatRate;
    void Start()
    {
        InvokeRepeating("BlinkText", _startDelay, _repeatRate);
    }

    private void BlinkText()
    {
        _pressButtonText.SetActive(!_pressButtonText.activeSelf); 
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("Game");
        }
    }
}
