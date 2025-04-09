let lista = [1..20]

let result =
    lista
    |> List.groupBy (fun elemento -> elemento % 2)

printfn $"{result}"

type Moneda =
    | Peso 
    | Dolar 
    | Euro
    
type Saldo = {
    Moneda: Moneda
    Valor: decimal
}

let valores = [
    {Moneda = Peso; Valor = 100000m}
    {Moneda = Dolar; Valor= 500m}
    {Moneda = Euro; Valor= 2000m}
    {Moneda = Dolar; Valor= 400m}
    {Moneda = Euro; Valor= 230m}
    {Moneda = Euro; Valor= 300m}
    {Moneda = Peso; Valor= 2000000m}
]
             
                

let totalizarMoneda valores =
    valores
    |> List.groupBy (fun x -> x.Moneda)
    |> List.map ( fun (key,lista) ->
            let total = lista |> List.map (fun x -> x.Valor) |> List.sum
            (key, total)
        )

let grupo = totalizarMoneda valores
printfn $"{grupo}"