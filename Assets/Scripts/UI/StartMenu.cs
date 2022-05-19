using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private TransitionPane _transitionPane;

    public void OnStartButtonClick() {
        _transitionPane.FadeIn();
    }
}
