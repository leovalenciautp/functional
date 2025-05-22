open System
open System.Threading

type State = {
    Tick: int
    Timer: int
    Continuar: bool
    MoonTheta: float
}

let eventClock = 20
let oneSecond = 1000/eventClock

let screenWidth = Console.BufferWidth
let screenHeight = Console.BufferHeight

let centerX = screenWidth/2-1
let centerY = screenHeight/2-1

let moonPeriod = 5.0
let moonAngularSpeed = 2.0*Math.PI/(float oneSecond*moonPeriod) 

let initialState = {
    Tick=0
    Timer=0
    Continuar=true
    MoonTheta=0.0    
}


let updateTick state =
    {state with Tick=state.Tick+1}

let updateExitState state =
    if Console.KeyAvailable then
        match Console.ReadKey(true).Key with
        | ConsoleKey.Escape ->
            {state with Continuar=false}
        | _ -> state
    else
        state

let updateTimer state =
    if state.Tick % oneSecond = 0 then
        {state with Timer=state.Timer+1}
    else
        state
let updateMoon state =
    {state with MoonTheta = (float state.Tick)*moonAngularSpeed}

let updateState state =
    state
    |> updateTick
    |> updateExitState
    |> updateTimer
    |> updateMoon


let displayMessagetAt x y color (message:string) =
    let oldColor = Console.ForegroundColor
    let curX = Console.CursorLeft
    let curY = Console.CursorTop

    Console.ForegroundColor <- color
    Console.SetCursorPosition(x,y)
    Console.Write message

    Console.ForegroundColor <- oldColor
    Console.SetCursorPosition(curX,curY)

let displayMessagetAtPolar r theta color (message:string) =
    let oldColor = Console.ForegroundColor
    let curX = Console.CursorLeft
    let curY = Console.CursorTop

    let posX = 2.0*r*Math.Cos theta
    let posY = r*Math.Sin theta

    let x = centerX + (int posX)
    let y = centerY - (int posY)

    Console.ForegroundColor <- color
    Console.SetCursorPosition(x,y)
    Console.Write message

    Console.ForegroundColor <- oldColor
    Console.SetCursorPosition(curX,curY)



let displayTimer state =
    let msg = $"Reloj: {state.Timer}"
    let x = screenWidth-msg.Length
    displayMessagetAt x 0 ConsoleColor.Cyan msg

let displayMoon state =
    displayMessagetAtPolar 8.0 state.MoonTheta ConsoleColor.Yellow "👾" 

let clearMoon state =
    displayMessagetAtPolar 8.0 state.MoonTheta ConsoleColor.Yellow " "

let displayApp state =
    displayTimer state
    displayMoon state

let clearApp state =
    clearMoon state   

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
