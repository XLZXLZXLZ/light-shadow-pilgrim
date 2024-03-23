using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.Utilities;
using Unity.VisualScripting;
using UnityEngine;
using Sequence = DG.Tweening.Sequence;

public class AudioManager : ManagerBase<AudioManager>
{
    // 存有BGM和音效引用的字典
    private Dictionary<string, AudioBlock> bgmDic = new();
    private Dictionary<string, AudioBlock> seDic = new();
    
    // 相关组件和Root
    [SerializeField] private GameObject audioRoot;
    private AudioSource bgmComponent;
    private AudioSource seComponent;

    // 音量大小相关
    public float GlobalVolume => GlobalVolumeInSettings;
    public float BgmVolume => GlobalVolumeInSettings * BgmVolumeInSettings;
    public float SeVolume => GlobalVolumeInSettings * SeVolumeInSettings;

    public float GlobalVolumeInSettings { get; private set; }
    public float BgmVolumeInSettings { get; private set; }
    public float SeVolumeInSettings { get; private set; }

    private float fadeInOutDuration = 1f;
    
    public override void Awake()
    {
        base.Awake();
        
        AudioSettings settings = Root.GetRuntimeSettings().audioSettings;
        SYInitModule initModule = Root.initModule;
        
        // 添加相关组件
        if(audioRoot == null)
            audioRoot = Instantiate(new GameObject(),transform);
        DontDestroyOnLoad(audioRoot);
        audioRoot.name = "AudioRoot";
        bgmComponent = audioRoot.AddComponent<AudioSource>();
        seComponent = audioRoot.AddComponent<AudioSource>();
        
        // 加载相关配置
        LoadSettings(settings);
        LoadInitModule(initModule);
    }

    #region Load

    /// <summary>
    /// 加载音量，bgm是否循环播放等相关数据配置
    /// </summary>
    /// <param name="settings"></param>
    private void LoadSettings(AudioSettings settings)
    {
        if (settings == null)
        {
            settings = new();
#if UNITY_EDITOR
            SYLog.LogWarning("传入的AudioSettings为空，将使用默认设置");
#endif
        }
        
        bgmComponent.loop = settings.isBgmLoop;
        seComponent.loop = false;

        GlobalVolumeInSettings = settings.globalVolume;
        BgmVolumeInSettings = settings.bgmVolume;
        SeVolumeInSettings = settings.seVolume;
        fadeInOutDuration = settings.fadeInOutDuration;
        UpdateVolume();
    }

    /// <summary>
    /// 加载基础模块
    /// </summary>
    /// <param name="initModule"></param>
    private void LoadInitModule(SYInitModule initModule)
    {
        if (initModule.bgmContainers != null)
        {
            foreach (var bgmContainer in initModule.bgmContainers)
            {
                LoadBgms(bgmContainer);
            }
        }
        if (initModule.seContainers != null)
        {
            foreach (var seContainer in initModule.seContainers)
            {
                LoadSes(seContainer);
            }
        }
    }

    /// <summary>
    /// 加载Bgm配置
    /// </summary>
    /// <param name="bgmContainer"></param>
    /// <param name="soundEffectContainer"></param>
    public void LoadBgms(BgmContainer bgmContainer)
    {
        if (bgmContainer != null && !bgmContainer.bgms.IsNullOrEmpty())
        {
            foreach (var bgm in bgmContainer.bgms)
            {
                if(bgm == null) continue;
                bgmDic.Add(bgm.GetName(), bgm);
            }
        }
#if UNITY_EDITOR
        else
        {
            SYLog.LogWarning("AudioManager：要加载的Bgm包为空！");
        }
#endif
        
    }

    /// <summary>
    /// 加载音效配置
    /// </summary>
    /// <param name="soundEffectContainer"></param>
    public void LoadSes(SoundEffectContainer soundEffectContainer)
    {
        if (soundEffectContainer != null && !soundEffectContainer.soundEffects.IsNullOrEmpty())
        {
            foreach (var se in soundEffectContainer.soundEffects)
            {
                if(se == null) continue;
                bgmDic.Add(se.GetName(), se);
            }
        }
#if UNITY_EDITOR
        else
        {
            SYLog.LogWarning("AudioManager：要加载的音效包为空！");
        }
#endif
    }
    
    /// <summary>
    /// 卸载Bgm配置
    /// </summary>
    /// <param name="bgmContainer"></param>
    /// <param name="soundEffectContainer"></param>
    public void UnloadBgms(BgmContainer bgmContainer)
    {
        if (bgmContainer != null && !bgmContainer.bgms.IsNullOrEmpty())
        {
            foreach (var bgm in bgmContainer.bgms)
            {
                if(bgm == null && !bgmDic.ContainsKey(bgm.GetName())) continue;
                bgmDic.Remove(bgm.GetName());
            }
        }
#if UNITY_EDITOR
        else
        {
            SYLog.LogWarning("AudioManager：要卸载的Bgm包为空！");
        }
#endif
        
    }

