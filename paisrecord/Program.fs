type Atlas = {
    Pais: string
    Capital: string
    Habitantes: int
    Pib: float //field
}

let atlas =
    [ 
        {
            Pais = "Colombia"
            Capital = "Bogota"
            Habitantes = 52320000
            Pib = 363.5
        }
        {
            Pais = "Francia" 
            Capital = "Paris"
            Habitantes = 68290000
            Pib = 3052.0
        }
        {
            Pais = "España" 
            Capital = "Madrid" 
            Habitantes = 48350000
            Pib = 1620.0
        }
        {
            Pais = "Azerbaiyán" 
            Capital = "Baku"
            Habitantes = 10150000
            Pib = 72.36
        }
        {
            Pais = "Alemania" 
            Capital = "Berlin"; 
            Habitantes = 83280000
            Pib = 4520.0
        }
        {
            Pais = "Japon"; 
            Capital = "Tokyo"; 
            Habitantes = 124500000
            Pib = 4204.0
        }
    ]

let elemento = atlas[1]
let nuevoPais = { elemento with Habitantes = 78290000; Pib =8000.0}
printfn $"El pais es: {nuevoPais.Pais} {nuevoPais.Habitantes}"

let buscarCapital pais =
    match atlas |> List.tryFind (fun r -> r.Pais = pais) with
    | Some(r) -> r.Capital
    | None ->
        printfn $"Capital No encontrada!"
        ""


let pais = "Alemania"
let result = buscarCapital pais

printfn $"La capital de {pais} es {result}"


//Punto 3 Buscar número de haitantes

let buscarHabitantes pais =  
    match atlas |> List.tryFind (fun r -> r.Pais = pais) with
    | Some (r) -> r.Habitantes
    | None -> 
        printfn $"No se encontro número de habitantes"
        0


let pais2 = "Japon"
let result2 = buscarHabitantes pais2

printfn $"El numerro de habitantes de {pais2} es {result2}"

// Punto 4 Odernar paises por tamaño de población 



let obtenerlista () =
    atlas
    |> List.sortBy (fun r -> r.Habitantes)
    |> List.map (fun r -> r.Pais)

let imprimir = obtenerlista ()

printfn $"{imprimir}"


// Punto 5 Imprimir paises dentro de un limite establecido de habitantes

let limite lim =
    atlas 
    |> List.filter (fun r -> (r.Habitantes >= lim))
    |> List.map (fun r -> r.Pais)

let imprimirlimite = limite 50000000

printfn $"{imprimirlimite}"

let ordenarPorPib () =
    atlas
    |> List.sortBy (fun r -> r.Pib)
    |> List.map (fun r -> r.Pais)
    |> List.rev

let bigCountries = ordenarPorPib ()
printfn $"{bigCountries}"