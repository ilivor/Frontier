using Robust.Shared.GameStates; // Forge edit

namespace Content.Shared.Eye.Blinding.Components;
/// <summary>
/// For welding masks, sunglasses, etc.
/// </summary>
[RegisterComponent, NetworkedComponent, AutoGenerateComponentState] // Forge edit
public sealed partial class EyeProtectionComponent : Component
{
    /// <summary>
    /// How many seconds to subtract from the status effect. If it's greater than the source
    /// of blindness, do not blind.
    /// </summary>
    [DataField("protectionTime")]
    [AutoNetworkedField] // Forge edit
    public TimeSpan ProtectionTime = TimeSpan.FromSeconds(10);
}
