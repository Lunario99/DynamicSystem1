using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lunity.AudioVis
{
    public class SoundCaptureBinsScaleTest : MonoBehaviour
    {
        private SoundCapture _sc;
        
        public void Start()
        {
            StartCoroutine(InitializeAfterBarData());
        }

        private IEnumerator InitializeAfterBarData()
        {
            //waits for the sound capture FFT data to be initially populated, then creates the cubes
            _sc = FindObjectOfType<SoundCapture>();
            while (_sc.BarData.Length == 0) yield return null;
            for (var i = 0; i < _sc.BarData.Length; i++) {
                var newObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                
                newObj.layer = LayerMask.NameToLayer("Main");
                newObj.tag = "Cubes";

                var scaleTest = newObj.AddComponent<SoundCaptureScaleTest>();
                scaleTest.Target = SoundCapture.DataSource.SingleBand;
                scaleTest.BandIndex = i;
                scaleTest.transform.parent = transform;
                scaleTest.transform.localPosition = new Vector3(scaleTest.MinScale * i, 0f, 0f);

                
            }

            var bgObj = GameObject.CreatePrimitive(PrimitiveType.Cube);

            bgObj.layer = LayerMask.NameToLayer("Main");
            bgObj.tag = "BG";

            var bgTest = bgObj.AddComponent<SoundCaptureScaleTest>();
            bgTest.Target = SoundCapture.DataSource.SingleBand;
            bgTest.BandIndex = 0;
            bgTest.transform.parent = transform;
            bgTest.transform.position = new Vector3(bgTest.MinScale, 1f, 1f);

            var dwnObj = GameObject.CreatePrimitive(PrimitiveType.Cube);

            dwnObj.layer = LayerMask.NameToLayer("Main");
            dwnObj.tag = "DWN";

            var dwnTest = dwnObj.AddComponent<SoundCaptureScaleTest>();
            dwnTest.Target = SoundCapture.DataSource.SingleBand;
            dwnTest.BandIndex = 0;
            dwnTest.transform.parent = transform;
            dwnTest.transform.position = new Vector3(bgTest.MinScale, -1f, 1f);
        }
    }
}