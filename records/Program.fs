//
// Ejemplo de records
//

type Atlas = {
    Pais: string
    Capital: string
    Habitantes: int
}

//
// Base de datos actualizada usando records
//
let atlas =
    [ 
        {
            Pais = "Colombia"
            Capital = "Bogota"
            Habitantes = 52320000
        }
        {
            Pais = "Francia" 
            Capital = "Paris"
            Habitantes = 68290000
        }
        {
            Pais = "España" 
            Capital = "Madrid" 
            Habitantes = 48350000
        }
        {
            Pais = "Azerbaiyán" 
            Capital = "Baku"
            Habitantes = 10150000
        }
        {
            Pais = "Alemania" 
            Capital = "Berlin"; 
            Habitantes = 83280000
        }
        {
            Pais = "Japon"; 
            Capital = "Tokyo"; 
            Habitantes = 124500000
        }
    ]

type Circle = {
    Color: string
    Radius: float
    Pen: int
}

//
// Creando un record asignando todos los fields
//
let circle1 = { Color = "Blue"; Radius = 1.0; Pen = 2}

//
// Usando copy and update para otro record
//
let circle2 = { circle1 with Radius=2.0}