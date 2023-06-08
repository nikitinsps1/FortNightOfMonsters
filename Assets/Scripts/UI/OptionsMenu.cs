using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField]
    private AudioMixerGroup _mixer;

    [SerializeField]
    private Slider
        _sliderSounds;

    [SerializeField]
    private Toggle _toggle;

    private void Awake()
    {
        _sliderSounds.maxValue = 0;
        _sliderSounds.minValue = -25;
    }

    private void OnEnable()
    {
        float musicValue;
        _mixer.audioMixer.GetFloat("Volume", out musicValue);
        _sliderSounds.value = musicValue;

        if (QualitySettings.GetQualityLevel() == 0)
        {
            _toggle.isOn = false;
        }
        else
        {
            _toggle.isOn = true;
        }

    }

    public void SetQualityGraphics(bool enable)
    {
        if (enable)
        {
            QualitySettings.SetQualityLevel(1);
        }
        else
        {
            QualitySettings.SetQualityLevel(0);
        }

    }

    public void ChangeSoundsVolume(float value)
    {
        if (value < -24)
        {
            _mixer.audioMixer.SetFloat("Volume", -80);
        }
        else
        {
            _mixer.audioMixer.SetFloat("Volume", value);
        }
    }
}
