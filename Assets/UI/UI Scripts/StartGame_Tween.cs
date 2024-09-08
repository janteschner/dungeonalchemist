using DG.Tweening;
using UnityEngine;

public class StartGame_Tween: MonoBehaviour
{
    
    public static StartGame_Tween Instance { get; private set; }
    
    //[SerializeField]
    private GameObject startButton;

    [SerializeField]
    private RectTransform mainMenuDoor_0, mainMenuDoor_1;

    [SerializeField]
    private TweenScriptableObject mainMenuDoorSO;

    [SerializeField]
    private bool goUpDown;

    [SerializeField]
    private Vector2 screenHeightWidth;

    [SerializeField] private GameObject eyes;
    
    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        screenHeightWidth.x = 2000;
        screenHeightWidth.y = 1500;
        
        eyes.SetActive(false);

        startButton = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DoorTween_Selection()
    {
        if (goUpDown)
        {
            TweenAnim_UpDown();
        }

        else
        {
            TweenAnim_LeftRight();
        }
    }

    private void TweenAnim_UpDown()
    {
        //Door 0 will go up
        mainMenuDoor_0.DOAnchorPosY(screenHeightWidth.y, mainMenuDoorSO.TweenDuration, mainMenuDoorSO.TweenSnapping);

        //Door 1 will go down
        mainMenuDoor_1.DOAnchorPosY(-screenHeightWidth.y, mainMenuDoorSO.TweenDuration, mainMenuDoorSO.TweenSnapping);

        startButton.SetActive(false);
    }

    private void TweenAnim_LeftRight()
    {
        //Door 0 will go left
        mainMenuDoor_0.DOAnchorPosX(-screenHeightWidth.x, mainMenuDoorSO.TweenDuration, mainMenuDoorSO.TweenSnapping);

        //Door 1 will go right
        mainMenuDoor_1.DOAnchorPosX(screenHeightWidth.x, mainMenuDoorSO.TweenDuration, mainMenuDoorSO.TweenSnapping);

        startButton.SetActive(false);
    }

    public void CloseMouth(bool playerDied)
    {
        mainMenuDoor_0.DOAnchorPosY(0, mainMenuDoorSO.TweenDuration, mainMenuDoorSO.TweenSnapping);
        mainMenuDoor_1.DOAnchorPosY(0, mainMenuDoorSO.TweenDuration, mainMenuDoorSO.TweenSnapping);
        if (!playerDied)
        {
            eyes.SetActive(true);
        }
    }
}
