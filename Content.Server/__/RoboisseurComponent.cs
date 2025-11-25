using Robust.Shared.Prototypes;

namespace Content.Server.Roboisseur.Roboisseur;

[RegisterComponent]
public sealed partial class RoboisseurComponent : Component
{
    [ViewVariables]
    [DataField("accumulator")]
    public float Accumulator = 0f;

    [ViewVariables(VVAccess.ReadOnly)]
    [DataField("impatient")]
    public bool Impatient { get; set; } = false;

    [ViewVariables]
    [DataField("resetTime")]
    public TimeSpan ResetTime = TimeSpan.FromMinutes(10);

    [DataField("barkAccumulator")]
    public float BarkAccumulator = 0f;

    [DataField("barkTime")]
    public TimeSpan BarkTime = TimeSpan.FromMinutes(1);

    /// <summary>
    /// Antispam.
    /// </summary>

    public TimeSpan StateTime = default!;
    public DateTime TimerStartTime { get; set; }
    public TimeSpan TimerDuration { get; } = TimeSpan.FromMinutes(25);

    [DataField("stateCD")]
    public TimeSpan StateCD = TimeSpan.FromSeconds(5);

    [ViewVariables(VVAccess.ReadWrite)]
    public EntityPrototype DesiredPrototype = default!;

    [ViewVariables(VVAccess.ReadWrite)]
    public int DoneTotal = 0;

    [DataField("timerRunNow")]
    public IReadOnlyList<string> TimerRunNow = new[]
    {
        "roboisseur-timerenablenow-1",
        "roboisseur-timerenablenow-2",
        "roboisseur-timerenablenow-3",
        "roboisseur-timerenablenow-4",
        "roboisseur-timerenablenow-5"
    };

    [DataField("demandMessages")]
    public IReadOnlyList<string> DemandMessages = new[]
    {
        "roboisseur-request-1",
        "roboisseur-request-2",
        "roboisseur-request-3",
        "roboisseur-request-4",
        "roboisseur-request-5",
        "roboisseur-request-6"
    };

    [DataField("impatientMessages")]
    public IReadOnlyList<string> ImpatientMessages = new[]
    {
        "roboisseur-request-impatient-1",
        "roboisseur-request-impatient-2",
        "roboisseur-request-impatient-3",
    };

    [DataField("demandMessagesTier2")]
    public IReadOnlyList<string> DemandMessagesTier2 = new[]
    {
        "roboisseur-request-second-1",
        "roboisseur-request-second-2",
        "roboisseur-request-second-3"
    };

    [DataField("rewardMessages")]
    public IReadOnlyList<string> RewardMessages = new[]
    {
        "roboisseur-thanks-1",
        "roboisseur-thanks-2",
        "roboisseur-thanks-3",
        "roboisseur-thanks-4",
        "roboisseur-thanks-5"
    };

    [DataField("rewardMessagesTier2")]
    public IReadOnlyList<string> RewardMessagesTier2 = new[]
    {
        "roboisseur-thanks-second-1",
        "roboisseur-thanks-second-2",
        "roboisseur-thanks-second-3",
        "roboisseur-thanks-second-4",
        "roboisseur-thanks-second-5"
    };

    [DataField("rejectMessages")]
    public IReadOnlyList<string> RejectMessages = new[]
    {
        "roboisseur-deny-1",
        "roboisseur-deny-2",
        "roboisseur-deny-3"
    };

    [DataField("tier1Protos")]
    public List<string> Tier2Protos = new();

    [DataField("tier2Protos")]
    public List<string> Tier2Protos = new();

    [DataField("tier3Protos")]
    public List<string> Tier2Protos = new();

    [DataField("robossuierRewards")]
    public IReadOnlyList<string> RobossuierRewards = new[]
    {
        //"DrinkIceCreamGlass",
        //"FoodFrozenPopsicleOrange",
        //"FoodFrozenPopsicleBerry",
        //"FoodFrozenPopsicleJumbo",
        //"FoodFrozenSnowconeBerry",
        //"FoodFrozenSnowconeFruit",
        //"FoodFrozenSnowconeClown",
        //"FoodFrozenSnowconeMime",
        //"FoodFrozenSnowconeRainbow",
        //"FoodFrozenCornuto",
        //"FoodFrozenSundae",
        //"FoodFrozenFreezy",
        //"FoodFrozenSandwichStrawberry",
        //"FoodFrozenSandwich",
        "ClothingNeckCloakBotanistCloak",
        "ClothingNeckBlackCloak",
        "ClothingNeckCloakRose",
        "ClothingUniformJumpsuitBlueGalaxy",
        "ClothingUniformJumpsuitRedGalaxy",
        //"ClothingUniformJumpsuitStrangeBunny",
        "ClothingUniformReallyBlackSuitSkirt",
        "ClothingNeckRegalMantle",
        "ClothingHeadHatRedRog",
        "ClothingHeadHatCueball",
        "CrateHydroponicsSeedsExotic",
        "CrateVendingMachineRestockChefvendFilled",
        //"CrateFunATV",
        "CrateNPCDuck",
        "ClothingOuterHardsuitLing",
        "ClothingHeadHatFancyCrown",
        "MaterialHideBear",
        "ClothingOuterSuitIan",
        "ClothingOuterRedRacoon",
        "TeslaToy",
        "PlushieGhostRevenant",
        "Chainsaw",
        "MobMousi",
        "MechRipleyBattery",
        "ClothingBackpackDuffelHolding",
        "ClothingShoesBootsSpeed",
        "SheetPlasma",
        "CognizineChemistryBottle",
        "CombatMedipen",
        //"CloningConsoleComputerCircuitboard",
        //"CloningPodMachineCircuitboard",
        "FloorTileItemAstroGrass30",
        "FloorTileItemAstroIce30",
    };
}
