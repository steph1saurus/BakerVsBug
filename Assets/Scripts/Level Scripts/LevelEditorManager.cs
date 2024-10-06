using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class LevelEditorManager : MonoBehaviour
{
    [Header ("Player items")]
    [SerializeField] public ItemController[] itemButtons; // Array of item buttons
    [SerializeField] public GameObject[] itemPrefabs; // Array of item prefabs
    [SerializeField] public GameObject[] itemImage; //Array of item images

    [SerializeField] public int currentButtonPressed; // Reference buttonID

    [Header("Level progress")]
    [SerializeField] public Slider progressBar;
    [SerializeField] public bool levelCompleteBool = false;
    [SerializeField] public GameObject gameOverScreen;
    [SerializeField] public GameObject completeScreen;

    [Header("Audio")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip clickSound;
    [SerializeField] AudioClip gameOverSound;
    [SerializeField] AudioClip levelCompleteSound;
    [SerializeField] AudioSource musicAudioSource;

    [Header("Managers")]
    [SerializeField] SoundMixerManager soundMixerManager;
    [SerializeField] BakedGoodPayoutManager bakedGoodPayoutManager;

    private void Start()
    {
        soundMixerManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SoundMixerManager>();
        bakedGoodPayoutManager = GameObject.FindGameObjectWithTag("BakedGoodsManager").GetComponent<BakedGoodPayoutManager>();
        clickSound = soundMixerManager.clickSound;
        gameOverSound = soundMixerManager.gameOverSound;
        levelCompleteSound = soundMixerManager.levelCompleteSound;
        soundMixerManager.isPaused = false;

    }

    private void Update()
    {
        //move based on mouse position
        Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worlPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        if (Input.GetMouseButtonDown(0) && itemButtons[currentButtonPressed].clicked)
        {
            itemButtons[currentButtonPressed].clicked = false;
            Instantiate(itemPrefabs[currentButtonPressed], new Vector3(worlPosition.x, worlPosition.y, 0), Quaternion.identity);
            Destroy(GameObject.FindGameObjectWithTag("ItemImage"));
        }

        if (!soundMixerManager.isPaused)
        {
            Time.timeScale = 1f;
            LevelComplete();
            GameObject[] bakedGoods = GameObject.FindGameObjectsWithTag("BakedGood");
            if (bakedGoods.Length ==0)
            {
                GameOver();
            }
        }

    }

    public void RestartButtonPressed()
    {
        audioSource.PlayOneShot(clickSound);

        soundMixerManager.Restart();
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        levelCompleteBool = true;
        audioSource.PlayOneShot(gameOverSound);
        soundMixerManager.TimePaused();
        StartCoroutine(HandleLevelCompletion());
    }

    public void LevelComplete()
    {
 
        if (progressBar.value == 1 && !levelCompleteBool)
        {
            levelCompleteBool = true;
            completeScreen.SetActive(true);
            audioSource.PlayOneShot(levelCompleteSound);
            soundMixerManager.TimePaused();
            bakedGoodPayoutManager.CompleteLevelWithPayout();

            StartCoroutine(HandleLevelCompletion());
        }
    }


    private IEnumerator HandleLevelCompletion()
    {
       

        // Wait for 3 seconds
        yield return new WaitForSecondsRealtime(3f);  // Realtime because Time.timeScale is set to 0 during pause and level complete

        soundMixerManager.LoadScene("RewardScene");
    }


}


