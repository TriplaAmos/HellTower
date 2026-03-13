using UnityEngine;

public class MovingBomb : MonoBehaviour
{
    [Header("Impostazioni Movimento")]
    public float distance = 3.0f;
    public float fastSpeed = 4.0f;
    public float slowSpeed = 0.5f;
    
    // Stato del movimento: 0 = Fermo, 1 = Lento, 2 = Veloce
    private int movementState = 0; 
    private Vector3 startPosition;
    private float currentPhase = 0f;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Se lo stato è 0, la bomba non si muove affatto
        if (movementState == 0) return;

        // Scegliamo la velocità in base allo stato
        float targetSpeed = (movementState == 2) ? fastSpeed : slowSpeed;

        currentPhase += Time.deltaTime * targetSpeed;
        float movement = Mathf.Sin(currentPhase) * distance;
        transform.position = startPosition + new Vector3(movement, 0, 0);
    }

    // Metodo chiamato dal Buco Nero per cambiare stato
    public void SetState(int state)
    {
        movementState = state;
    }
}