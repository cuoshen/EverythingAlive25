using UnityEngine;

public class FruitGameMaster : MonoBehaviour
{
    [SerializeField]
    private AudioSource ambientAudio;

    private void OnEnable()
    {
        ambientAudio?.Play();
    }

    private void Update()
    {
        
    }
}
