using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager soundManager;
    public float volumenSound;
    public float volumeMusic;
    [SerializeField]
    AudioMixer audioMixer;

    [SerializeField]
    AudioMixer musicMixer;

    [SerializeField]
    AudioClip buff;

    [SerializeField]
    AudioSource playOneShot;
    [SerializeField]
    AudioSource explorationMusic;

    [SerializeField]
    AudioClip button;
    [SerializeField]
    AudioClip combateIntro;

    [SerializeField]
    AudioSource combateMusic;

    [SerializeField]
    AudioClip crit;
    [SerializeField]
    AudioClip defeat;
    [SerializeField]
    AudioClip victory;
    [SerializeField]
    AudioClip evasion;
    [SerializeField]
    AudioClip hit;
    [SerializeField]
    AudioClip heal;
    [SerializeField]
    AudioClip qte;
    [SerializeField]
    AudioClip stingerBattleFound;
    [SerializeField]
    AudioClip battleStarts;
    [SerializeField]
    AudioClip specialBarFilled;
    [SerializeField]
    AudioClip dialogue;

    void Awake()
    {
        if (soundManager == null)
        {
            soundManager = this;
            DontDestroyOnLoad(this.gameObject);
            explorationMusic.Play();
        }
        else
        {
            Destroy(this.gameObject);
        }

        volumenSound = 0;
        volumeMusic = 0;
    }

    // Update is called once per frame
    public void PlayBuff()
    {
        playOneShot.PlayOneShot(buff);
    }

    public void PlayCrit()
    {
        playOneShot.PlayOneShot(crit);
    }

    public void PlayVictory()
    {
        playOneShot.PlayOneShot(victory);
    }

    public void PlayDefeat()
    {
        playOneShot.PlayOneShot(defeat);
    }

    public void PlayEvasion()
    {
        playOneShot.PlayOneShot(evasion);
    }

    public void PlayButton()
    {
        playOneShot.PlayOneShot(button);
    }

    public void PlayQTE()
    {
        playOneShot.PlayOneShot(qte);
    }
    public void PlayBattleMusic()
    {
        combateMusic.Play();
    }
    public void PlayHeal()
    {
        playOneShot.PlayOneShot(heal);
    }

    public void PlayHit()
    {
        playOneShot.PlayOneShot(hit);
    }

    public void StopCombatMusic()
    {
        combateMusic.Stop();
    }

    public void PlayStingerBattleFound()
    {
        playOneShot.PlayOneShot(stingerBattleFound);
    }

    public void PlayBattleStarts()
    {
        playOneShot.PlayOneShot(battleStarts);
    }

    public void PlaySpecialBarFilled()
    {
        playOneShot.PlayOneShot(specialBarFilled);
    }

    public void PlayDialogue()
    {
        playOneShot.PlayOneShot(dialogue);
    }

    public void StopExplorationMusic()
    {
        var allAudioSources = FindObjectsOfType<AudioSource>();

        foreach (AudioSource a in allAudioSources)
        {
            a.Stop();
        }
    }

    public void PlayExplorationMusic()
    {
        explorationMusic.Play();
    }


    public IEnumerator PlayAfterIntro()
    {
        PlayBattleMusic();
        yield return null;
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
        volumenSound = volume;
    }

    public void SetVolumeMusic(float volume)
    {
        musicMixer.SetFloat("Volume", volume);
        volumeMusic = volume;
    }

}
