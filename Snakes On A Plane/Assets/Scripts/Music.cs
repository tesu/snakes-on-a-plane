using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music {
    private AudioSource song;
    private int beatCount;
    private bool newBeat;
    private float secondsPerBeat;
    private float leeway;
    private float offset;

    public Music(AudioSource audio, int bpm, float initialOffset = 0)
    {
        song = audio;
        secondsPerBeat = (float)60 / bpm;
        leeway = secondsPerBeat / 5;
        offset = initialOffset;
        beatCount = 0;
        newBeat = true;
        song.Play();
    }

    public void UpdateTime(float delta)
    {
        offset += delta;
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
        return offset < leeway || offset > secondsPerBeat - leeway;
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
}
