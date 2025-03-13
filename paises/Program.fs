

let numeros = [5.001; 4.998; 5.002; 4.998]

let sumLista lista = 
    lista 
    |> List.fold (fun acc e -> acc+e) 0.0 

//
// Sigmasquare without let
//
let sigmaSquare (lista: float list) =
    lista
    |> sumLista
    |> fun e -> e/float lista.Length
    |> fun mean ->
        lista 
        |> List.map (fun e -> (e-mean)*(e-mean))
    |> sumLista
    |> fun e -> e/float lista.Length

    
let sigma = sqrt(sigmaSquare numeros)
printfn $"Standard Deviation is {sigma}"




// Taller calificable

//Punto 1 Agregar población

let atlas =
    [ ("Colombia", "Bogota", 52320000)
      ("Francia", "Paris", 68290000)
      ("España", "Madrid", 48350000)
      ("Azerbaiyán", "Baku", 10150000)
      ("Alemania", "Berlin", 83280000) 
      ("Japon","Tokyo", 124500000)
    ]


// Punto 2 BuscarCapital con tryFind

let buscarCapital pais =
    match atlas |> List.tryFind (fun (p,_,_) -> p = pais) with
    | Some((_,c,_)) -> c
    | None ->
        printfn $"Capital No encontrada!"
        ""


let pais = "Alemania"
let result = buscarCapital pais

printfn $"La capital de {pais} es {result}"


//Punto 3 Buscar número de haitantes

let buscarHabitantes pais2 =  
    match atlas |> List.tryFind (fun (p,_,_) -> p = pais2) with
    | Some ((_,_,h)) -> h
    | None -> 
        printfn $"No se encontro número de habitantes"
        0


let pais2 = "Japon"
let result2 = buscarHabitantes pais2

printfn $"El numerro de habitantes de {pais2} es {result2}"

// Punto 4 Odernar paises por tamaño de población 



let obtenerlista () =
    atlas
    |> List.sortBy (fun (_,_,habitantes) -> habitantes)
    |> List.map (fun (pais,_,_) -> pais)

let imprimir = obtenerlista ()

printfn $"{imprimir}"


// Punto 5 Imprimir paises dentro de un limite establecido de habitantes

let limite lim =
    atlas 
    |> List.filter (fun (_,_,habitantes) -> (habitantes >= lim))
    |> List.map (fun (pais,_,_) -> pais)

let imprimirlimite = limite 50000000

printfn $"{imprimirlimite}"
