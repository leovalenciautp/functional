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
    | Jack of Pinta
    | CartaNumero of int * Pinta // Quizz, que tyipo es este?

//
// Helper functions for the big homework
//

//
// Esta funcion nos retorna la pinta de cuaquier carta
//

let obtenerPintaDeCarta carta =
    match carta with 
    | As pinta -> pinta
    | Rey pinta -> pinta
    | Reina pinta -> pinta
    | Jack pinta -> pinta
    | CartaNumero(_,pinta) -> pinta

let result = obtenerPintaDeCarta (CartaNumero(10,Corazones))
printfn $"La pinta de la carta es: {result}"

//
// Funcion que chequea si una lista de cartas es de la misma
// pinta
//
let mismaPintaEnLista listaCartas =
    //
    // La pinta de muestra la sacamos del primer elemento de la lista
    //
    let pintaMuestra = listaCartas|> List.head |> obtenerPintaDeCarta

    //
    // Usamos forall para comprobar 
    listaCartas
    |> List.forall(fun e -> obtenerPintaDeCarta(e) = pintaMuestra)

let test = mismaPintaEnLista [As(Treboles);CartaNumero(10,Corazones);Rey(Treboles);CartaNumero(3,Corazones)]
printfn $"Misma pinta es {test}"

//
// Obtener valor de la carta, esto es util para
// ordernar la mano
//
let obtenerValorDeCarta carta =
    match carta with
    | CartaNumero(x,_) -> x
    | Jack(_) -> 11
    | Reina(_) -> 12
    | Rey(_) -> 13
    | As(_)-> 14


//
// Esta funcion da un valor arbitrario a cada suit
//

let obtenerValorDePinta carta =
    match carta |> obtenerPintaDeCarta with
    | Treboles -> 1
    | Picas -> 2
    | Corazones -> 3
    | Diamantes -> 4

let compararCartas carta1 carta2 =
    let pinta1 = carta1 |> obtenerValorDePinta
    let pinta2 = carta2 |> obtenerValorDePinta
  
    let c1 = compare pinta1 pinta2
    if c1 <> 0 
        then
            c1
        else
            let valor1 = carta1 |> obtenerValorDeCarta
            let valor2 = carta2 |> obtenerValorDeCarta
            compare valor1 valor2

