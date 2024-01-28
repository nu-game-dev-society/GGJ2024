using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiPopup : MonoBehaviour
{
    private CanvasGroup _canvasGroup;
    public float ReenableAlpha = 2.0f;
    public float FadeSpeed = 1.0f;

    private float _alpha;
    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnEnable()
    {
        _alpha = ReenableAlpha;
    }
    void Update()
    {
        if (_alpha <= 0.0f)
        {
            gameObject.SetActive(false);
            return;
        }

        _alpha -= Time.deltaTime * FadeSpeed;
        _canvasGroup.alpha = _alpha;
    }
}
