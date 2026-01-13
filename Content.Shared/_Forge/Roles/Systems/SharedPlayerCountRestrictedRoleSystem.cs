using Robust.Shared.GameStates;
using Robust.Shared.Serialization;
using System;

namespace Content.Shared._Forge.Roles
{
    public abstract class SharedPlayerCountRestrictedRoleSystem : EntitySystem
    {
        // Базовая логика, если нужна общая для клиента и сервера
        public override void Initialize()
        {
            base.Initialize();

            // Можно добавить сетевую синхронизацию здесь
            SubscribeLocalEvent<PlayerCountRestrictedRoleComponent, ComponentGetState>(OnGetState);
            SubscribeLocalEvent<PlayerCountRestrictedRoleComponent, ComponentHandleState>(OnHandleState);
        }

        private void OnGetState(EntityUid uid, PlayerCountRestrictedRoleComponent component, ref ComponentGetState args)
        {
            args.State = new PlayerCountRestrictedRoleComponentState(
                component.RoleId,
                component.MinPlayers,
                component.SlotsWhenAvailable,
                component.CurrentSlots
            );
        }

        private void OnHandleState(EntityUid uid, PlayerCountRestrictedRoleComponent component, ref ComponentHandleState args)
        {
            if (args.Current is not PlayerCountRestrictedRoleComponentState state)
                return;

            component.RoleId = state.RoleId;
            component.MinPlayers = state.MinPlayers;
            component.SlotsWhenAvailable = state.SlotsWhenAvailable;
            component.CurrentSlots = state.CurrentSlots;
        }

        [Serializable, NetSerializable]
        private sealed class PlayerCountRestrictedRoleComponentState : ComponentState
        {
            public string RoleId { get; }
            public int MinPlayers { get; }
            public int SlotsWhenAvailable { get; }
            public int CurrentSlots { get; }

            public PlayerCountRestrictedRoleComponentState(
                string roleId,
                int minPlayers,
                int slotsWhenAvailable,
                int currentSlots)
            {
                RoleId = roleId;
                MinPlayers = minPlayers;
                SlotsWhenAvailable = slotsWhenAvailable;
                CurrentSlots = currentSlots;
            }
        }
    }
}
