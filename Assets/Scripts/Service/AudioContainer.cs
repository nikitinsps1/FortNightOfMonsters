using System.Collections.Generic;
using UnityEngine;

public class AudioContainer : MonoBehaviour
{
    [SerializeField]
    private AudioSource
        _musicSource,
        _soundSource,
        _flame;

    [SerializeField]
    private AudioClip
        _hitBuilding,
        _electric,
        _destruction,

        _swing,
        _pistol,
        _shootGun,
        _flamethrower,
        _spite,
        _explosion,

        _victory,
        _radio,

        _main,
        _scoFi,
        _wind,
        _railRoad,
        _pursuit,
        _cyber;

    private Dictionary<int, AudioClip>
        _sounds,
        _musics;

    public AudioSource Flame => _flame;

    private void Awake()
    {
        MusicInit();
        InitSounds();
    }

    public void MusicInit()
    {
        _musics = new Dictionary<int, AudioClip>()
        {
            {(int)TypeMusic.Main, _main},
            {(int)TypeMusic.SciFi, _scoFi},
            {(int)TypeMusic.Wind, _wind},
            {(int)TypeMusic.RailRoads, _railRoad},
            {(int)TypeMusic.Pursuit, _pursuit},
            {(int)TypeMusic.Cyber, _cyber}
        };
    }

    public void InitSounds()
    {
        _sounds = new Dictionary<int, AudioClip>()
        {
            {(int)TypeSound.HitBuilding, _hitBuilding},
            {(int)TypeSound.Electric, _electric},
            {(int)TypeSound.Destruction, _destruction},

            {(int)TypeSound.Swing, _swing},
            {(int)TypeSound.Pistol, _pistol},
            {(int)TypeSound.ShootGun, _shootGun},
            {(int)TypeSound.FlameThrower, _flamethrower},
            {(int)TypeSound.Spite, _spite},
            {(int)TypeSound.Explosion, _explosion},

            {(int)TypeSound.Victory, _victory},
            {(int)TypeSound.Radio, _radio},
        };
    }

    public void PlaySound(TypeSound clip, float volume)
    {
        if (_soundSource != null && clip != TypeSound.None)
        {
            _soundSource.pitch = Random.Range(0.9f, 1.01f);

            _soundSource
                .PlayOneShot
                (_sounds[(int)clip], volume);
        }

    }

    public void PlayMusic(TypeMusic clip)
    {
        if (_musicSource.clip != _musics[(int)clip])
        {
            _musicSource.clip = _musics[(int)clip];
            _musicSource.Play();
        }
    }
}