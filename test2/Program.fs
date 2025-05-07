// Write code or load a sample from sidebar

//Segundo parcial

//Primera pregunta
let generarPares n =
    [1..n]
    |> List.map( fun e-> e*2)

//segunda
let factorial n =
    match n with
    | 0 -> 1
    | _ -> 
        [1..n]
        |> List.fold ( fun acc e -> e*acc) 1

//tercera

type Moneda =
| Dolares
| Pesos
| Euros

let convertirValor moneda valor =
    match moneda with
    | Dolares -> valor
    | Euros -> valor*1.13m
    | Pesos -> valor/4200.0m

// Cuarta
type Cuentas = {
    Nombre: string
    Moneda: Moneda
    Balance: decimal   
}

let cuentas = [
    {
        Nombre="Banco de Colombia"
        Moneda = Pesos
        Balance = 2000000m
    }
    {
        Nombre = "UBS"
        Moneda = Euros
        Balance = 1200m
    }
    {
        Nombre = "CitiBank"
        Moneda = Dolares
        Balance = 2000m
    }
]

//Quinta
let totalizarCuentas lista =
    lista
    |> Seq.map (fun e -> (e.Moneda,e.Balance))
    |> Seq.map ( fun (moneda, balance) -> convertirValor moneda balance )
    |> Seq.sum

let result = totalizarCuentas cuentas
printfn $"Total: %0.2f{result}"