type Candidato =
    {
        Cedula: int
        Nombre: string
        Partido: string
    }

type EstadoVoto =
    | Marcado
    | EnBlanco

type Tarjeton =
    {
        EstadoUno: EstadoVoto
        EstadoDos: EstadoVoto
        EstadoTres: EstadoVoto
    }

let candidatos =
    [
        {Cedula = 101; Nombre = "JFK"; Partido ="Democrata"}
        {Cedula = 273; Nombre = "Guillermo Leon Valencia"; Partido="Liberal"}
        {Cedula = 564; Nombre = "Ronald Reagan";Partido ="Republicano"}
    ]

let mapaCandidato = [(1,273);(2,564);(3,101)] |> Map.ofList

let resultados =
    [
        {EstadoUno = Marcado;EstadoDos=EnBlanco;EstadoTres=EnBlanco}
        {EstadoUno = Marcado;EstadoDos=Marcado;EstadoTres=EnBlanco}
        {EstadoUno = EnBlanco;EstadoDos=EnBlanco;EstadoTres=EnBlanco}
        {EstadoUno = EnBlanco;EstadoDos=EnBlanco;EstadoTres=EnBlanco}
        {EstadoUno = EnBlanco;EstadoDos=Marcado;EstadoTres=EnBlanco}
        {EstadoUno = Marcado;EstadoDos=EnBlanco;EstadoTres=EnBlanco}
        {EstadoUno = Marcado;EstadoDos=EnBlanco;EstadoTres=EnBlanco}
        {EstadoUno = Marcado;EstadoDos=EnBlanco;EstadoTres=EnBlanco}
        {EstadoUno = EnBlanco;EstadoDos=Marcado;EstadoTres=EnBlanco}
        {EstadoUno = EnBlanco;EstadoDos=EnBlanco;EstadoTres=Marcado}
        {EstadoUno = EnBlanco;EstadoDos=EnBlanco;EstadoTres=Marcado}
        {EstadoUno = EnBlanco;EstadoDos=EnBlanco;EstadoTres=Marcado}
        {EstadoUno = EnBlanco;EstadoDos=EnBlanco;EstadoTres=Marcado}
        {EstadoUno = EnBlanco;EstadoDos=Marcado;EstadoTres=EnBlanco}
        {EstadoUno = EnBlanco;EstadoDos=Marcado;EstadoTres=EnBlanco}
        {EstadoUno = EnBlanco;EstadoDos=EnBlanco;EstadoTres=Marcado}
    ]