using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Плавна райдужна анімація кольору для UI Image.
/// Використовує HSV-простір для ідеального переходу між кольорами.
/// </summary>
[RequireComponent(typeof(Image))]
public class RainbowImage : MonoBehaviour
{
    [Header("Швидкість і ритм")]
    [Tooltip("Кількість повних циклів райдуги за секунду")]
    [Range(0.01f, 5f)]
    public float cyclesPerSecond = 0.3f;

    [Header("Насиченість і яскравість")]
    [Range(0f, 1f)] public float saturation = 1f;
    [Range(0f, 1f)] public float brightness = 1f;

    [Header("Зміщення фази (0–1)")]
    [Tooltip("Зручно для групи об'єктів — щоб кожен стартував з іншого кольору")]
    [Range(0f, 1f)] public float phaseOffset = 0f;

    private Image _image;
    private float _hue;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void Update()
    {
        // Рівномірний рух по колу кольорів (HSV)
        _hue = Mathf.Repeat(Time.time * cyclesPerSecond + phaseOffset, 1f);
        _image.color = Color.HSVToRGB(_hue, saturation, brightness);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        // Превʼю прямо в Editor без Play Mode
        if (_image == null) _image = GetComponent<Image>();
        float previewHue = Mathf.Repeat(phaseOffset, 1f);
        _image.color = Color.HSVToRGB(previewHue, saturation, brightness);
    }
#endif
}
