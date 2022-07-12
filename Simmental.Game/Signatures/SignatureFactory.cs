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

    public IEnumerable<ISignature> CreateMultiple(string signatureText)
    {
        // Sword (mw), Rusty 2d8
        // Box (c), Ruby covered box
        //   Sword2 (mw), Rusty 2d8
        // Sword3 (mw), Rusty 2d8

        // Sword (mw), Rusty 2d8\nBox (c), Ruby covered box\n  Sword2 (mw), Rusty 2d8\nSword3 (mw), Rusty 2d8
        // Convert it to:  "\n " -> "\t "
        // Sword (mw), Rusty 2d8\nBox (c), Ruby covered box\t  Sword2 (mw), Rusty 2d8\nSword3 (mw), Rusty 2d8

        string tweakedSignatures = signatureText.Replace("\n ", "\t ");
        var signatures = tweakedSignatures.Split("\n");
        
        foreach(var signature in signatures)
        {
            yield return Create(signature.Replace("\t ", "\n "));
        }

    }

    public static string GetMultilineSignature(IEnumerable<ISignature> items)
    {
        string result = "";
        
        foreach(var signature in items)
        {
            if (result.Length > 0)
                result += "\n";
                
            result += signature.GetSignature();

        }

        return result;

    }

    /// <summary>
    /// Creates class instances and supports compound signatures for containers
    /// </summary>
    /// <param name="signatureText"></param>
    /// <returns></returns>
    public ISignature Create(string signatureText)
    {
        string[] lines = signatureText.Split('\n');
        
        // Should contain the last container found at each depth
        Container[] levels = new Container[lines.Length];
        ISignature result = null;

        foreach(string signature in lines)
        {
            if (string.IsNullOrEmpty(signature))        // Ignore blank lines
                continue;

            int depth = IndentationDepth(signature);
            var item = CreateSingle(signature.Trim());
            if (result == null) result = item;      // First guy is what we're returning

            // Remember to cast item to IItem before container.add
            if (item is Container container)
                levels[depth] = container;

            if (depth > 0)
                levels[depth - 1].Add((IItem)item);

        }

        return result;
    }

    /// <summary>
    /// Return the GetSignature() from the item, with nested GetSignatures() under it (delimited by /n) if item is a container
    /// </summary>
    /// <param name="backpack"></param>
    /// <returns></returns>
    public static string GetMultilineSignature(ISignature item, string indentationPrefix = "  ")
    {

        string result = item.GetSignature();

        //string signature = "Backpack (c), Leather Backpack\n" +
        //    "  Box (c), Cheap cardboard box\n" +
        //    "    Short Sword (mw), Rusty sword you picked up somewhere, 2d8";


        //    "Tile Inventory (ti)";
        //    "  Short Sword (mw), Rusty sword you picked up somewhere, 2d8";
        //    "  Short Sword (mw), Rusty sword you picked up somewhere, 2d8";


        if (item is IInventory container)
        {
            foreach(var i in container.Items)
            {
                if (i is ISignature signature)
                    result += "\n" + indentationPrefix + GetMultilineSignature(signature, indentationPrefix + "  ");
                
            }
        }

        return result;

    }

    /// <summary>
    /// Returns how deep the text is indented using two space indentation. For example
    /// '  Text' is level 1, '    Text' is level 2, and 'Text' is level 0.
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public int IndentationDepth(string text)
    {
        int i = 0;
        while (i < text.Length)
        {
            if (text[i] == ' ')
                i++;
            else
                return i / 2;
        }

        throw new Exception($"Blank or invalid Signature: '{text}'.");
    }



    public ISignature CreateSingle(string signatureText)
    {
        // Example of a abstract factory (GoF)
        var sp = new SignatureParts(signatureText);

        switch(sp.SignatureStamp)
        {
            case "mw": return new MeleeWeapon(sp);
            case "c":  return new Container(sp);
            case "xx": return new Corpse(sp);
            case "l":  return new LightSource(sp);
            case "pl": return new ProjectileLauncher(sp);
            case "rw": return new RangedWeapon(sp);
            
            default:   return null;
        }       
    }



}
