namespace Newmark_Technical_Assessment.Entity
{
    public class PropertyDetails
    {
        public string? PropertyId { get; set; }
        public string? PropertyName { get; set; }
        public List<string>? Features { get; set; }
        public List<string>? Highlights { get; set; }
        public List<TransportMode>? Transportation { get; set; }
        public List<Space>? Spaces { get; set; }

    }
}
