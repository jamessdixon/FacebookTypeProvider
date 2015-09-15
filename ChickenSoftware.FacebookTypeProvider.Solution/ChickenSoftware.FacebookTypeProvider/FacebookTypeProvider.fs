//http://blog.mavnn.co.uk/type-providers-from-the-ground-up/

module ChickenSoftware.TypeProvider

open System
open System.Reflection
open ProviderImplementation.ProvidedTypes
open Microsoft.FSharp.Core.CompilerServices

[<TypeProvider>]
type Facebook(config: TypeProviderConfig) as this =
    inherit TypeProviderForNamespaces()
    
    let ns = "ChickenSoftware.TypeProvider"
    let asm = Assembly.GetExecutingAssembly()

    let createTypes() =
        let myType = ProvidedTypeDefinition(asm,ns,"Facebook",Some typeof<obj>)
        let myProp = ProvidedProperty("MyProperty", typeof<string>, IsStatic=true,
                                        GetterCode = (fun args -> <@@ "Hello World" @@>))

        myType.AddMember(myProp)
        [myType]

    do
        this.AddNamespace(ns, createTypes())

[<assembly:TypeProviderAssembly>]
do()
