using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public abstract class AudioBlock
{
    public abstract string GetName();
    public abstract AudioClip GetAudioClip();
    public static implicit operator AudioBlock(AudioClip clip)
    {
        return new AudioBlockSimple(clip);
    }
}

[Serializable]
public class AudioBlockSimple : AudioBlock
{
    public AudioClip clip;
    
    public override string GetName()
    {
        return clip.name;
    }

    public override AudioClip GetAudioClip()
    {
        return clip;
    }

    public AudioBlockSimple(AudioClip clip)
    {
        this.clip = clip;
    }

    public static implicit operator AudioBlockSimple(AudioClip clip)
    {
        return new AudioBlockSimple(clip);
    }
}

[Serializable]
public class AudioBlockSequence : AudioBlock
{
    public string name;
    public List<AudioClip> clips = new();
    private int index;
    
    public override string GetName()
    {
        return name;
    }

    public override AudioClip GetAudioClip()
    {
        index = (index + 1) % clips.Count;
        return clips[index];
    }
}

[Serializable]
public class AudioBlockRandom : AudioBlock
{
    public string name;
    public List<AudioClip> clips = new();
    
    public override string GetName()
    {
        return name;
    }

    public override AudioClip GetAudioClip()
    {
        return clips[Random.Range(0,clips.Count)];
    }
}



