using Content.Shared.Examine;
using Content.Shared.Paper;
using Robust.Shared.Map;
using Robust.Shared.Random;
using Robust.Shared.Timing;
using Robust.Shared.Utility;

namespace Content.Shared.Doors.Systems;

public sealed class HermeticCodePaperSystem : EntitySystem
{
    [Dependency] private readonly IRobustRandom _random = default!;
    [Dependency] private readonly IEntityLookup _lookup = default!;
    [Dependency] private readonly MetaDataSystem _meta = default!;
    
    public override void Initialize()
    {
        SubscribeLocalEvent<HermeticCodePaperComponent, MapInitEvent>(OnPaperInit);
    }
    
    private void OnPaperInit(EntityUid uid, HermeticCodePaperComponent comp, MapInitEvent args)
    {
        // Ищем ближайший гермозатвор без привязанной бумажки
        if (comp.LinkedSeal == EntityUid.Invalid)
        {
            FindAndLinkSeal(uid, comp);
        }
        
        // Генерируем код, если его нет
        if (string.IsNullOrEmpty(comp.GeneratedCode))
        {
            GenerateCodeForSeal(uid, comp);
        }
        
        // Обновляем описание бумажки
        UpdatePaperDescription(uid, comp);
    }
    
    private void FindAndLinkSeal(EntityUid paperUid, HermeticCodePaperComponent comp)
    {
        var xform = Transform(paperUid);
        var nearby = _lookup.GetEntitiesInRange(paperUid, 10f); // Ищем в радиусе 10 метров
        
        foreach (var entity in nearby)
        {
            if (HasComp<HermeticSealDoorComponent>(entity))
            {
                // Проверяем, нет ли у этого затвора уже привязанной бумажки
                var query = EntityQueryEnumerator<HermeticCodePaperComponent>();
                var alreadyLinked = false;
                
                while (query.MoveNext(out var otherPaper, out var otherComp))
                {
                    if (otherComp.LinkedSeal == entity)
                    {
                        alreadyLinked = true;
                        break;
                    }
                }
                
                if (!alreadyLinked)
                {
                    comp.LinkedSeal = entity;
                    Dirty(paperUid, comp);
                    return;
                }
            }
        }
    }
    
    private void GenerateCodeForSeal(EntityUid paperUid, HermeticCodePaperComponent comp)
    {
        if (comp.LinkedSeal == EntityUid.Invalid || !Exists(comp.LinkedSeal))
            return;
            
        if (!TryComp<HermeticSealDoorComponent>(comp.LinkedSeal, out var seal))
            return;
        
        // Генерируем код
        var code = "";
        for (int i = 0; i < seal.CodeLength; i++)
        {
            code += _random.Next(0, 10).ToString();
        }
        
        comp.GeneratedCode = code;
        comp.GenerationTime = _timing.CurTime;
        
        // Устанавливаем этот код в гермозатвор
        seal.CurrentCode = code;
        Dirty(comp.LinkedSeal, seal);
        Dirty(paperUid, comp);
    }
    
    private void UpdatePaperDescription(EntityUid uid, HermeticCodePaperComponent comp)
    {
        var description = "Бумажка с цифровым кодом.\n";
        
        if (comp.LinkedSeal != EntityUid.Invalid && Exists(comp.LinkedSeal))
        {
            description += $"Привязана к гермозатвору #{comp.LinkedSeal.GetHashCode() % 10000:0000}\n";
        }
        
        if (!string.IsNullOrEmpty(comp.GeneratedCode))
        {
            description += $"Код: ||{comp.GeneratedCode}||\n";
            description += $"Сгенерирован: {comp.GenerationTime:HH:mm:ss}";
        }
        
        _meta.SetEntityDescription(uid, description);
    }
    
    private void OnPaperExamined(EntityUid uid, HermeticCodePaperComponent comp, ExaminedEvent args)
    {
        if (!string.IsNullOrEmpty(comp.GeneratedCode))
        {
            args.PushMarkup(Loc.GetString("hermetic-paper-code",
                ("code", comp.GeneratedCode),
                ("time", comp.GenerationTime.ToString("HH:mm:ss"))));
        }
    }
}
