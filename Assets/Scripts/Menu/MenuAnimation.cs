using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAnimation : MonoBehaviour
{
    [SerializeField] private GameObject _pressButtonText;
    [SerializeField] private GameObject _title;
    [SerializeField] private GameObject _menu;
    [SerializeField] private ParticleSystem _explosion;
    
    [SerializeField] private MenuSounds _menuSounds;
    [SerializeField] private float _startDelay;
    [SerializeField] private float _repeatRate;

    private Animator _animator;
    private bool isPlayerHere;

    private void Awake()
    {
        _animator = GetComponent<Animator>();    
    }

    private void Update()
    {
        if (Input.anyKeyDown && !isPlayerHere)
        {
            isPlayerHere = true;
            _animator.SetTrigger("enterMenu");
            ShowMenuWithExplosion();
            _menuSounds.PlayExplosionSound();
        }
    }

    private void ShowMenuWithExplosion()
    {
        //_pressButtonText.SetActive(false);
        //CancelInvoke("BlinkText");
        //ParticleSystem explosion = Instantiate(_explosion, new Vector2(0, -2), Quaternion.identity);
        //explosion.gameObject.transform.localScale *= 8;
        //StartCoroutine(MoveTitleAndShowMenu());
    }

    IEnumerator MoveTitleAndShowMenu()
    {
        for (int i = 60; i > 0; i--)
        {
            _title.transform.position += Vector3.up * i / 20;
            yield return new WaitForSecondsRealtime(1 / 30);
        }
        _menu.SetActive(true);
    }
}
