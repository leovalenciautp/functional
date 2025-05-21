open System

let dibujarRectanguloAt x y color n m =
    let oldColor = Console.BackgroundColor
    Console.BackgroundColor <- color

    let fila =
        [1..n]
        |>
        List.fold (fun acc _ -> acc + " ") ""

    [0..m-1]
    |> List.iter (fun i -> 
        Console.SetCursorPosition(x,y+i)
        Console.Write fila)


    Console.BackgroundColor <- oldColor
// Vamos a hacer desaparecer el cursor

type State = {
    x: int
    y: int
}


Console.CursorVisible <- false

let oldBackground = Console.BackgroundColor
let oldForeground = Console.ForegroundColor

Console.ForegroundColor <- ConsoleColor.Yellow
Console.BackgroundColor <- ConsoleColor.Blue

Console.Clear()
dibujarRectanguloAt 0 0 ConsoleColor.Red 25 10
Seq.initInfinite (fun _ -> Console.ReadKey().Key)
|> Seq.takeWhile (fun key -> key <> ConsoleKey.Escape)
|> Seq.scan (fun state k ->
    dibujarRectanguloAt state.x state.y ConsoleColor.Blue 25 10
    match k with
    | ConsoleKey.RightArrow ->
        dibujarRectanguloAt (state.x+1) state.y ConsoleColor.Red 25 10
        {state with x = state.x+1}
    | ConsoleKey.LeftArrow ->
        dibujarRectanguloAt (state.x-1) state.y ConsoleColor.Red 25 10
        {state with x = state.x-1}
    
    | ConsoleKey.UpArrow ->
        dibujarRectanguloAt state.x (state.y-1) ConsoleColor.Red 25 10
        {state with y = state.y-1}
    | ConsoleKey.DownArrow ->
        dibujarRectanguloAt state.x (state.y+1) ConsoleColor.Red 25 10
        {state with y = state.y+1}
    | _ -> state
) {x=0;y=0}
|> Seq.toList
|> ignore




Console.CursorVisible <- true
Console.ForegroundColor <- oldForeground
Console.BackgroundColor <- oldBackground
Console.Clear()