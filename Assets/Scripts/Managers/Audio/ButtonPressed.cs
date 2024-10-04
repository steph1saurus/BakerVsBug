
using UnityEngine;

public class ButtonPressed : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip clickSound;

    [SerializeField] GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        clickSound = gameManager.clickSound;
    }

    public void OnButtonPressed()
    {
        audioSource.PlayOneShot(clickSound);
    }
}
