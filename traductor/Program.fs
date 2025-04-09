let dictionario =
    [
        "LAPIZ","PENCIL"
        "MESA","TABLE"
        "SOBRE","ON"
        "ESTA","IS"
        "EL","THE"
        "LA","THE"
        "AMARILLO","YELLOW"
        "BLANCO", "WHITE"
        "BLANCA", "WHITE"
    ] |> Map.ofList


let frase = "El parcial esta aplazado para despues de Semana Santa"


let buscarPalabraEnDiccionario palabra =
    dictionario 
    |> Map.tryFind palabra 
    |> Option.defaultValue palabra

let traducir (frase: string) =
    frase.ToUpper().Split(" ")
    |> Seq.map buscarPalabraEnDiccionario
    |> Seq.fold (fun acumulador elemento -> acumulador + " " + elemento) ""

let resultado = traducir frase

printfn $"{resultado}"


