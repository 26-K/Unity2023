using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] SEDatas seDatas;

    public List<AudioClip> playWaitList = new List<AudioClip>();
    public List<AudioClip> playWaitIgnorePitchList = new List<AudioClip>();
    int waitFlame = 0;
    protected override void UnityAwake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        waitFlame--;
        if (playWaitList.Count >= 1 && waitFlame <= 0) //一度に同時になる可能性のある効果音はタイミングをずらして鳴らす
        {
            waitFlame = 2;
            audioSource.pitch = Random.Range(0.95f,1.05f);
            audioSource.PlayOneShot(playWaitList[0]);
            playWaitList.RemoveAt(0);
        }
        else if (playWaitIgnorePitchList.Count >= 1 && waitFlame <= 0)
        {
            waitFlame = 2;
            audioSource.pitch = 1.0f;
            audioSource.PlayOneShot(playWaitIgnorePitchList[0]);
            playWaitIgnorePitchList.RemoveAt(0);
        }
    }

    public void PlayCardHoverSound()
    {
        audioSource.PlayOneShot(seDatas.cardHover);
    }

    public void PlayCardDrawSound()
    {
        AddPlayWaitIgnorePitchSoundEffect(seDatas.drawCard);
    }
    public void PlayNailHitSound()
    {
        AddPlayWaitSoundEffect(seDatas.nailHit);
    }
    public void PlayWoodHitSound()
    {
        AddPlayWaitSoundEffect(seDatas.wallHit);
    }
    public void PlayCardSelectSound()
    {
        audioSource.PlayOneShot(seDatas.cardSelect);
    }

    public void PlayOneMoreSound()
    {
        audioSource.PlayOneShot(seDatas.oneMore);
    }
    public void PlayBattleStartSound()
    {
        AddPlayWaitIgnorePitchSoundEffect(seDatas.battleStart);
    }
    public void PlayTurnStartSound()
    {
        audioSource.pitch = 1.0f;
        AddPlayWaitIgnorePitchSoundEffect(seDatas.turnStart);
    }
    public void PlayGateInSound()
    {
        AddPlayWaitSoundEffect(seDatas.gateIn);
    }
    public void PlayDuplicateSound()
    {
        AddPlayWaitSoundEffect(seDatas.duplicate);
    }
    public void PlaySetObjectSound()
    {
        AddPlayWaitSoundEffect(seDatas.setObject);
    }
    public void PlayFireImpactSound()
    {
        AddPlayWaitSoundEffect(seDatas.fireImpact);
    }
    void AddPlayWaitSoundEffect(AudioClip audioClip)
    {
        int maxWaitSoundEffectCount = 15;
        if (playWaitList.Count >= maxWaitSoundEffectCount) //連続でならす効果音待ちが多すぎる場合鳴らさない
        {
            return;
        }
        playWaitList.Add(audioClip);
    }
    void AddPlayWaitIgnorePitchSoundEffect(AudioClip audioClip)
    {
        int maxWaitSoundEffectCount = 15;
        if (playWaitIgnorePitchList.Count >= maxWaitSoundEffectCount) //連続でならす効果音待ちが多すぎる場合鳴らさない
        {
            return;
        }
        playWaitIgnorePitchList.Add(audioClip);
    }
}
