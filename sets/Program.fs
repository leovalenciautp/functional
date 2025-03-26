let numeros = [|1..20|]
let primos = [|1;2;3;5;7;11;13;17;19|]

let conjunto1 = numeros |> Set.ofArray
let conjuntos2 = primos |> Set.ofArray

let sinPrimos = Set.difference conjunto1 conjuntos2

sinPrimos 
|> Set.iter (fun e -> printfn $"{e}")
