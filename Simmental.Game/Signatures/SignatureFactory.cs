namespace Simmental.Game.Signatures;

/// <summary>
/// Creates new instances of classes which implement ISignature from a signatureParts (or signatureString)
/// </summary>  
public class SignatureFactory
{
    private static Dictionary<string, Type> _stampToType;
    private static Dictionary<Type, string> _typeToStamp;

    static SignatureFactory()
    {
        _stampToType = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase);

        _stampToType["mw"] = typeof(MeleeWeapon);
        _stampToType["c"] = typeof(Container);
        _stampToType["xx"] = typeof(Corpse);
        _stampToType["f"] = typeof(Food);
        _stampToType["l"] = typeof(LightSource);
        _stampToType["portal"] = typeof(MonsterPortal);
        _stampToType["p"] = typeof(Potion);
        _stampToType["pl"] = typeof(ProjectileLauncher);
        _stampToType["rw"] = typeof(RangedWeapon);

        _typeToStamp = new();
        foreach (string stamp in _stampToType.Keys)
            _typeToStamp[_stampToType[stamp]] = stamp;
    }

    public static Type TypeFromStamp(string stamp)
    {
        //(rw)
        return _stampToType[stamp];
    }

    public static string StampFromType(Type type)
    {
        return _typeToStamp[type];
    }



}
