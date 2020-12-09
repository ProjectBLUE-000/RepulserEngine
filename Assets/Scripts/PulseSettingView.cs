﻿using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class PulseSettingView : MonoBehaviour
{

    // TODO capsule
    [SerializeField] public InputField oscAddressField;
    [SerializeField] public InputField oscDataField;

    [SerializeField] public InputField overrideIpField;

    [SerializeField] public InputField timecodeHourInputField;
    [SerializeField] public InputField timecodeMinuteInputField;
    [SerializeField] public InputField timecodeSecondInputField;
    [SerializeField] public InputField timecodeFrameInputField;
    
    public IObservable<string> HourAsObservable => timecodeHourInputField.OnValueChangedAsObservable();
    public IObservable<string> MinuteAsObservable => timecodeMinuteInputField.OnValueChangedAsObservable();
    public IObservable<string> SecondAsObservable => timecodeSecondInputField.OnValueChangedAsObservable();
    public IObservable<string> FrameAsObservable => timecodeFrameInputField.OnValueChangedAsObservable();

    public IObservable<string> OscAddressAsObservable => oscAddressField.OnValueChangedAsObservable();
    public IObservable<string> OscDataAsObservable => oscDataField.OnValueChangedAsObservable();
    public IObservable<string> OverrideIpAsObservable => overrideIpField.OnValueChangedAsObservable();

}