using UnityEngine;
[RequireComponent(typeof(characterMotor))]
public class pacmaninput : MonoBehaviour
{
    private characterMotor _motor;
    private void Start()
    {
        _motor = GetComponent<characterMotor>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _motor.SetMoveDirection(Direction.Up);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _motor.SetMoveDirection(Direction.Left);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _motor.SetMoveDirection(Direction.Down);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _motor.SetMoveDirection(Direction.Right);
        }
    }

    /* void Update()
     {

     }
    */
}
