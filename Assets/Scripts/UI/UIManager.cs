using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : Singleton<UIManager>
{
    [System.Serializable] public class EventTransitionPaneFade : UnityEvent<bool> { }
    public EventTransitionPaneFade OnTransitionPaneFade;

    [SerializeField] private StartMenu _startMenu;
    [SerializeField] private Camera _dummyCamera;
    [SerializeField] private TransitionPane _transitionPane;

    private void Start() {
        _transitionPane.OnTransitionPaneFade.AddListener(HandleTransitionPaneFade);
    }

    private void HandleTransitionPaneFade(bool isFadeIn)
    {
        _startMenu.gameObject.SetActive(false);
        if(isFadeIn) SetDummyCameraActive(true);

        OnTransitionPaneFade.Invoke(isFadeIn);
    }

    public void SetDummyCameraActive(bool active) {
        _dummyCamera.gameObject.SetActive(active);
    }
}
