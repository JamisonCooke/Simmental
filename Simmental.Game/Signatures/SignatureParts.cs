namespace Simmental.Game.Signatures;

/// <summary>
/// Wraps up all the parameters from a signature into a single string. Also can unwrap them.
/// Also can have specific values set, ie., signatureParts[0] = "Short Sword"; 
/// </summary>
public class SignatureParts
{
    private string[] _parts;

    /// <summary>
    /// Creates a new SignatureParts from signatureText
    /// </summary>
    /// <param name="signatureText"></param>
    public SignatureParts(string signatureText)
    {
        List<string> p = new List<string>();
        string[] parts = signatureText.Split(",");
        foreach (string part in parts)
        {            
            p.Add(part.Trim());
        }

        string p0 = p[0];
        // p0.IndexOf('(') return 12
        // p0.Substring(12, 1) returns "(";
        // p0.Substring(13, 2) returns "mw";
        //       012345678 1 2345
        // p[0]: Short Sword (mw)
        int lp = p0.IndexOf('(');
        int rp = p0.IndexOf(')');
        var stamp = p0.Substring(lp + 1, rp - lp - 1);
        
        // SignatureFactory.IsValidStamp(stamp)

        // remove the signature stamp in ()'s from p[0]
        SignatureStamp = stamp;
        p[0] = p0.Substring(0, lp - 1).Trim();

        _parts = p.ToArray();
    }

    // var parts = new SignatureParts(typeof MeleeWeapon, name, description, damageRoll);
    // var parts = new SignatureParts("Short Sword (mw), 2d8, Rusty sword you picked up somewhere");
    // var parts = new SignatureParts("Ration of food");

    // var p = new SignatureParts(Name);
    // var p = new SignatureParts(Name, RollDice, Description);
    public SignatureParts(Type type, params string[] textParts)
    {
        this.SignatureStamp = SignatureFactory.StampFromType(type);
        _parts = textParts;
    }

    public string SignatureStamp { get; set; }

    public string this[int index]
    {
        get
        {
            return _parts[index];
        }        
    }

    /// <summary>
    /// Returns the full signature based on the SignatureStamp and the _parts[]
    /// </summary>
    /// <returns></returns>
    public string ToSignature()
    {
        StringBuilder sb = new StringBuilder();

        // Always get the name w/ the signature stamp
        sb.Append($"{_parts[0]} ({SignatureStamp})");

        // Loop over the rest of the paramters and comma delimit them on the end
        bool firstTime = true;
        foreach(string part in _parts)
        {
            if (firstTime)
            {
                firstTime = false;
                continue;
            }

            sb.Append(", ");
            sb.Append(part);
        }

        return sb.ToString();
    }

    public override string ToString()
    {
        return ToSignature();
    }

    public IDamageRoll ToDamageRoll(int index)
    {
        return new DamageRoll(_parts[index]);
    }

    public ElementEnum ToElement(int index)
    {
        return (ElementEnum) Enum.Parse(typeof(ElementEnum), _parts[index]);
    }

}
