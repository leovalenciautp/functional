let miMapa = [(1,"Cali");(2,"Bogota");(3,"Medellin")] |> Map.ofList

//
// Vamos a adicionar una ciudad mas
// Recuerden que en F# todo es inmutable
// asi que un nuevo mapa es retornado
//
let nuevoMapa = miMapa |> Map.add 3 "Pereira"
