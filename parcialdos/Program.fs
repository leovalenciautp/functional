type Moneda =
    | Dolar
    | Pesos
    | Euro
    | Yuan

type Balance =
    Valor of Moneda * decimal

type Cuenta = {
    Banco: string
    Saldo: Balance
}

let cuentas = [
    {
        Banco = "Bancolombia"
        Saldo = Valor(Pesos,100000000m)
    }
    {
        Banco = "HSBC"
        Saldo = Valor(Yuan,500000m) 
    }
    {
        Banco = "LA CAIXA"
        Saldo = Valor(Euro,125000m)
    }
    {
        Banco = "Citibank"
        Saldo = Valor(Dolar,345000m)
    }
]

let cambioMoneda (cantidad:Balance) =
    match cantidad with 
    | Valor(Dolar,x) -> x
    | Valor(Pesos,x) -> x/4162m
    | Valor(Euro,x) -> 1.1073m*x
    | Valor(Yuan,x) -> x/7.2823m

let totalizarCuentas cuentas =
    cuentas
    |> List.fold (fun total cuenta -> total + cambioMoneda cuenta.Saldo) 0m

let totalizarCuentas2 cuentas =
    cuentas
    |> Seq.map (fun elemento -> elemento.Saldo)
    |> Seq.map cambioMoneda
    |> Seq.sum

let portafolio = cuentas |> totalizarCuentas
printfn $"{portafolio}"