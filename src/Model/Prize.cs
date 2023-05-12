namespace SpinGameApp.Model
{
    public class Prize
    {
        /// <summary>
        /// The name of the prize
        /// </summary>
        public required string Name { get; set; }
        /// <summary>
        /// The probability of winning the prize, represented as a percentage
        /// </summary>
        public int Probability { get; set; }
    }
}