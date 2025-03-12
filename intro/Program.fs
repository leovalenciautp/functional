//
// Base de datos inicial para taller
//
let atlas =
    [ ("Colombia", "Bogota")
      ("Francia", "Paris")
      ("España", "Madrid")
      ("Azerbaiyán", "Baku")
      ("Alemania", "Berlin") 
      ("Japon","Tokyo")
    ]

let printCountry l =
    l
    |> List.iter (fun (pais,_) -> printfn $"{pais}")

let printCapital l =
    l
    |> List.iter (fun (_,capital) -> printfn $"{capital}")

let printTodo l =
    l
    |> List.iter 
        (fun (pais,capital) 
            -> 
                printfn $"La capital de {pais} es {capital}")

printfn "-------"

atlas
|> List.sortBy (fun (pais,_) -> pais)
|> printCountry

printfn "-------"

atlas
|> List.sortBy (fun (_,capital) -> capital)
|> printCapital



printfn "-------"
printTodo atlas

let paises =
    atlas
    |> List.map (fun (pais,_) -> pais)

let capitales =
    atlas
    |> List.map (fun (_,capital)-> capital)

let buscarCapitalOld pais =
    let (_,capital) =
        atlas 
        |> List.find (fun (p,_) -> p = pais)
    capital
let buscarCapital pais =
    match atlas |> List.tryFind (fun (p,_) -> p = pais) with
    | Some((_,b)) -> b
    | None ->
        printfn $"Capital No encontrada!"
        ""


let pais = "Japon"
let result = buscarCapital pais

printfn $"La capital de {pais} es {result}"

let numeros = [1..20]

let impares =
    numeros
    |> List.filter (fun e -> (e % 2) <> 0)

impares 
|> List.iter (fun e -> printfn $"{e}")

let listaOriginal = ["Colombia";"España"]
let otraLista = ["Alemania";"Japon"]

let nuevaLista = otraLista @ listaOriginal

match nuevaLista with
| cabeza :: cola -> printfn $"La cabeza de la lista es {cabeza} y la cola es {cola}"
| [] -> printfn $"La lista esta vacia"


let medidas = [5.001;4.998;5.002;4.998]

//
// This function computes the sum of all elements on the list
//
let sumLista lista =
    lista 
    |> List.fold (fun acc e -> acc+e) 0.0

let sigmaSquare (lista: float list) =
    let n = lista.Length
    let mean = (sumLista lista)/(float n)
    let minSquare = 
        lista
        |> List.map ( fun e -> (e-mean)*(e-mean))
    let variance = (sumLista minSquare)/(float n)
    variance

let sigma = sqrt(sigmaSquare medidas)

printfn $"La desviacion standard es {sigma}"