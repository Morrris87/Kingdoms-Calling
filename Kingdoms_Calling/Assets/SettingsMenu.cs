using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
	public AudioMixer audioMixer;

	public void Setvolume_Music (float volumeM)
	{
		audioMixer.SetFloat("MusicVolume", volumeM);
	}
	public void Setvolume_Effects (float volumeE)
	{
		audioMixer.SetFloat("EffectsVolume", volumeE);
	}
	public void Setvolume_Master(float volumeMaster)
	{
		audioMixer.SetFloat("MasterVolume", volumeMaster);
	}

	public void SetQuality (int qualityIndex)
	{
		QualitySettings.SetQualityLevel(qualityIndex);
	}
}
