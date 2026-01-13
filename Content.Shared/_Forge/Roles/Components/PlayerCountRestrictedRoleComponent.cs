using Robust.Shared.GameStates;
using Robust.Shared.Serialization;

namespace Content.Shared._Forge.Roles
{
    [RegisterComponent, NetworkedComponent]
    public sealed partial class PlayerCountRestrictedRoleComponent : Component
    {
        [DataField(required: true)]
        public string RoleId = string.Empty;

        [DataField]
        public int MinPlayers = 30;

        [DataField]
        public int SlotsWhenAvailable = 1;

        [ViewVariables]
        public int CurrentSlots = 0;
    }
}
