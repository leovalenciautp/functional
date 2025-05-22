open System
open System.Threading

//
// Variable de estadp de nuestro programa
// Aqui deben ir las cosas que cambian con el
// paso del tiempo, o segun el input del usuario
//
type State = {
    Tick: int
    Timer: int
    Continuar: bool
    MoonTheta: float
}

//
// Tiempo en milisegundos que el eventLoop
// va a dormir.
//
let eventClock = 20

//
// Numero de vueltas en el eventLoop
// para que transcurra un segundo
//
let oneSecond = 1000/eventClock

//
// Almacenamos el ancho y alto de la pantalla 
// en estad os variables
//
let screenWidth = Console.BufferWidth
let screenHeight = Console.BufferHeight

//
// Calculamos el centro de la pantalla
// recuerden restar uno porque las coordenadas
// empiezan en 0
//
let centerX = screenWidth/2-1
let centerY = screenHeight/2-1

//
// Periodo orbital de la "luna"
// En este caso da uaa vuelta completa cada
// cinco segundos
//
let moonPeriod = 5.0

//
// Definir la velocidad angular para que una vuelta 
// entera que son 2*PI radianes, ocurra en el periodo
// deseado.
//
let moonAngularSpeed = 2.0*Math.PI/(float oneSecond*moonPeriod) 

//
// Valor inicial de nuestra variable de estado
//
let initialState = {
    Tick=0
    Timer=0
    Continuar=true
    MoonTheta=0.0    
}

//
// En esta aplicacion el paso del tiempo esta marcado
// por cada Tick que pasa. Esta función actualiza
// el estado incrementando el número de ticks en cada
// vuelta del eventLoop
//
let updateTick state =
    {state with Tick=state.Tick+1}

//
// Importante chequear por una condicion de salida
// en esta App, presionando la tecla Escape hace
// que el eventLoop pare.
//
let updateExitState state =
    //
    // Es important chequear si esta disponible una tecla
    // para que la aplicacion no se bloquee en ReadKey()
    //
    if Console.KeyAvailable then
        match Console.ReadKey(true).Key with
        | ConsoleKey.Escape ->
            {state with Continuar=false}
        | _ -> state
    else
        state
//
// El timer de la esquina lleva cuanta de los segundos.
// Usamos la operacion remainder para ver si si cumplieron
// los ciclos por cada segundo
//
let updateTimer state =
    if state.Tick % oneSecond = 0 then
        {state with Timer=state.Timer+1}
    else
        state

//
// La orbita del satelite es muy simple, el angugulo es
// 𝛳 = ω*t, donde ω es la velocidad angular del satelite
//
let updateMoon state =
    {state with MoonTheta = (float state.Tick)*moonAngularSpeed}

//
// Función principal que actualiza el state del app
// Noten como usamos un pipeline para eso
//
let updateState state =
    state
    |> updateTick
    |> updateExitState
    |> updateTimer
    |> updateMoon


//
// Funcion generica para escribir algo en la pantalla
// en las coordenads cartesianas x,y con el color deseado
//
let displayMessagetAt x y color (message:string) =

    //
    // Guardamos el estado actual del color y la posicion del 
    // cursor
    let oldColor = Console.ForegroundColor
    let curX = Console.CursorLeft
    let curY = Console.CursorTop

    //
    // Aqui cambiamos el color, y la posicion
    // y se escribe el mensaje
    //
    Console.ForegroundColor <- color
    Console.SetCursorPosition(x,y)
    Console.Write message

    //
    // Finalmente restauramos todo como estaba
    //
    Console.ForegroundColor <- oldColor
    Console.SetCursorPosition(curX,curY)


//
// Esta version es la misma que la anterior pero usando 
// coordenadas polares
//
let displayMessagetAtPolar r theta color (message:string) =
  
    //
    // Esta es la conversion standard entre polar y cartesiano
    //
    //  x = r*cos(𝛳)
    //  y = r*sin(𝛳)
    //
    // Usamos un factor de 2.0 en x para compenzar el aspect ratio
    //
    let posX = 2.0*r*Math.Cos theta
    let posY = r*Math.Sin theta

    //
    // Aqui hacemos una translacion para que la orbita quede
    // centrada en la pantalla
    //
    let x = centerX + (int posX)
    let y = centerY - (int posY)

    displayMessagetAt x y color message


//
// El timer de segundos lo ponemos en la esquina
// superior derecha
//
let displayTimer state =
    let msg = $"Reloj: {state.Timer}"
    let x = screenWidth-msg.Length
    displayMessagetAt x 0 ConsoleColor.Cyan msg

//
// El satelite orbita con un radio de 8 por ahora
//
let displayMoon state =
    displayMessagetAtPolar 8.0 state.MoonTheta ConsoleColor.Yellow "👾" 

//
// Funcion para borrar la imagen pasada para que la animacion
// se vea bien
//
let clearMoon state =
    displayMessagetAtPolar 8.0 state.MoonTheta ConsoleColor.Yellow " "


// 
// Funcion principal para visualizar
// cada objecto en la pantalla
//
let displayApp state =
    displayTimer state
    displayMoon state

//
// Funcion principal para borrar objectos que sean
// animados
//
let clearApp state =
    clearMoon state   

//
// Es importante que el eventLoop duerma unos cuantos
// milisegundos para no ocupar el CPU todo el tiempo
//
let performSleep() =
    Thread.Sleep eventClock



Console.Clear()
//
// Real EventLoop for the application
//
Seq.initInfinite (fun i -> i)
|> Seq.scan ( fun state _ -> 
    clearApp state
    updateState state
) initialState 
|> Seq.takeWhile ( fun state -> state.Continuar)
|> Seq.iter ( fun state -> 
    displayApp state
    performSleep()
)
