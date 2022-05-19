using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionPane : MonoBehaviour
{
    public UIManager.EventTransitionPaneFade OnTransitionPaneFade;

    [SerializeField] private Animation _transitionAnimation;
    [SerializeField] private AnimationClip _fadeOutAnimation;
    [SerializeField] private AnimationClip _fadeInAnimation;

    private void Start() {
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChange);
        LoadManager.Instance.OnLoadComplete.AddListener(HandleLoadComplete);
    }

    private void HandleGameStateChange(GameManager.GameState current, GameManager.GameState prev) {
        if(current == GameManager.GameState.LOADING) FadeIn();
    }

    private void HandleLoadComplete() {
        FadeOut();
    }

    public void FadeIn() {
        gameObject.SetActive(true);
        _transitionAnimation.Stop();
        _transitionAnimation.clip = _fadeInAnimation;
        _transitionAnimation.Play();

    }

    public void FadeOut() {
        UIManager.Instance.SetDummyCameraActive(false);

        _transitionAnimation.Stop();
        _transitionAnimation.clip = _fadeOutAnimation;
        _transitionAnimation.Play();
    }

    private void OnFadeInComplete() {
        OnTransitionPaneFade.Invoke(true);
    }

    private void OnFadeOutComplete() {
        gameObject.SetActive(false);
        OnTransitionPaneFade.Invoke(false);
    }
}
