/// <summary>
/// Place the labels for the States in this enum.
/// Don't change the first label, NullTransition as FSMSystem class uses it.
/// </summary>
public enum StateID
{
    NullStateID = 0, // Use this ID to represent a non-existing State in your system	
    Attacking = 1,
    Jumping = 2,
    Dead = 3,
    Idle = 4,
}
