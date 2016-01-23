/// <summary>
/// Place the labels for the Transitions in this enum.
/// Don't change the first label, NullTransition as FSMSystem class uses it.
/// </summary>
public enum Transition
{
    NullTransition = 0, // Use this transition to represent a non-existing transition in your system
    TakingBreak = 1,
    StartedSearch = 2,
    DetectedPlayer = 3,
    LostPlayer = 5,
    Dying = 6,
    Jumping = 7,
    StopJump = 8,
}
