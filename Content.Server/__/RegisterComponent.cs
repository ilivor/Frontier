[RegisterComponent]
public sealed partial class DoorCodePaperComponent : Component
{
    [DataField("doorCode")]
    public string? DoorCode; // Код для двери
    
    [DataField("doorUid")] 
    public EntityUid? DoorUid; // UID двери
}