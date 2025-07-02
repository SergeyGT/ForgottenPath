using UnityEngine;

public class SFXManager : MonoBehaviour
{
   public static SFXManager Instance;

   [SerializeField] private AudioSource sfx;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("SFX Manager is already exists");
        }
    }

    public void PlaySound(AudioClip clip, Transform obj, float volume)
    {
        AudioSource audioSource = Instantiate(sfx, obj.position, Quaternion.identity);

        audioSource.clip = clip;

        audioSource.volume = volume;

        audioSource.loop = false;

        float clipLength = audioSource.clip.length;

        audioSource.Play();

        Destroy(audioSource.gameObject, clipLength);
    }
}
