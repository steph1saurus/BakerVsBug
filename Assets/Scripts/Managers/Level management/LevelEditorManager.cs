using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class LevelEditorManager : MonoBehaviour
{
    [Header ("Player items")]
    public ItemController[] itemButtons; // Array of item buttons
    public GameObject[] itemPrefabs; // Array of item prefabs
    public GameObject[] itemImage; //Array of item images

    public int currentButtonPressed; // Reference buttonID

    [Header("Level progress")]
    public Slider progressBar;
    public bool levelCompleteBool = false;
    public GameObject gameOverScreen;

    [Header("Audio")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip clickSound;
    [SerializeField] AudioClip gameOverSound;
    [SerializeField] AudioClip levelCompleteSound;

    [Header("GameManager")]
    [SerializeField] GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        clickSound = gameManager.clickSound;
        gameOverSound = gameManager.gameOverSound;
        levelCompleteSound = gameManager.levelCompleteSound;

    }

    private void Update()
    {
        Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worlPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        if (Input.GetMouseButtonDown(0) && itemButtons[currentButtonPressed].clicked)
        {
            itemButtons[currentButtonPressed].clicked = false;
            Instantiate(itemPrefabs[currentButtonPressed], new Vector3(worlPosition.x, worlPosition.y, 0), Quaternion.identity);
            Destroy(GameObject.FindGameObjectWithTag("ItemImage"));
        }

        if (!gameManager.isPaused)
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
       
        gameManager.Restart();
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        levelCompleteBool = true;
        audioSource.PlayOneShot(gameOverSound);
        gameManager.TimePaused();
        StartCoroutine(HandleLevelCompletion());
    }

    public void LevelComplete()
    {
        if (progressBar.value == 1 && !levelCompleteBool)
        {
            levelCompleteBool = true;

            audioSource.PlayOneShot(levelCompleteSound);
            gameManager.TimePaused();

            StartCoroutine(HandleLevelCompletion());

        }
    }

    private IEnumerator HandleLevelCompletion()
    {

        // Wait for 3 seconds
        yield return new WaitForSecondsRealtime(3f);  // Realtime because Time.timeScale is set to 0

        gameManager.LoadScene("RewardScene");
    }

   
}


