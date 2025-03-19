//
// A game of cards
//

//
// Este es un discriminated union
//
type Pinta =
    | Corazones
    | Diamantes
    | Picas
    | Treboles


let suitUno = Corazones
let suitDos = Diamantes

let imprimirPinta carta  =
    match carta with
    | Corazones -> printfn "La Pinta es Corazones"
    | Diamantes -> printfn "La Pinta es Diamantes"
    | _ -> printfn "Aun no se que pinta es"

//
// Pasando los valores asignados
//
imprimirPinta suitUno
imprimirPinta suitDos

//
// Pasando el valor directo
//
imprimirPinta Picas 
imprimirPinta Treboles


