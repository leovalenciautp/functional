type NombreCompleto = {
    PrimerNombre: string
    SegundoNombre: string option
    PrimerApellido: string
    SegundoApellido: string option
}

type Directorio = {
    Nombre: NombreCompleto
    Telefono: int
    Email: string
}

let directorio = [
    {
        Nombre = {PrimerNombre = "Leonardo";SegundoNombre=None;PrimerApellido="Valencia";SegundoApellido=Some "Olarte"}
        Telefono = 2
        Email = "leonardo.valencia@uto.edu.co"
    }
]

let persona = directorio[0]
printfn $"{persona.Nombre.PrimerNombre}"

type Colores =
    | Blanco
    | Rojo
    | Negro
    // En este ejemplo RGB espera una tupla de 3 valores enteros
    | RGB of int*int*int 

let colorUno = Colores.Blanco
let colorDos = Rojo //No hay necesidad de usar Colores.Rojo

//Aqui tenemos que darle valor a RGB
let colorTres = RGB(255,255,0) 

//
// Usando match con el discriminated union
//
match colorTres with
| Blanco -> printfn "El color es Blanco"
| Rojo -> printfn "El color es Rojo"
| Negro -> printfn "El color es Nego"
| RGB(r,g,b) -> printfn $"Components {r} ** {g} ** {b}"