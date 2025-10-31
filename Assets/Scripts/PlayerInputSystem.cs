using UnityEngine;

public class PlayerInputSystem : MonoBehaviour
{
    public bool JumpPressed { get; private set; }
 
    void Update()
    {
        JumpPressed = Input.GetKeyDown(KeyCode.Space);
        
    }

    public void ResetInputs()
    {
        JumpPressed = false;
       
    }
}
