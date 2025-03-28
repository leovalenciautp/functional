// Write code or load a sample from sidebar

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

type Resultado =
    | CandidatoUno
    | CandidatoDos
    | CandidatoTres
    | Invalido
    | Blanco

let resultadosIniciales =
    [
        (CandidatoUno,0)
        (CandidatoDos,0)
        (CandidatoTres,0)
        (Invalido,0)
        (Blanco,0)
    ]
    |> Map.ofList

let incrementarVoto acumulador key =
    let total = acumulador |> Map.find key
    acumulador |> Map.add key (total+1)

let procesarVoto acumulador voto =
    let incremente = incrementarVoto acumulador

    match (voto.EstadoUno,voto.EstadoDos,voto.EstadoTres) with
    | (EnBlanco,EnBlanco,EnBlanco) -> incremente Blanco

    | (Marcado, EnBlanco,EnBlanco) -> incremente CandidatoUno

    | (EnBlanco,Marcado,EnBlanco) -> incremente CandidatoDos
    | (EnBlanco,EnBlanco,Marcado) -> incremente CandidatoTres
    | _ -> incremente Invalido

let totalizarVotos votos =
    votos
    |> List.fold procesarVoto resultadosIniciales

