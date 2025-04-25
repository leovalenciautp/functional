

let generarImpares n =
    [0..(n-1)]
    |> List.map(fun i -> 2*i+1)

let r = generarImpares 5
printfn $"{r}"

let generarCuadrados lista =
    lista
    |> List.map (fun x -> x*x)

let c = generarCuadrados [3;7;9]
printfn $"{c}"

let calcularSuma lista =
    lista
    |> List.fold (fun acc n -> acc+n) 0


let suma = calcularSuma [5;1;8]


type Atlas = {
    Pais: string
    Capital: string
    Habitantes: int
}

let atlas = [
    {
        Pais = "Colombia"
        Capital = "Bogota"
        Habitantes = 48000000
    }
    {
        Pais = "Alemania"
        Capital = "Berlin"
        Habitantes = 60000000
    }
    {
        Pais = "España"
        Capital = "Madrid"
        Habitantes = 49000000
    }
]

let reportarPaises() =
    atlas
    |> List.sortBy (fun e -> e.Habitantes)
    |> List.rev
    |> List.map (fun r -> (r.Pais,r.Habitantes))

reportarPaises()
|> List.iter (fun i -> printfn $"{i}")

let totalHabitantes() =
    atlas
    |> List.fold (fun acc r -> acc+r.Habitantes) 0

let hab = totalHabitantes()
printfn $"{hab}"

let buscarCapital pais =
    match atlas |> List.tryFind (fun e -> e.Pais = pais) with
    | Some r -> Some r.Capital
    | None -> None

let buscarCapital2 pais =
    atlas
    |> List.tryFind (fun e -> e.Pais = pais)
    |> Option.map (fun e -> e.Capital)

let imprimirCapital pais =
    match buscarCapital2 pais with
    | Some capital -> printfn $"La capital es {capital}"
    | None -> printfn $"Pais no existe!!"

let imprimirCapital2 pais =
    let c = buscarCapital2 pais |> Option.defaultValue "Pais no existe!!"
    printfn $"{c}"

imprimirCapital2 "Alemania"

type Moneda =
| Pesos
| Dolares
| Euros

type Cuenta = {
    Banco: string
    Moneda: Moneda
    Balance: decimal
}

let cuentas = [
    {
        Banco = "Bancolombia"
        Moneda = Pesos
        Balance = 2500000m
    }
    { 
        Banco = "CitiBank"
        Moneda = Dolares
        Balance = 5000m
    }
    {
        Banco = "HSBC"
        Moneda = Euros
        Balance = 800m
    }
    {
        Banco = "UBS"
        Moneda = Euros
        Balance = 2300m
    }
]

let agruparCuentas cuentas =
    cuentas
    |> List.groupBy (fun e -> e.Moneda)

agruparCuentas cuentas
|> List.iter (fun e -> printfn $"{e}")