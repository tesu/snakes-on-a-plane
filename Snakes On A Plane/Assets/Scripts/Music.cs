using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music {
    private AudioSource song;
    private int beatCount;
    private bool newBeat;
    public float secondsPerBeat;
    private float leeway;
    private float offset;
    private float totalTime;
    private bool playing;

    public Music(AudioSource audio, int bpm, float initialOffset = 0)
    {
        song = audio;
        secondsPerBeat = (float)60 / bpm;
        leeway = secondsPerBeat / 5 * 2;
        offset = initialOffset;
        beatCount = 0;
        totalTime = 0;
        newBeat = false;
        playing = false;
    }

    // Needed to initialize the beat visualizer
    public float GetLeeway() 
    {
        return this.leeway;
    }

    public int GetBeatCount()
    {
        return beatCount;
    }

    public void UpdateTime(float delta)
    {
        if (!playing) {
            song.Play();
            playing = true;
        }
        offset += delta;
        totalTime += delta;
        while (offset >= secondsPerBeat)
        {
            offset -= secondsPerBeat;
            beatCount++;
            newBeat = true;
        }
    }

    public bool WithinLeeway()
    {
        Debug.Log("offset: "+offset);
        return offset < leeway;
    }

    public bool IsNewBeat()
    {
        if (newBeat)
        {
            newBeat = false;
            return true;
        }
        return false;
    }

    public float[] GetBeatsForNextNSeconds(float n)
    {
        int size = 0;
        for (float time = secondsPerBeat - offset; time < n; time += secondsPerBeat) size++;

        float[] output = new float[size];
        for (int i = 0; i < output.Length; i++)
        {
            output[i] = (secondsPerBeat - offset) + secondsPerBeat * i;
        }
        return output;
    }
}
