using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayAudioAtPitch : MonoBehaviour
{
  [SerializeField]
  bool _playOnEnable = true;

  [SerializeField]
  float _minPitch = .9f;

  [SerializeField]
  float _maxPitch = 1.1f;

  AudioSource _audioSource;
  AudioSource AudioSource
  {
    get
    {
      if (_audioSource != null) return _audioSource;
      return _audioSource = GetComponent<AudioSource>();
    }
  }

  void OnEnable()
  {
    _audioSource = GetComponent<AudioSource>();
    if (_audioSource == null) return;

    if (_playOnEnable) PlayAtRandomPitch();
  }

  public void PlayAtSpecificPitch(float specifiedPitch)
  {
    if (AudioSource == null) return;
    if (_audioSource.clip == null) return;

    // First clamp the input between our min and max settings.
    specifiedPitch = Mathf.Clamp(specifiedPitch, _minPitch, _maxPitch);

    _audioSource.pitch = specifiedPitch;
    _audioSource.Play();
  }

  public void PlayAtRandomPitch()
  {
    if (AudioSource == null) return;
    if (_audioSource.clip == null) return;

    _audioSource.pitch = Random.Range(_minPitch, _maxPitch);
    _audioSource.Play();
  }
}