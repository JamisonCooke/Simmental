namespace Simmental.Game.Signatures;

/// <summary>
/// Wraps up all the parameters from a signature into a single string. Also can unwrap them.
/// Also can have specific values set, ie., signatureParts[0] = "Short Sword"; 
/// </summary>
public class SignatureParts : PartsBase

{
    public SignatureParts(string signatureText) : base(signatureText)
    {
    }

    public SignatureParts(Type type, params string[] textParts) : base(type, textParts)
    {
    }

    public override void InitializeFromSignature(string signatureText)
    {
        _parts = new List<Part>();
        string[] parts = signatureText.Split(",");
        foreach (string part in parts)
        {
            _parts.Add(new Part(part.Trim()));
        }

        string p0 = _parts[0].Value;
        // p0.IndexOf('(') return 12
        // p0.Substring(12, 1) returns "(";
        // p0.Substring(13, 2) returns "mw";
        //       012345678 1 2345
        // p[0]: Short Sword (mw)
        int lp = p0.IndexOf('(');
        int rp = p0.IndexOf(')');

        if (lp != -1 && lp < rp)
        {
            // remove the signature stamp in ()'s from p[0]
            SignatureStamp = p0.Substring(lp + 1, rp - lp - 1);
            _parts[0].Value = p0.Substring(0, lp - 1).Trim();
        }
    }
    /// <summary>
    /// Returns the full signature based on the SignatureStamp and the _parts[]
    /// </summary>
    /// <returns></returns>
    public override string ToSignature()
    {
        StringBuilder sb = new StringBuilder();

        // Always get the name w/ the signature stamp
        sb.Append($"{_parts[0].Value} ({SignatureStamp})");

        // Loop over the rest of the paramters and comma delimit them on the end
        bool firstTime = true;
        foreach (Part part in _parts)
        {
            if (firstTime)
            {
                firstTime = false;
                continue;
            }

            sb.Append(", ");
            sb.Append(part.Value);
        }

        return sb.ToString();
    }
    public override string StampFromType(Type type)
    {
        return SignatureFactory.StampFromType(type);
    }

    public override Type TypeFromStamp(string signatureStamp)
    {
        return SignatureFactory.TypeFromStamp(signatureStamp);
    }
}
