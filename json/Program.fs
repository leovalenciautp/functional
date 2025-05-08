open System.Text.Json
//XML

type Ciudadano = {
    Nombre: string
    Cedula: int
}

let a = """ [{ "Nombre": "Leonardo", "Cedula": 12}, { "Nombre": "Manuel", "Cedula" : 14} ] """

let result = JsonSerializer.Deserialize<list<Ciudadano>> a

result
|> List.iter (fun e -> printfn $"Nombre: {e.Nombre} *** Cedula {e.Cedula}")


let lista = [
    {
        Nombre = "Madelein"
        Cedula = 3
    }
    {
        Nombre = "Kelly"
        Cedula = 4
    }
    {
        Nombre = "Mariana"
        Cedula = 5
    }
]

let json = JsonSerializer.Serialize lista
printfn $"JSON serializado es\n {json}"