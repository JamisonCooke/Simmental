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

}
