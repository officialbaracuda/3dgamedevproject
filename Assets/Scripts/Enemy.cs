using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float speed;

    AudioSource audioSource;

    public AudioClip[] sounds;

    NavMeshAgent navMeshAgent;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        navMeshAgent.speed = speed;
        SetZombieSound();
    }

    public void SetZombieSound() {
        int index = Random.Range(0, sounds.Length);
        audioSource.clip = sounds[index];
    }

    public void PlaySound() {
        if (!audioSource.isPlaying) {
            audioSource.Play();
        }
    }

    public void StopSound()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

}
