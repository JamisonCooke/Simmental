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

    }

    // var parts = new SignatureParts(typeof MeleeWeapon, name, description, damageRoll);
    // var parts = new SignatureParts("Short Sword (mw), 2d8, Rusty sword you picked up somewhere");
    // var parts = new SignatureParts("Ration of food");

    // var p = new SignatureParts(Name);
    // var p = new SignatureParts(Name, RollDice, Description);
    public SignatureParts(Type type, params string[] textParts)
    {
        this.SignatureStamp = SignatureFactory.StampFromType(type);

    }

    public string SignatureStamp { get; set; }

    public string this[int index]
    {
        get
        {
            return "text";
        }        
    }

    public IDamageRoll ToDamageRoll(int index)
    {
        return new DamageRoll(1, 1, ElementEnum.Normal);
    }


}
