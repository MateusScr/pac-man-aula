using UnityEngine;

public class pacmanview : MonoBehaviour
{
    public characterMotor characterMotor;

    public Animator Animator;

    private void Start()
    {
        characterMotor.OnDirectionChanged += CharacterMotor_OnDirectionChanged;
    }

    private void CharacterMotor_OnDirectionChanged(Direction direction)
    {
        Animator.SetBool("moving", true);

        switch (direction)
        {

            case Direction.None:
                Animator.SetBool("moving", false);
                break;

            case Direction.Up:
                Animator.SetBool("moving", true);
                transform.rotation = Quaternion.Euler(0, 0, 90);
                break;

            case Direction.Left:
                Animator.SetBool("moving", true);
                transform.rotation = Quaternion.Euler(0, 0, 180);
                break;

            case Direction.Down:
                Animator.SetBool("moving", true);
                transform.rotation = Quaternion.Euler(0, 0, 270);
                break;

            case Direction.Right:
                Animator.SetBool("moving", true);
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
        }

    }
}