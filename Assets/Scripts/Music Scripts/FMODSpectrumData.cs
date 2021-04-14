using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class FMODSpectrumData : MonoBehaviour
{
    [FMODUnity.EventRef] public string _eventPath = null;
    public int _windowSize = 512;
    public FMOD.DSP_FFT_WINDOW _windowShape = FMOD.DSP_FFT_WINDOW.RECT;

    private FMOD.Studio.EventInstance SongPlaylist;
    private FMOD.ChannelGroup _channelGroup;
    private FMOD.DSP _dsp;
    private FMOD.DSP_PARAMETER_FFT _fftparam;

    public float[] _samples;

    [Header("Song Change")]
    public float NextPrevSong = 0f;
    //float[] SongTime = {175f, 345f, 538f, 775f, 1023f, 1318f, 1582f, 1827f, 2233f, 2450f, 2766f};//new float[12];
    //int SongTimeArray = 0;

    private void Start()
    {
        //Prepare FMOD event
        PrepareFMODeventInstance();

        _samples = new float[_windowSize];
    }

    private void PrepareFMODeventInstance()
    {
        SongPlaylist = FMODUnity.RuntimeManager.CreateInstance(_eventPath);
        //SongPlaylist.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
        SongPlaylist.start();

        FMODUnity.RuntimeManager.CoreSystem.createDSPByType(FMOD.DSP_TYPE.FFT, out _dsp);
        _dsp.setParameterInt((int)FMOD.DSP_FFT.WINDOWTYPE, (int)_windowShape);
        _dsp.setParameterInt((int)FMOD.DSP_FFT.WINDOWSIZE, _windowSize * 2);

        SongPlaylist.getChannelGroup(out _channelGroup);
        _channelGroup.addDSP(0, _dsp);
    }

    //The buttons that will change songs
    public void NextSong()
    {
        NextPrevSong += 1f;
        //SongTimeArray += 1;
        if(NextPrevSong > 11f)
        {
            NextPrevSong = 0f;
        }
    }

    public void PrevSong()
    {
        NextPrevSong -= 1f;
        //SongTimeArray -= 1;
        if(NextPrevSong < 0f)
        {
            NextPrevSong = 11f;
        }
    }

    //Automatic Song changer
    /*void AutomaticChange()
    {
        if(Time.time >= SongTime[SongTimeArray])
        {
            NextPrevSong += 1f;
            SongTimeArray += 1;
        }
    }*/

    private void Update()
    {
        GetSpectrumData();
        //AutomaticChange();
        SongPlaylist.setParameterByName("Song Changer", NextPrevSong);
    }

    private void GetSpectrumData()
    {
        System.IntPtr _data;
        uint _length;

        _dsp.getParameterData(2, out _data, out _length);
        _fftparam = (FMOD.DSP_PARAMETER_FFT)Marshal.PtrToStructure(_data, typeof(FMOD.DSP_PARAMETER_FFT));
        

        if (_fftparam.numchannels == 0)
        {
            SongPlaylist.getChannelGroup(out _channelGroup);
            _channelGroup.addDSP(0, _dsp);
        }
        else if (_fftparam.numchannels >= 1)
        {
            for (int s = 0; s < _windowSize; s++)
            {
                float _totalChannelData = 0f;
                for (int c = 0; c < _fftparam.numchannels; c++)
                    _totalChannelData += _fftparam.spectrum[c][s];
                _samples[s] = _totalChannelData / _fftparam.numchannels;
            }
        }
    }
}