    /// <summary>
    /// 卸载音效配置
    /// </summary>
    /// <param name="soundEffectContainer"></param>
    public void UnloadSes(SoundEffectContainer soundEffectContainer)
    {
        if (soundEffectContainer != null && !soundEffectContainer.soundEffects.IsNullOrEmpty())
        {
            foreach (var se in soundEffectContainer.soundEffects)
            {
                if(se == null && !seDic.ContainsKey(se.GetName())) continue;
                bgmDic.Remove(se.GetName());
            }
        }
#if UNITY_EDITOR
        else
        {
            SYLog.LogWarning("AudioManager：要卸载的音效包为空！");
        }
#endif
    }

    /// <summary>
    /// 卸载所有Bgm和音频音效配置
    /// </summary>
    public void UnloadAllAudios()
    {
        bgmDic.Clear();
        seDic.Clear();
    }

    #endregion

    #region Set

    public void SetGlobalVolume(float globalVolume)
    {
        GlobalVolumeInSettings = globalVolume;
        UpdateVolume();
    }

    public void SetVolume(float bgmVolume,float seVolume)
    {
        BgmVolumeInSettings = bgmVolume;
        SeVolumeInSettings = seVolume;
        UpdateVolume();
    }

    private void UpdateVolume()
    {
        bgmComponent.volume = BgmVolume;
        seComponent.volume = SeVolume;
    }

    public void SetBgmLoop(bool isLoop)
    {
        bgmComponent.loop = true;
    }

    #endregion

    #region Use

    public void PlayBgm(string bgmName, bool isFadeInOut = true)
    {
        if (bgmDic.ContainsKey(bgmName))
        {
            PlayBgm(bgmDic[bgmName], isFadeInOut);
        }
#if UNITY_EDITOR
        else
        {
            SYLog.LogWarning($"AudioManager：没有在音频模块中找到名称为{bgmName}的Bgm");
        }
#endif
        
    }

    public void PlayBgm(AudioBlock audioBlock, bool isFadeInOut = true)
    {
        if (audioBlock != null)
        {
            PlayBgm(audioBlock.GetAudioClip(),isFadeInOut);
        }
#if UNITY_EDITOR
        else
        {
            SYLog.LogWarning("AudioManager：传入的AudioBlock为空！");
        }
#endif
    }

    public void PlayBgm(AudioClip clip, bool isFadeInOut = true)
    {
        if (clip != null)
        {
#if UNITY_EDITOR
            SYLog.LogWarning("AudioManager：请求播放的音效为空！");
#endif
            return;
        }

        if (isFadeInOut)
        {
            AudioSource lastBgmComponent = bgmComponent;
            Sequence sequence = DOTween.Sequence();
            if (lastBgmComponent.isPlaying && lastBgmComponent.clip != null)
            {
                sequence
                    .Append(lastBgmComponent.DOFade(0, fadeInOutDuration).OnComplete(() => Destroy(lastBgmComponent)));
                bgmComponent = audioRoot.AddComponent<AudioSource>();
            }
            sequence
                .Append(bgmComponent.DOFade(BgmVolume, fadeInOutDuration))
                .OnComplete(() =>
                {
                    bgmComponent.clip = clip;
                    bgmComponent.Play();
                });
        }
        else
        {
            bgmComponent.clip = clip;
            bgmComponent.Play();
        }
    }

    public void StopBgm(bool isFadeInOut = true)
    {
        if (isFadeInOut)
        {
            bgmComponent.DOFade(0, fadeInOutDuration)
                .OnComplete(bgmComponent.Stop);
        }
        else
        {
            bgmComponent.Stop();
        }
    }

    public void PlaySe(string seName)
    {
        if (seDic.ContainsKey(seName))
        {
            PlaySe(seDic[seName]);
        }
#if UNITY_EDITOR
        else
        {
            SYLog.LogWarning($"AudioManager：没有在音频模块中找到名称为{seName}的音效");
        }
#endif
    }

    public void PlaySe(AudioBlock audioBlock)
    {
        if (audioBlock != null)
        {
            PlaySe(audioBlock.GetAudioClip());
        }
#if UNITY_EDITOR
        else
        {
            SYLog.LogWarning("AudioManager：传入的AudioBlock为空！");
        }
#endif
    }

    public void PlaySe(AudioClip clip)
    {
        if (clip != null)
        {
            seComponent.PlayOneShot(clip);
        }
#if UNITY_EDITOR
        else
        {
            SYLog.LogWarning("AudioManager：请求播放的音效为空！");
        }
#endif
    }

    #endregion
}
