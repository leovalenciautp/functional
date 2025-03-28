//
// Este codigo es un hack para poder leer informacion
// del teclado desde Fable
//
open Fable.Core

[<Emit("prompt($0)")>]
let ReadLine (mensaje:string) : string = jsNative
