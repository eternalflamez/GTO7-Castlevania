using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TyperText : MonoBehaviour {
    [SerializeField]
    private float typeDelay;
    [SerializeField]
    private AudioSource audioSource;
    private Text text;
    private int currentPointer;
    private float currentTime;
    private string textValue;
    private float originalPitch;
    private const float pitchDiff = .2f;

    [SerializeField]
    private float typeSoundDelay;
    private float currentSoundTimer;

	// Use this for initialization
	void Start () {
        this.text = GetComponent<Text>();
        currentPointer = 0;
        currentTime = 0;
        textValue = text.text;
        originalPitch = audioSource.pitch;
        currentSoundTimer = 0;

        this.text.text = "";
	}
	
	// Update is called once per frame
	void Update () {
        if(currentPointer > textValue.Length)
        {
            return;
        }

        currentTime += Time.deltaTime;
        currentSoundTimer += Time.deltaTime;

        if(currentTime >= typeDelay)
        {
            
            text.text = textValue.Substring(0, currentPointer);
            currentPointer++;
            currentTime = 0;
        }

        if (currentSoundTimer > typeSoundDelay)
        {
            float random = Random.Range(-pitchDiff, pitchDiff);
            audioSource.pitch = originalPitch * (1 + random);
            currentSoundTimer = 0;

            audioSource.PlayOneShot(audioSource.clip);
        }
	}
}
