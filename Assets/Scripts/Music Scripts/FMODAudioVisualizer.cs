using UnityEngine;
using System.Collections.Generic;
using FMODUnity;
using FMOD;
using FMOD.Studio;
using System.Runtime.InteropServices;

public class FMODAudioVisualizer : MonoBehaviour
{
    [Header("Game Performance")]
    [SerializeField] private int fps = 0;
    [Header("FMOD Event")]
    [EventRef] [SerializeField] private string eventPath = null;
    [SerializeField] private bool playOnAwake = true;
    [SerializeField] private bool flipHorizontally = false;
    [Header("Audio Sample Data Settings")]
    [SerializeField] private int windowSize = 512;
    [SerializeField] private DSP_FFT_WINDOW windowShape = DSP_FFT_WINDOW.RECT;
    [Header("Select Metering Object Prefab")]
    [SerializeField] private GameObject MeterObject = null;
    [SerializeField] private float meterIntensity = 10f;
    [SerializeField] private float SpaceBetweenMeters = 0.5f;
    [Header("Meter Speed Settings")]
    [SerializeField] private float bufferStartSpeed = 0.005f;
    [SerializeField] private float bufferAccelRate = 1.2f;
    [Header("Testing")]
    private List<float> freqRanges = new List<float>();
    private int numSampleInFirstBand = 1;

    private EventInstance SongPlaylist;
    private ChannelGroup channelGroup;
    private DSP DSPFFT;
    private DSP_PARAMETER_FFT fftparam;

    private GameObject[] bandMeters;

    private float[] _samples;
    public static float[] freqBands = new float[7];
    public static float[] bandBuffer = new float[7];
    private float[] bufferDecrease = new float[7];

    private float time = 0f;
    private int frameCount = 0;

    [Header("Song Change")]
    public float NextPrevSong = 1f;
    
    private void Start()
    {
        //Prepare FMOD event
        PrepareFMODeventInstance();
        
        //find out how many meters are needed
        SetNumberOfMeters();

        //spawn meters.
        SpawnMeterObjects();

        _samples = new float[windowSize];
        freqBands = new float[freqRanges.Count];
        bandBuffer = new float[freqRanges.Count];
        bufferDecrease = new float[freqRanges.Count];
    }

    private void PrepareFMODeventInstance()
    {
        SongPlaylist = RuntimeManager.CreateInstance(eventPath);
        SongPlaylist.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject.transform));
        if(playOnAwake)
            SongPlaylist.start();

        RuntimeManager.CoreSystem.createDSPByType(DSP_TYPE.FFT, out DSPFFT);
        DSPFFT.setParameterInt((int)DSP_FFT.WINDOWTYPE, (int)windowShape);
        DSPFFT.setParameterInt((int)DSP_FFT.WINDOWSIZE, windowSize * 2);

        SongPlaylist.getChannelGroup(out channelGroup);
        channelGroup.addDSP(0, DSPFFT);
    }

    private void SetNumberOfMeters()
    {
        float singleSizeOfOneSample = 22050f / windowSize;
        float HzForFirstBand = singleSizeOfOneSample;

        while(HzForFirstBand < 60f)
        {
            numSampleInFirstBand++;
            HzForFirstBand += singleSizeOfOneSample;
        }

        freqRanges.Clear();
        freqRanges.Add(HzForFirstBand);
        float hzRange = HzForFirstBand;
        float hzSize = HzForFirstBand;
        while(hzRange < 22050f)
        {
            hzSize *= 2;
            hzRange += hzSize;
            if (hzRange < 22050f)
                freqRanges.Add(hzRange);
        }
    }
    
    private void SpawnMeterObjects()
    {
        int posOffSet = 0;
        float spaceOffset = 0;
        bandMeters = new GameObject[freqRanges.Count];
        for(int i = 0; i < freqRanges.Count; i++)
        {
            bandMeters[i] = Instantiate(MeterObject, transform.position, transform.rotation, this.transform);
            //bandMeters[i].transform.position = new Vector3(transform.position.x + posOffSet + spaceOffset, transform.position.y, transform.position.z);

            if (flipHorizontally)
                bandMeters[i].transform.Translate(-Mathf.Pow(spaceOffset, 1f + i/100f), 0f, 0f);    // spaces increase exponentially
            else
                bandMeters[i].transform.Translate(Mathf.Pow(spaceOffset, 1f + i/100f), 0f, 0f);


            if(bandMeters[i].GetComponent<ParamCube>() != null)
            {
                bandMeters[i].GetComponent<ParamCube>()._band = posOffSet;
            }
            posOffSet++;
            spaceOffset += SpaceBetweenMeters;
        }
    }

    //The buttons that will change songs
    public void NextSong()
    {
        NextPrevSong += 1f;
        if(NextPrevSong > 11f)
        {
            NextPrevSong = 0f;
        }
    }

    public void PrevSong()
    {
        NextPrevSong -= 1f;
        if(NextPrevSong < 0f)
        {
            NextPrevSong = 11f;
        }
    }

    private void Update()
    {
        GetSpectrumData();
        FrequencyBands();
        BandBuffer();
        countFPS();

        SongPlaylist.setParameterByName("Song Changer", NextPrevSong);
    }

    private void GetSpectrumData()
    {
        System.IntPtr data;
        uint length;

        DSPFFT.getParameterData(2, out data, out length);
        fftparam = (DSP_PARAMETER_FFT)Marshal.PtrToStructure(data, typeof(DSP_PARAMETER_FFT));

        if(fftparam.numchannels == 0)
        {
            SongPlaylist.getChannelGroup(out channelGroup);
            channelGroup.addDSP(0, DSPFFT);
        }
        else if(fftparam.numchannels >= 1)
        {
            for(int b = 0; b < windowSize; b++)
            {
                float totalChannelData = 0f;
                for(int c = 0; c < fftparam.numchannels; c++)
                    totalChannelData += fftparam.spectrum[c][b];
                _samples[b] = totalChannelData / fftparam.numchannels;
            }
        }
    }

    private void FrequencyBands()
    {
        int counter = 0;
        for(int i = 0; i < freqRanges.Count; i++)
        {
            float average = 0f;
            int numSampleInThisBand = numSampleInFirstBand * (int)Mathf.Pow(2, i);

            for(int j = 0; j < numSampleInThisBand; j++)
            {
                average += _samples[counter] * (counter + 1);
                counter++;
            }
            average /= counter;
            freqBands[i] = average * meterIntensity;
        }
    }
    
    private void BandBuffer()
    {
        for(int i = 0; i < freqRanges.Count; i++)
        {
            if(freqBands[i] > bandBuffer[i])
            {
                bandBuffer[i] = freqBands[i];
                bufferDecrease[i] = bufferStartSpeed;
            }

            if(freqBands[i] < bandBuffer[i])
            {
                bandBuffer[i] -= bufferDecrease[i];
                bufferDecrease[i] *= bufferAccelRate;
            }

            if(bandBuffer[i] < 0)
                bandBuffer[i] = 0f;
        }
    }

    private void countFPS()
    {
        time += Time.deltaTime;
        if(time > 1f)
        {
            time = 0f;
            fps = 0;
            fps += frameCount;
            frameCount = 0;
        }
        frameCount++;
    }

    public void PlayFMODEvent()
    {
        SongPlaylist.start();
    }

    public void StopFMODEvent()
    {
        SongPlaylist.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void PauseFMODEvent()
    {
        bool p = false;
        SongPlaylist.getPaused(out p);
        p = !p;
        SongPlaylist.setPaused(p);
    }

    private void OnDestroy()
    {
        SongPlaylist.release();
    }
}