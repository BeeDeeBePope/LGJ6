#if UNITY_EDITOR
using RichUnity.Audio;
#endif
using Unity.Collections;
using UnityEngine;
using UnityEngine.Audio;

namespace RichUnity.Audio
{
    [CreateAssetMenu(fileName = "AudioMixerVolumeController", menuName = "RichUnity/Audio/Audio Mixer Volume Controller")]
    public class AudioMixerVolumeController : ScriptableObject
    {
        [SerializeField]
        private AudioMixer audioMixer;

        public AudioMixer AudioMixer
        {
            get
            {
                return audioMixer;
            }
        }

        public string VolumeParameterName;

        private const float LowerVolumeBound = -80.0f;
        private const float UpperVolumeBound = 0.0f;

        public float Volume // do not use it in Awake() or OnEnable(), Unity has a bug with AudioMixer
        {
            get
            {
                if (muted)
                {
                    return volumeBeforeMute;
                }
                else
                {
                    return VolumeNoMutedGuards;
                }
            }
            set
            {
                float newVolumeT = Mathf.Clamp01(value);
                if (muted)
                {
                    volumeBeforeMute = newVolumeT;
                }
                else
                {
                    VolumeNoMutedGuards = newVolumeT;
                }
            }
        }

        private float VolumeNoMutedGuards
        {
            get
            {
                float volume = 0.0f;
                if (AudioMixer)
                {
                    AudioMixer.GetFloat(VolumeParameterName, out volume);
                }

                volume = Mathf.InverseLerp(LowerVolumeBound, UpperVolumeBound, volume);
                return volume;
            }

            set
            {
                if (AudioMixer)
                {
                    AudioMixer.SetFloat(VolumeParameterName,
                        Mathf.Lerp(LowerVolumeBound, UpperVolumeBound, value));
                }
            }
        }

#if UNITY_EDITOR
        [ReadOnly]
#endif
        [SerializeField]
        private bool muted;
        private float volumeBeforeMute;

        public bool Muted // do not use it in Awake() or OnEnable(), Unity has a bug with AudioMixer
        {
            get
            {
                return muted;
            }
            set
            {
                muted = value;
                if (muted)
                {
                    volumeBeforeMute = VolumeNoMutedGuards;
                    VolumeNoMutedGuards = 0.0f;
                }
                else
                {
                    VolumeNoMutedGuards = volumeBeforeMute;
                }
            }
        }

        private void OnEnable()
        {
            muted = false;
        }
    }
}