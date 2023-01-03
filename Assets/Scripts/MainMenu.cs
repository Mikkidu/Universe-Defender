using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;


public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pressButtonText;
    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _title;
    [SerializeField] private GameObject _spaceship;
    [SerializeField] private ParticleSystem _explosion;
    [SerializeField] private float _startDelay;
    [SerializeField] private float _repeatRate;
    
    
    private bool isPlayerHere;
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
        if (Input.anyKeyDown && !isPlayerHere)
        {
            isPlayerHere = true;
            StartCoroutine(EnterMenu());
        }
    }

    IEnumerator EnterMenu()
    {
        _pressButtonText.SetActive(false);
        CancelInvoke();
        Instantiate(_explosion, _spaceship.transform.position, Quaternion.identity).
            gameObject.transform.localScale *= 4;
        _spaceship.SetActive(false);
        for (int i = 15; i > 0; i--)
        {
            _title.transform.position += Vector3.up * i;
            yield return new WaitForSecondsRealtime(1 / 30);
        }
        
        _menu.SetActive(true);
    }
    
}
