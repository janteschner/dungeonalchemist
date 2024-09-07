using UnityEngine;
using UnityEngine.EventSystems;

public class ComeToFront : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.SetAsLastSibling();
        transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = new Vector3(1f, 1f, 1f);

    }
}