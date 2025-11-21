using System.Collections;

/// <summary>
/// An action to be performed during step simulation.
/// When performed, this will change the state of the game somehow,
/// usually with a visual indicator or animation.
/// </summary>
public interface ITurnAction
{
    /// <summary>
    /// Called when the turn action begins playing.
    /// </summary>
    void Start(TurnManager turnManager);

    /// <summary>
    /// Coroutine that implements the behavior of the turn action.
    /// </summary>
    /// <returns></returns>
    IEnumerator Run(TurnManager turnManager);

    /// <summary>
    /// Called as the turn action finishes playing.
    /// </summary>
    void End(TurnManager turnManager);
}