using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class DragObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public bool dragOnSurfaces = true;
    public FilpOneCard filpOneCard;

    private GameObject m_DraggingIcon;
    private RectTransform m_DraggingPlane;
    private Vector2 cardSize = new Vector2();
    private Vector2 moveRange = new Vector2();

    public void OnBeginDrag(PointerEventData eventData)
    {
        
        cardSize = gameObject.GetComponent<RectTransform>().sizeDelta;
        moveRange = new Vector2(cardSize.y/2f, cardSize.x/2f);
        print(moveRange);
        var canvas = FindInParents<Canvas>(gameObject);
        if (canvas == null)
            return;

        // We have clicked something that can be dragged.
        // What we want to do is create an icon for this.
        m_DraggingIcon = new GameObject("icon");

        m_DraggingIcon.transform.SetParent(canvas.transform, false);
        m_DraggingIcon.transform.SetAsLastSibling();

        var image = m_DraggingIcon.AddComponent<Image>();

        image.sprite = GetComponent<Image>().sprite;
        image.GetComponent<RectTransform>().sizeDelta = cardSize;

        gameObject.GetComponent<Image>().enabled = false;
        //image.SetNativeSize();

        if (dragOnSurfaces)
            m_DraggingPlane = transform as RectTransform;
        else
            m_DraggingPlane = canvas.transform as RectTransform;

        SetDraggedPosition(eventData);
    }

    public void OnDrag(PointerEventData data)
    {
        if (m_DraggingIcon != null)
            SetDraggedPosition(data);
    }

    private void SetDraggedPosition(PointerEventData data)
    {
        if (dragOnSurfaces && data.pointerEnter != null && data.pointerEnter.transform as RectTransform != null)
            m_DraggingPlane = data.pointerEnter.transform as RectTransform;

        var rt = m_DraggingIcon.GetComponent<RectTransform>();
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_DraggingPlane, data.position, data.pressEventCamera, out globalMousePos))
        {
            rt.position = globalMousePos;
            rt.rotation = m_DraggingPlane.rotation;
        }
        if(Mathf.Abs(WorldToCanvas(globalMousePos).x) > moveRange.x || Mathf.Abs(WorldToCanvas(globalMousePos).y) > moveRange.y){
            Destroy(m_DraggingIcon);
            ShowCard();
        }
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (m_DraggingIcon != null){
            Destroy(m_DraggingIcon);
            ShowCard();
        }
            
    }

    static public T FindInParents<T>(GameObject go) where T : Component
    {
        if (go == null) return null;
        var comp = go.GetComponent<T>();

        if (comp != null)
            return comp;

        Transform t = go.transform.parent;
        while (t != null && comp == null)
        {
            comp = t.gameObject.GetComponent<T>();
            t = t.parent;
        }
        return comp;
    }

    public Vector2 WorldToCanvas(Vector3 world_position, Camera camera = null)
    {
         if (camera == null)
         {
             camera = Camera.main;
         }
 
         var viewport_position = camera.WorldToViewportPoint(world_position);
         var canvas_rect = FindInParents<Canvas>(gameObject).GetComponent<RectTransform>();
 
         return new Vector2((viewport_position.x * canvas_rect.sizeDelta.x) - (canvas_rect.sizeDelta.x * 0.5f), (viewport_position.y * canvas_rect.sizeDelta.y) - (canvas_rect.sizeDelta.y * 0.5f));
     }

     private void ShowCard(){
        filpOneCard.Show();
        print("showCard");
     }
}
