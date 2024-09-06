using DG.Tweening;
using UnityEngine;

public class EnemyInfo_Tween: MonoBehaviour
{
    [SerializeField]
    private RectTransform enemyInfoButton;

    [SerializeField] TweenScriptableObject enemyInfoButton_SO;

    [SerializeField]
    private bool isSelected;

    private float startingYValue;


    // Start is called before the first frame update
    void Start()
    {
        isSelected = false;
        startingYValue = enemyInfoButton.anchoredPosition.y;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //public so we can access via Button
    public void ButtonSelection()
    {
        if (isSelected)
        {
            isSelected = false;
            enemyInfoButton_DeSelectedAnimation();
        }

        else
        {
            isSelected = true;
            EnemyInfoButton_SelectedAnimation();
        }
    }


    private void EnemyInfoButton_SelectedAnimation()
    {


        //Play pop-up animation
        var endValueSelected = enemyInfoButton.rect.height * 0.5f;
        enemyInfoButton.DOAnchorPosY(endValueSelected, enemyInfoButton_SO.TweenDuration, enemyInfoButton_SO.TweenSnapping);

    }

    private void enemyInfoButton_DeSelectedAnimation()
    {
        //Play pop-down animation
        enemyInfoButton.DOAnchorPosY(startingYValue, enemyInfoButton_SO.TweenDuration, enemyInfoButton_SO.TweenSnapping);

    }

}
