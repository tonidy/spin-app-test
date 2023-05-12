using SpinGameApp.Model;

namespace SpinGameApp.Repositories
{
    public interface ISpinResultRepository
    {
        /// <summary>
        /// Creates a new spin result in the database
        /// </summary>
        /// <param name="spinResult">instance of spin game result</param>
        /// <returns>spin game identifier</returns>
        string Create(SpinResult spinResult);
        /// <summary>
        /// Retrieves all spin results for a given player
        /// </summary>
        /// <param name="playerId">player identifier</param>
        /// <returns>List of SpinResult instance</returns>
        IReadOnlyList<SpinResult> GetByPlayerId(string playerId);
        /// <summary>
        /// Retrieves all spin results for a given spin game
        /// </summary>
        /// <param name="spinGameId">spin game identifier</param>
        /// <returns>List of SpinResult instance</returns>
        IReadOnlyList<SpinResult> GetBySpinGameId(string spinGameId);
    }
}