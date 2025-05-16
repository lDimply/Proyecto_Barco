using UnityEngine;

public class BotePlayer : MonoBehaviour
{
    Vector2 inputVector = new Vector2(0, 0);

    private void Update()
    {
        Vector2 inputVector = new Vector2(0, 0);

        if(Input.GetKey(KeyCode.W))
        {
            inputVector.y = +1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y = -1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x = -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.y = +1f;
        }

        inputVector = inputVector.normalized;

        Vector3 moveDir = new Vector3 (inputVector.x,0f, inputVector.y);
        transform.position += moveDir;
    }
}
