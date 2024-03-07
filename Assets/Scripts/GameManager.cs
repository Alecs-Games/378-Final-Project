using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject poofab;
    public Player player;
    public GameObject playerPrefab;
    public Vector3 playerResetPosition;
    public bool useTempPosition;
    public Vector2 tempPositionOnLoad;
    public Hearts hearts;
    public List<GameObject> pets = new List<GameObject>();
    public bool paused;
    public bool lizardRescued;
    public bool catRescued;
    public bool dogRescued;
    public AudioSource audi;

    // Start is called before the first frame update
    void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            //player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            player = Instantiate(playerPrefab, playerResetPosition, Quaternion.identity)
                .GetComponent<Player>();
            DontDestroyOnLoad(player.gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
            FindHearts();
            audi = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadEntrance(Entrance entrance)
    {
        LoadScene(entrance.sceneName, entrance.map, entrance.startLocation);
    }

    public void LoadScene(string name, bool map, Vector2 startLocation)
    {
        StartCoroutine(LoadWithAnimation(name, map, startLocation));
    }

    public IEnumerator LoadWithAnimation(string name, bool map, Vector2 startLocation)
    {
        SpriteRenderer black = GameObject
            .FindGameObjectWithTag("Black")
            .GetComponent<SpriteRenderer>();
        black.enabled = true;
        Pause(true);
        while (black.color.a < 1f)
        {
            black.color = new Color(
                black.color.r,
                black.color.g,
                black.color.b,
                black.color.a + 0.05f
            );
            yield return new WaitForSeconds(0.05f);
        }
        SceneManager.LoadScene(name);
        if (map)
        {
            player.EnterMapMode();
        }
        else
        {
            player.EnterRegularMode();
        }
        useTempPosition = true;
        tempPositionOnLoad = startLocation;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindHearts();
        if (useTempPosition)
        {
            player.transform.position = tempPositionOnLoad;
            useTempPosition = false;
        }
        foreach (GameObject p in pets)
        {
            if (p != null)
            {
                p.transform.position = player.transform.position + new Vector3(0, -1f, 0);
                p.GetComponent<Pet>().OnSceneLoaded();
            }
        }
        StartCoroutine(FadeIn(scene, mode));
    }

    public IEnumerator FadeIn(Scene scene, LoadSceneMode mode)
    {
        SpriteRenderer black = GameObject
            .FindGameObjectWithTag("Black")
            .GetComponent<SpriteRenderer>();
        black.enabled = true;
        black.color = new Color(black.color.r, black.color.g, black.color.b, 1);
        while (black.color.a > 0f)
        {
            black.color = new Color(
                black.color.r,
                black.color.g,
                black.color.b,
                black.color.a - 0.05f
            );
            yield return new WaitForSeconds(0.05f);
        }
        black.enabled = false;
        Pause(false);
        if (lizardRescued && dogRescued && catRescued)
        {
            LoadScene("end", false, Vector2.zero);
        }
    }

    void Pause(bool pause)
    {
        this.paused = pause;
    }

    void FindHearts()
    {
        hearts = GameObject.FindGameObjectWithTag("Hearts").GetComponent<Hearts>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Screen.fullScreen = !Screen.fullScreen;
        }
    }

    public void OnPlayerDeath()
    {
        StartCoroutine(ResetGame());
    }

    IEnumerator ResetGame()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Map");
        player = Instantiate(playerPrefab, playerResetPosition, Quaternion.identity)
            .GetComponent<Player>();
        DontDestroyOnLoad(player.gameObject);
    }

    public void AddPet(GameObject pet)
    {
        audi.Play();
        DontDestroyOnLoad(pet);
        pets.Add(pet);
    }
}
