open System
open System.IO
let presentarMenu() =
    Console.BackgroundColor <- ConsoleColor.Blue
    Console.Clear()
    Console.ForegroundColor <- ConsoleColor.Cyan
    printfn "Bienvenido al proyecto final!!"
    printfn "Elija una opcion:"
    Console.ForegroundColor <- ConsoleColor.Yellow
    printfn "********* 1 ***** Agregar Información"
    printfn "********* 2 ***** Mostrar Información"
    printfn "********* 3 ***** Salir"



let agregarInformacion() =

    Seq.initInfinite ( fun _ ->
        printfn "Escribe SALIR para terminar"
        printf "Entre un Nombre: "
        Console.ReadLine()
    )
    |> Seq.takeWhile (fun linea ->
        match linea with
        | "SALIR" -> false
        | _ -> true
    )
    |> Seq.toList
    |> fun info -> File.AppendAllLines("datos.txt",info)


let mostrarInfornacion() =
    Console.ForegroundColor <- ConsoleColor.Green
    try
        File.ReadAllLines("datos.txt")
        |> Seq.iter (fun linea ->
            printfn $"{linea}"
        )
    with
    | _ -> printfn "No data to display!"
    Console.ForegroundColor <- ConsoleColor.Cyan
    printfn "Presiona cualquier tecla para continuar"
    Console.ReadKey() |> ignore
    


let mainLoop() =
    Seq.initInfinite (fun _ ->
        presentarMenu()
        Console.ReadKey()
    )
    |> Seq.takeWhile ( fun o ->
        printfn ""
        match o.KeyChar with 
        | '1' -> 
            agregarInformacion()
            true
        | '2' -> 
            mostrarInfornacion()
            true
        | '3' ->
            printfn "Good bye!"
            false
        | _ ->
            Console.ForegroundColor <- ConsoleColor.Red
            printfn "Opcion erreda intente de nuevo"
            printfn "Presiona cualquier teclar para continuar"
            Console.ReadKey() |> ignore
            true
    )
    |> Seq.toList

mainLoop() |> ignore