using Content.Shared._Forge.Roles;
using Robust.Server.Player;
using Robust.Shared.Player;

namespace Content.Server._Forge.Roles
{
    public sealed class PlayerCountRestrictedRoleSystem : SharedPlayerCountRestrictedRoleSystem
    {
        [Dependency] private readonly IPlayerManager _playerManager = default!;

        private int _cachedPlayerCount = 0;

        public override void Initialize()
        {
            base.Initialize();

            SubscribeLocalEvent<PlayerCountRestrictedRoleComponent, MapInitEvent>(OnMapInit);
            SubscribeLocalEvent<PlayerCountRestrictedRoleComponent, ComponentStartup>(OnStartup);

            SubscribeLocalEvent<PlayerAttachedEvent>(OnPlayerAttached);
            SubscribeLocalEvent<PlayerDetachedEvent>(OnPlayerDetached);
        }

        private void OnMapInit(EntityUid uid, PlayerCountRestrictedRoleComponent component, MapInitEvent args)
        {
            UpdateRoleSlots(uid, component);
        }

        private void OnStartup(EntityUid uid, PlayerCountRestrictedRoleComponent component, ComponentStartup args)
        {
            UpdateRoleSlots(uid, component);
        }

        private void OnPlayerAttached(PlayerAttachedEvent args)
        {
            UpdateAllRestrictedRoles();
        }

        private void OnPlayerDetached(PlayerDetachedEvent args)
        {
            UpdateAllRestrictedRoles();
        }

        private void UpdateAllRestrictedRoles()
        {
            var currentPlayers = _playerManager.PlayerCount;

            if (_cachedPlayerCount == currentPlayers)
                return;

            _cachedPlayerCount = currentPlayers;

            var query = EntityQueryEnumerator<PlayerCountRestrictedRoleComponent>();
            while (query.MoveNext(out var uid, out var component))
            {
                UpdateRoleSlots(uid, component);
            }
        }

        private void UpdateRoleSlots(EntityUid uid, PlayerCountRestrictedRoleComponent component)
        {
            var currentPlayers = _playerManager.PlayerCount;

            var newSlots = currentPlayers >= component.MinPlayers
                ? component.SlotsWhenAvailable
                : 0;

            if (component.CurrentSlots == newSlots)
                return;

            component.CurrentSlots = newSlots;

            // Обновляем UI или другие системы
            UpdateRoleLimitInGameSystems(uid, component);

            Dirty(uid, component);

            // Логирование для отладки
            Logger.Info($"Обновлены слоты для роли {component.RoleId}: {component.CurrentSlots} (Игроков: {currentPlayers})");
        }

        /// <summary>
        /// Метод для обновления ограничений в игровых системах
        /// Нужно интегрироваться с системой выбора ролей вашей версии SS14
        /// </summary>
        private void UpdateRoleLimitInGameSystems(EntityUid uid, PlayerCountRestrictedRoleComponent component)
        {
            // TODO: Интеграция с системой ролей SS14

            // Попробуйте найти нужную систему:
            // 1. StationSpawningSystem
            // 2. JobSystem
            // 3. RoleSystem
            // 4. SpawnerSystem

            // Временное решение - только логирование
            Logger.Debug($"Требуется обновление слотов для {component.RoleId}: {component.CurrentSlots}");

            // Если в вашей версии есть система StationSpawningSystem:
            // if (EntitySystem.TryGet<StationSpawningSystem>(out var stationSpawning))
            // {
            //     stationSpawning.AdjustJobSlots(component.RoleId, component.CurrentSlots);
            // }
        }
    }
}
