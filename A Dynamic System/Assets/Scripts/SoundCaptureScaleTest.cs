using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

namespace Lunity.AudioVis
{

    //A simple component to visualize the raw audio data coming out of SoundCapture
    public class SoundCaptureScaleTest : MonoBehaviour
    {
        public SoundCapture.DataSource Target = SoundCapture.DataSource.AverageVolume;
        public int BandIndex = 0;

        public float MinScale = 0.1f;
        public float ScaleAmount = 1f;

        private SoundCapture _sc;
        private AudioAverageSet _averageSet;


        [HideInInspector]
        public Renderer objRenderer;

        private Color _color;
        private Color _bgColor;
        private Color _dwnColor;
        private Color _planeColor;

        public void Awake()
        {
            _sc = FindObjectOfType<SoundCapture>();
            _averageSet = FindObjectOfType<AudioAverageSet>();
            objRenderer = GetComponent<Renderer>();
        }

        public void Update()
        {
            transform.localScale = new Vector3(MinScale, MinScale + 5 * GetValue(), MinScale);

            if (this.CompareTag("BG"))
            {
                transform.localScale = new Vector3(10f, MinScale + 5 * GetValue(), MinScale);

                if (_averageSet.Pulse > 0)
                {
                    _bgColor = new Color(0, _averageSet.Pulse * 20, 0);                  
                }

                objRenderer.material.SetColor("_Color", _bgColor);
            }

            if (this.CompareTag("Cubes"))
            {
                if (_averageSet.Flicker > 0)
                {
                    _color = new Color( Random.value * (_averageSet.Flicker * 20), 0, 0, 1);
                }

                objRenderer.material.SetColor("_Color", _color);
            }

            if (this.CompareTag("DWN"))
            {
                transform.localScale = new Vector3(10f, MinScale + 5 * GetValue(), MinScale);

                if (_averageSet.Vibe > 0)
                {
                    _dwnColor = new Color(0f, 0f, Random.value * (_averageSet.Vibe * 20));
                }

                objRenderer.material.SetColor("_Color", _dwnColor);
            }

            if (this.CompareTag("Plane"))
            {
                transform.localScale = new Vector3(100, 100, 100);
                if (GetValue() > 0)
                {
                    _planeColor = new Color(0f, GetValue() * 20, GetValue() * 20, 1);
                }

                objRenderer.material.SetColor("_Color", _planeColor);
            }
            
                       
        }

        private float GetValue()
        {
            switch (Target) {
                case SoundCapture.DataSource.AverageVolume:
                    return _sc.AverageVolume;
                case SoundCapture.DataSource.PeakVolume:
                    return _sc.PeakVolume;
                case SoundCapture.DataSource.SingleBand:
                    return _sc.BarData[Mathf.Clamp(BandIndex, 0, _sc.BarData.Length)];
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}