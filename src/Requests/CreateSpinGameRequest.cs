using System.Diagnostics.CodeAnalysis;
using SpinGameApp.Model;

namespace SpinGameApp.Requests
{
    public class CreateSpinGameRequest
    {
        public required string Name { get; set; }
        public required List<Model.Prize> Prizes { get; set; }

        public static SpinGame To([NotNull]CreateSpinGameRequest req)
        {
            return new SpinGame
            {
                Name = req.Name,
                Prizes = req.Prizes
            };
        }
    }
}