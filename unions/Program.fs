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

type CartaDeJuego =
    | As of Pinta
    | Rey of Pinta
    | Reina of Pinta
    | Sota of Pinta
    | CartaNumero of int * Pinta // Quizz, que tyipo es este?


let suitUno = Corazones
let suitDos = Diamantes

let cartaUno = Rey Diamantes
let cartaDos = As Corazones

let cartaTres = CartaNumero(8,Treboles)

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


