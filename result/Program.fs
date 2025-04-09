open System

// 
// El sistema Random es parte de .Net
// aqui inicializamos el objecto que nos va
// a dar valores aleatorios
//
let rnd = new Random()
//
// De ahora en adelante usamos Result
//
let simpleFunction () =
    //
    // Simplemente pedimos un numero entre 0.0 y 1.0 al azar
    //
    let a = rnd.NextDouble() 
    if a < 0.85 then // Esta funciona retorna ok 85% del tiempo
        Ok a
    else
        Error "Explotó internet"

let procesarDatos dato =
    let a = rnd.NextDouble()
    if a < 0.85 then
        Ok (dato**3.0+1.5)
    else
        Error "Primer paso fracasó"
    

let masProceso dato =
    let a = rnd.NextDouble()
    if a < 0.85 then
        Ok (dato*10.0+3.0)
    else
        Error "Segundo paso fracasó"

let otroMasProceso dato =
    Ok (dato/1.5)

let programa() =
    simpleFunction()
    |> Result.bind procesarDatos
    |> Result.bind masProceso
    |> Result.bind otroMasProceso


let result = programa()
match result with
| Ok x -> printfn $"Perfecto: {x}"
| Error m -> printfn $"SOS! {m}"


