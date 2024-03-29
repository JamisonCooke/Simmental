﻿namespace Simmental.Interfaces;

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

    // Class should also implement GetSignatureFormat as a static-- (can't require in an interface)
    // public static string GetSignatureFormat()


}
