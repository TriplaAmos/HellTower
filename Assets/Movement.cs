using UnityEngine;

public class HoleController : MonoBehaviour
{
    [Header("Movimento Buco")]
    public float followSpeed = 10f;
    private float fixedY; 

    [Header("Impostazioni Crescita")]
    public float growthAmount = 0.2f;
    public float maxScale = 5.0f;
    
    [Header("Tag Riconoscimento")]
    public string coinTag = "Coin";
    public string bombTag = "Bomb";

    private Vector3 mOffset;
    private float mZCoord;
    private bool hasStartedMoving = false;

    void Start()
    {
        fixedY = transform.position.y;
    }

    void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();
        
        // Al primo click, le bombe passano da Ferme (0) a Veloci (2)
        hasStartedMoving = true;
        SetBombsState(2); 
    }

    void OnMouseUp()
    {
        // Quando rilasciamo, le bombe diventano Lente (1)
        SetBombsState(1);
    }

    void OnMouseDrag()
    {
        Vector3 rawTarget = GetMouseWorldPos() + mOffset;
        Vector3 targetPosition = new Vector3(rawTarget.x, fixedY, rawTarget.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * followSpeed);
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void SetBombsState(int state)
    {
        MovingBomb[] bombs = FindObjectsByType<MovingBomb>(FindObjectsSortMode.None);
        foreach (MovingBomb b in bombs)
        {
            b.SetState(state);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(coinTag))
        {
            Destroy(other.gameObject);
            if (transform.localScale.x < maxScale)
                transform.localScale += Vector3.one * growthAmount;
        }
        
        if (other.CompareTag(bombTag))
        {
            Destroy(gameObject);
        }
    }
}