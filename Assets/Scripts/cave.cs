using UnityEngine;
using UnityEngine.InputSystem;

public class Cave : MonoBehaviour
{
    bool playerInside;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        Debug.Log("пещера смотрит с презрением");
        Debug.Log(player);
        playerInside = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        Debug.Log("пещера проводила вас взглядом");
        playerInside = false;
    }

}
