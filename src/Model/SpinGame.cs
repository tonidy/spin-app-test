using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SpinGameApp.Model
{
    public class SpinGame
    {
        public SpinGame()
        {
            Prizes = new List<Prize>();
        }
        /// <summary>
        /// The unique identifier for the spin game
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        /// <summary>
        /// The name of the spin game
        /// </summary>
        public required string Name { get; set; }
        /// <summary>
        /// a list of prizes that can be won in the spin game. Each
        /// prize should include a name and a probability (e.g., "common", "rare", "epic")
        /// </summary>
        public List<Prize> Prizes { get; set; }
    }
}