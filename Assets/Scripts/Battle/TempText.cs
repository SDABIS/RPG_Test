using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempText : MonoBehaviour
{
    [SerializeField] Text textField;
    [SerializeField] float upSpeed = 0.1f;
    [SerializeField] float duration = 1;

    private Vector3 initialPos;

    private void Awake() {
        initialPos = GetComponent<RectTransform>().localPosition;
    }

    public void Activate(string text) {
        gameObject.SetActive(true);
        transform.localPosition = initialPos;
        ShowText(text);
        Invoke("Disappear", duration);
    }

    private void ShowText(string text) {
        textField.text = text;
        StartCoroutine(GoUp());

    }

    private void Disappear() {
        gameObject.SetActive(false);
    }

    IEnumerator GoUp() {
        while(true) {
            
            yield return null;
            transform.Translate(Vector3.up * Time.deltaTime * upSpeed);

        }
    }
}
