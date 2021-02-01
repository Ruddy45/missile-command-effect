﻿using UnityEngine;
using DG.Tweening;

public class FadeScreen : MonoBehaviour
{
	[SerializeField] private UnityEngine.UI.Slider[] _sliders = new UnityEngine.UI.Slider[0];
	[SerializeField] private float _duration = 1f;

	public System.Action OnFadeScreen = null;

	private void Start()
	{
		Open();
	}

	public void Open()
	{
		foreach (var slider in _sliders)
		{
			AnimationCurve animationCurve = AnimationCurve.EaseInOut(0f, slider.minValue, _duration, slider.maxValue);

			slider.DOValue(slider.minValue, _duration)
				.SetEase(animationCurve);
		}
	}

	public void CloseThenOpen()
	{
		foreach (var slider in _sliders)
		{
			AnimationCurve animationCurve = AnimationCurve.EaseInOut(0f, slider.minValue, _duration, slider.maxValue);

			slider.DOValue(slider.maxValue, _duration)
				.SetEase(animationCurve)
				.onComplete += Wait;
		}
	}

	public System.Collections.IEnumerator WaitOpen()
	{
		OnFadeScreen?.Invoke();
		yield return new WaitForSeconds(0.5f);
		Open();
	}

	private void Wait() => StartCoroutine(WaitOpen());
}
