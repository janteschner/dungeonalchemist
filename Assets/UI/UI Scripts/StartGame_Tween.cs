using DG.Tweening;
using UnityEngine;

public class StartGame_Tween: MonoBehaviour
{
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


    // Start is called before the first frame update
    void Start()
    {
        screenHeightWidth.x = Screen.width;
        screenHeightWidth.y = Screen.height;

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
}
