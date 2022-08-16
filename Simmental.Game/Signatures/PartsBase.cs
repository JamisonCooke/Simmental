namespace Simmental.Game.Signatures;

/// <summary>
/// Wraps up all the parameters from a signature into a single string. Also can unwrap them.
/// Also can have specific values set, ie., signatureParts[0] = "Short Sword"; 
/// </summary>
public abstract class PartsBase
{
    protected List<Part> _parts;

    /// <summary>
    /// Creates a new SignatureParts from signatureText
    /// </summary>
    /// <param name="signatureText"></param>
    public PartsBase(string signatureText)
    {
        InitializeFromSignature(signatureText); 
    }

    /// <summary>
    /// Used to parse signatureText and initialize _parts[] array and SignatureStamp
    /// </summary>
    /// <param name="signatureText"></param>
    public abstract void InitializeFromSignature(string signatureText);

    /// <summary>
    /// Returns a signature for the Task that could be parsed later by InitializeFromSignature().
    /// </summary>
    /// <returns></returns>
    public abstract string ToSignature();

    /// <summary>
    /// Number of Parts (or parameters) there are in the signature
    /// </summary>
    public int PartsCount => _parts.Count;

    /// <summary>
    /// Assigns Names and Types to the _parts[]
    /// </summary>
    /// <param name="partNames"></param>
    public void SetSignatureFormat(string partNames)
    {
        // partNames eg: "Name,Description,DamageRoll:DamageRoll"
        string[] parts = partNames.Split(',');

        int index = 0;
        foreach (string part in parts)
        {
            if (_parts.Count == index)
            {
                _parts.Add(new Part());
            }
            // Check for a : and look pull out the datatype after the : 
            string[] p = part.Split(':');
            // p.Length == 1 if there is no :,
            _parts[index].Name = p[0];
            if (p.Length == 1)
            {
                _parts[index].Type = typeof(string);
            }
            else
            {
                _parts[index].Type = Type.GetType(p[1]);
                if (_parts[index].Type == null)
                {
                    _parts[index].Type = Type.GetType("Simmental.Game.Items." + p[1]);
                }
                if (_parts[index].Type == null)
                {
                    _parts[index].Type = GetEnumType("Simmental.Interfaces." + p[1]);
                }
                if (_parts[index].Type == null)
                {
                    _parts[index].Type = Type.GetType("System." + p[1]);
                }
                if (_parts[index].Type == null)
                {
                    throw new Exception($"Invalid datatype: {p[1]}");
                }

            }
            // Set value in _parts[] -- need index 
            // Assume _parts[] has values-- assume it might not not have enough values
            index += 1;
        }
    }

    public static Type GetEnumType(string enumName)
    {
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            var type = assembly.GetType(enumName);
            if (type == null)
                continue;
            if (type.IsEnum)
                return type;
        }
        return null;
    }

    public static string Validate(string signatureText, string signatureFormat)
    {
        var sp = new SignatureParts(signatureText);
        sp.SetSignatureFormat(signatureFormat);
        return sp.GetErrorText();
    }

    /// <summary>
    /// Returns blank if there are no errors, or a human readable error message if there are problems.
    /// Requires SetSignatureFormat() to be called earlier
    /// </summary>
    /// <returns></returns>
    public string GetErrorText()
    {
        List<string> errorList = new List<string>();
        foreach (Part part in _parts)
        {
            if (string.IsNullOrEmpty(part.Value) && !string.IsNullOrEmpty(part.Name))
                errorList.Add($"{part.Name}: Missing value");

            if (string.IsNullOrEmpty(part.Name))
                errorList.Add($"Too many paramaters. Unexpected: {part.Value}");

            if (part.Type == typeof(DamageRoll) && !string.IsNullOrEmpty(part.Value))
            {
                string damageRollError = DamageRoll.ValidateDamageRoll(part.Value);
                if (!string.IsNullOrEmpty(damageRollError))
                    errorList.Add($"{part.Name}: {damageRollError}");
            }
            if (part.Type == typeof(Int32))
            {
                if (!int.TryParse(part.Value, out int i))
                {
                    errorList.Add($"{part.Name}: {part.Value} must be a number");
                }
            }
            if (part.Type == typeof(bool))
            {
                if (!bool.TryParse(part.Value, out bool j))
                {
                    errorList.Add($"{part.Name}: {part.Value} must be True or False");
                }
            }
            if (part.Type == typeof(ElementEnum))
            {
                if (!Enum.TryParse(typeof(ElementEnum), part.Value, out object e))
                {
                    errorList.Add($"{part.Name}: {part.Value} must be a damage type, ie: Fire, Lightning, Ice, Normal");
                }
            }

        }
        //Error: There was no name passed in, there was no damage roll, too many parts
        // MeleeWeapon: [Name] (mw), [Description], [DamageRoll : DamageRoll]
        // Name: Must be under 20 characters, 

        return String.Join(", ", errorList);
    }

    /// <summary>
    /// Returns a pretty format of the signature to display to the end user
    /// MeleeWeapon: [Name] (mw), [Description], [DamageRoll : DamageRoll], 
    /// </summary>
    /// <returns></returns>
    public string GetSignatureFormat()
    {
        string result = "";
        foreach (Part part in _parts)
        {
            if (result == "")
                result += $"[{part.Name}] ({this.SignatureStamp})";
            else
                result += $", [{part.Name}]";
        }
        return result;
    }

    // var parts = new SignatureParts(typeof MeleeWeapon, name, description, damageRoll);
    // var parts = new SignatureParts("Short Sword (mw), 2d8, Rusty sword you picked up somewhere");
    // var parts = new SignatureParts("Ration of food");

    // var p = new SignatureParts(Name);
    // var p = new SignatureParts(Name, RollDice, Description);
    public PartsBase(Type type, params string[] textParts)
    {
        this.SignatureStamp = StampFromType(type);

        _parts = new List<Part>();
        foreach (string part in textParts)
        {
            _parts.Add(new Part(part));
        }
    }

    public abstract string StampFromType(Type type);
    public abstract Type TypeFromStamp(string signatureStamp);

    public string SignatureStamp { get; set; }

    public string this[int index]
    {
        get
        {
            return _parts[index].Value;
        }
    }

  

    public override string ToString()
    {
        return ToSignature();
    }

    public IDamageRoll ToDamageRoll(int index)
    {
        return new DamageRoll(_parts[index].Value);
    }

    public ElementEnum ToElement(int index)
    {
        return (ElementEnum)Enum.Parse(typeof(ElementEnum), _parts[index].Value);
    }

    public int ToInt(int index)
    {
        return int.Parse(_parts[index].Value);
    }

    public bool ToBool(int index)
    {
        return bool.Parse(_parts[index].Value);
    }

}
