using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SpinGameApp.Model
{
    public class SpinResult
    {
        /// <summary>
        /// The unique identifier for the spin result
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        /// <summary>
        /// The ID of the player who spun the wheel
        /// </summary>
        public required string PlayerId { get; set; }
        /// <summary>
        /// The ID of the spin game
        /// </summary>
        public required string SpinGameId { get; set; }
        /// <summary>
        /// The name of the prize won
        /// </summary>
        public required string PrizeName { get; set; }
         /// <summary>
        /// The result of the spinning
        /// </summary>
        public int PrizeResult { get; set; }
        /// <summary>
        /// The date and time the spin occurred (in UTC)
        /// </summary>
        public DateTime Timestamp { get; set; }
    }
}