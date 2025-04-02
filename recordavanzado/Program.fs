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