let miMapa = [(1,"Cali");(2,"Bogota");(3,"Medellin")] |> Map.ofList

//
// Vamos a adicionar una ciudad mas
// Recuerden que en F# todo es inmutable
// asi que un nuevo mapa es retornado
//
let nuevoMapa = miMapa |> Map.add 3 "Pereira"

type Ciudadano =
    {
        Cedula: int
        Nombre: string
    }

let miPais = 
    [
        {Cedula = 1; Nombre = "Leonardo Valencia"}
        {Cedula = 2; Nombre = "Carlos Meneses"}
        {Cedula = 3; Nombre = "Luis Carlos Gonzales"}
    ]

//
// Vamos a construir un mapa para buscar rapido por
// cedula
//

let fastAccess =
    miPais
    |> List.fold (fun acc e -> acc |> Map.add e.Cedula e) Map.empty

fastAccess
|> Map.iter (fun key value -> printfn $"Key: {key} Value: {value}")