namespace Newmark_Technical_Assessment.Entity
{
    public class Space
    {
        public string? SpaceId { get; set; }
        public string? SpaceName { get; set; }
        public List<RentDetails>? RentRoll { get; set; }
    }
}
