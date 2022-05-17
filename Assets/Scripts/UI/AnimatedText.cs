using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class AnimatedText : MonoBehaviour
{
    [SerializeField] string _TextToAnimate;
    public string TextToAnimate { get { return _TextToAnimate; } set { _TextToAnimate = value; } }


    [SerializeField] TMP_Text _tmpText;
    [SerializeField] float _animationTime;

    private void Awake()
    {
        _tmpText = GetComponent<TMP_Text>();
        UpdateUI(string.Empty);
    }

    float _elapsed = 0;
    public IEnumerator Animate()
    {
        float stepTime =  _animationTime/ _TextToAnimate.Length;
        string currentText = string.Empty;
        foreach (var c in _TextToAnimate)
        {
            while(_elapsed < stepTime)
            {
                _elapsed += Time.unscaledDeltaTime;
                yield return new WaitForEndOfFrame();
            }
            currentText += c;
            UpdateUI(currentText);
            _elapsed = 0;
        }
    }

    private void UpdateUI(string txt)
    {
        _tmpText.text = txt;
    }
}
