using SpinGameApp.Model;

namespace SpinGameApp.Repositories
{
    public interface ISpinGameRepository
    {
       /// <summary>
        /// Creates a new spin game in the database
        /// </summary>
        /// <param name="spinGame">instance of spin game</param>
        /// <returns>spin game identifier</returns>
        string Create(SpinGame spinGame);
         /// <summary>
        /// Retrieves a spin games
        /// </summary>
        /// <param name="page">index of pagination (1-based indexing)</param>
        /// <param name="pageSize">the number of rows of pagination</param>
        /// <returns>List of SpinGame instance</returns>
        IReadOnlyList<SpinGame> GetAll(int page, int pageSize);
        /// <summary>
        /// Retrieves a spin game by its ID
        /// </summary>
        /// <param name="id">spin game identifier</param>
        /// <returns>SpinGame instance</returns>
        SpinGame Get(string id);
        /// <summary>
        /// Retrieves a spin game by its name
        /// </summary>
        /// <param name="name">spin game name</param>
        /// <returns>SpinGame instance</returns>
        SpinGame GetByName(string name);
        /// <summary>
        /// Updates an existing spin game in the database
        /// </summary>
        /// <param name="spinGame">SpinGame instance</param>
        void Update(SpinGame spinGame);
        /// <summary>
        /// Deletes a spin game by its ID
        /// </summary>
        /// <param name="id">spin game identifier</param>
        void Delete(string id);
    }
}