namespace Simmental.Game.Signatures;

public interface ISignature
{
    string GetSignature();

    // We can't do a NewFromSignature(string) because it would need to be a constructor. 
    // Every class that implements ISignature, should also have a constructor that takes
    // a SignatureParts. 
    // public class Foo : ISignature 
    // {
    //    public Foo(SignatureParts sp)
    //        : this(sp[0], sp[1], sp[2] ...) {}
    //    public string GetSignature() { ... }
    // }

}
